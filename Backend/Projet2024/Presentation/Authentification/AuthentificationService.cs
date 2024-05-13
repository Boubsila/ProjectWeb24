using DataAccesLayer;
using Domaine;
using Hl7.Fhir.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Presentation.Authentification
{
    public class AuthentificationService
    {
        IConfiguration _config;
        WebDbContext _context;
        public AuthentificationService(IConfiguration config, WebDbContext context)
        {
            _config = config;
            _context = context;
        }

        //genérer le token 
       
        private string GenerateJSONWebToken(string username, string role)
        {
            var secretKey = _config["JwtSettings:SecretKey"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString()),
                new Claim(ClaimTypes.Role, role)
            };

            var claimIdentity = new ClaimsIdentity(claims, "JwtBearer");

            var jwtIssuer = _config["JwtSettings:Issuer"];
            var jwtAudience = _config["JwtSettings:Audience"];
            var tokenExpiry = int.Parse(_config["JwtSettings:ExpirationMinutes"]);

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claimIdentity.Claims,
                expires: DateTime.UtcNow.AddMinutes(tokenExpiry),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //methode qui va hasher un password qui recoit en parametre
        private string HashPassword(string password, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
            password: Encoding.UTF8.GetBytes(password),
            salt: saltBytes,
            iterations: 10,
            hashAlgorithm: HashAlgorithmName.SHA512,
            outputLength: 10
             );
            return Convert.ToHexString(hash);

        }

        //Methode pour ajouter un user dans la BD
        public async Task RegisterUser(UserDTO newUserDTO)
        {
            if (newUserDTO == null)
            {
                throw new ArgumentNullException(nameof(newUserDTO), "UserDTO cannot be null");
            }

            // Vérifier si les propriétés obligatoires sont définies
            if (string.IsNullOrEmpty(newUserDTO.Name) || string.IsNullOrEmpty(newUserDTO.FirstName)
                || string.IsNullOrEmpty(newUserDTO.UserName) || string.IsNullOrEmpty(newUserDTO.Password))
            {
                throw new ArgumentException("UserDTO properties cannot be null or empty");
            }

            //Convertir les noms d'utilisateur en minuscules pour comparer
            string lowerCaseUserName = newUserDTO.UserName.ToLower();
            string lowerCaseFirstName = newUserDTO.FirstName.ToLower();
            string lowerCaseName = newUserDTO.Name.ToLower();

            //Vérifier si un utilisateur avec le même nom d'utilisateur existe déjà
            var existingUser = await _context.Users
                .AnyAsync(u => u.UserName.ToLower() == lowerCaseUserName);

            if (existingUser)
            {
                throw new Exception("User with this login already exists");
            }

            // Vérifier si un utilisateur avec le même nom et prénom existe déjà
            var existingUserByName = await _context.Users
                .AnyAsync(u => u.Name.ToLower() == lowerCaseName && u.FirstName.ToLower() == lowerCaseFirstName);

            if (existingUserByName)
            {
                throw new Exception("User already exists");
            }

            // Créer un nouvel utilisateur à partir des données DTO
            var newUser = new User
            {
                Name = newUserDTO.Name,
                FirstName = newUserDTO.FirstName,
                UserName = newUserDTO.UserName,
                Password = newUserDTO.Password,
                RoleId = newUserDTO.RoleId,
            };

            // Générer un nouveau sel et hacher le mot de passe
            newUser.Salt = GenerateSecureSalt();
            newUser.Password = HashPassword(newUser.Password, newUser.Salt);

            // Ajouter le nouvel utilisateur à la base de données
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
        }



        //mettre a jour un user 
        public async Task UpdateUser(int userId, UserDTO updatedUserDTO)
        {
            var userToUpdate = await _context.Users.FindAsync(userId);

            if (userToUpdate == null)
            {
                throw new Exception("User not found");
            }

            var existingUserWithSameLogin = await _context.Users
                .AnyAsync(u => u.UserName.ToLower() == updatedUserDTO.UserName.ToLower() && u.UserId != userId);
            if (existingUserWithSameLogin)
            {
                throw new Exception("User with this login already exists");
            }

            var existingUserByName = await _context.Users
                .AnyAsync(u => u.Name.ToLower() == updatedUserDTO.Name.ToLower() && u.FirstName.ToLower() == updatedUserDTO.FirstName.ToLower() && u.UserId != userId);
            if (existingUserByName)
            {
                throw new Exception("User with this name and first name combination already exists");
            }

            userToUpdate.Name = updatedUserDTO.Name;
            userToUpdate.FirstName = updatedUserDTO.FirstName;
            userToUpdate.UserName = updatedUserDTO.UserName;
            userToUpdate.RoleId = updatedUserDTO.RoleId;

            if (!string.IsNullOrWhiteSpace(updatedUserDTO.Password))
            {
                userToUpdate.Salt = GenerateSecureSalt();
                userToUpdate.Password = HashPassword(updatedUserDTO.Password, userToUpdate.Salt);
            }

            _context.Users.Update(userToUpdate);
            await _context.SaveChangesAsync();
        }


        //supprimer un user 
        public async Task DeleteUser(int userId)
        {
            var userToDelete = await _context.Users.FindAsync(userId);

          
                if (userToDelete == null)
                {
                    throw new Exception("User not found");
                }

                _context.Users.Remove(userToDelete);

                await _context.SaveChangesAsync();
         

        }

        //générer un salt
        private string GenerateSecureSalt(int size = 16)
        {
            byte[] salt = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }


        //Login
        public string Login(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserName.ToLower() == username.ToLower());

            try
            {
                if (user == null)
                {
                    throw new Exception("User not found");
                }

                var hashedPassword = HashPassword(password, user.Salt);
                if (user.Password == hashedPassword)
                {

                    var token = GenerateJSONWebToken(username, user.RoleId.ToString());
                    return token;
                }
                else
                {
                    throw new Exception("Invalid UserId or Password");
                   
                }
            }catch (Exception ex) 
            {
                return (ex.Message);
            }
        }


    }
}


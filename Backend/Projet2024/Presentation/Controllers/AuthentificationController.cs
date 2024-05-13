
using Domaine; 
using Microsoft.AspNetCore.Authentication; 
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Http; 
using Microsoft.AspNetCore.Mvc;
using Presentation.Authentification; 

namespace Presentation.Controllers
{
    
    [Route("api/[controller]")] 
    [ApiController] 
    //[Authorize]
    public class AuthentificationController : ControllerBase 
    {
        private readonly ILogger<AuthentificationController> _logger; 
        private readonly AuthentificationService _authenticationService; 

        // Constructeur du contrôleur
        public AuthentificationController(ILogger<AuthentificationController> logger,
                                        AuthentificationService authentificationService)
        {
            _logger = logger; 
            _authenticationService = authentificationService;
        }


        [HttpPost("Register")]
        //[Authorize (Roles ="1")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserDTO newUserDTO)
        {
            // Vérifiez si l'objet UserDTO est null ou si les propriétés obligatoires sont vides
            if (newUserDTO == null || string.IsNullOrEmpty(newUserDTO.UserName) || string.IsNullOrEmpty(newUserDTO.Password))
            {
                // Retournez une réponse indiquant que les paramètres ne peuvent pas être vides
                return BadRequest("UserName and Password cannot be null or empty");
            }

            try
            {
                // Appelez la méthode pour enregistrer un nouvel utilisateur
                await _authenticationService.RegisterUser(newUserDTO);
                return Ok("User registered successfully.");
            }
            catch (Exception ex)
            {
                // En cas d'erreur, retournez une réponse avec le message d'erreur
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }



        // Action pour se connecter (authentifier un utilisateur)
        [HttpPost("Login")] 
        [AllowAnonymous] 
        public string Login(string login, string password) // Déclare une action qui renvoie une chaîne
        {
            return _authenticationService.Login(login, password); // Appelle la méthode Login du service d'authentification avec le nom d'utilisateur et le mot de passe fournis, et renvoie le jeton d'authentification généré
        }


        // Endpoint pour modifier un utilisateur
        [HttpPut("{id}")]
        //[Authorize(Roles = "1")] 
        [AllowAnonymous]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDTO updatedUserDTO)
        {
            try
            {
                await _authenticationService.UpdateUser(id, updatedUserDTO);
                return Ok("User updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // Endpoint pour supprimer un utilisateur
        [HttpDelete("{id}")]
        [Authorize(Roles = "1")] 
        
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _authenticationService.DeleteUser(id);
                return Ok("User deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}

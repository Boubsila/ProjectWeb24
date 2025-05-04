
import { Component } from '@angular/core';
import { AuthService } from '../_shared/auth.service';
import { Router } from '@angular/router';
import { TokenService } from '../_shared/token.service';
import { jwtDecode } from 'jwt-decode';
import { AuthGuard } from '../_shared/auth.guard';



@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  form = {
    username: '',
    password: ''
  };

  constructor(private authService: AuthService,
    private tokenService: TokenService,
    private router: Router,
    private authGuard: AuthGuard
    
    
  ) { }





  login() {

    const loginUrl = this.authService.login(this.form.username, this.form.password).subscribe(
      (response: string) => {
        try {
          const jsonResponse = { token: response };
          let role = "";
          let name = "";
          console.log(jsonResponse);
          
          this.tokenService.saveToken(jsonResponse.token);
          try {
            role = this.decodeTokenAndGetRoleId(jsonResponse.token);
            name=this.authGuard.decodeTokenAndGetName(jsonResponse.token);
            switch (role) {
              case "1":
                this.router.navigate(['/admin']); 
                alert('Bienvenue vous êtes connecté en tant que : Admin');
                console.log('Role  : '+role);
                console.log('Name : '+ name);
                return true; 
              case "2":
                this.router.navigate(['/instructor']); 
                alert('Bienvenue vous êtes connecté en tant que : Professeur');
                return false;
              case "3":
                this.router.navigate(['/student']); // Rediriger vers la page de l'étudiant pour le rôle student
                alert('Bienvenue vous êtes connecté en tant que : Etudiant');
                return false;
              default:
                alert('Erreur lors de la connexion');
                return false;
            }
          } catch (error) {
            alert('Erreur lors du décodage du token :' + error);
            return false;
          }
        } catch (error) {
          alert('Erreur lors de la connexion :'+ error);
          return false;
        }
        alert('Erreur lors de la connexion');
      },
      (error) => {
        alert('Erreur lors de la connexion');
      }
    );




  }


  decodeTokenAndGetRoleId(token: string): string {
    try {
        // Décodage du token
        const decodedToken: any = jwtDecode(token);
        
        // Récupération de la valeur du roleId dans le token
        return decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    } catch (error) {
      alert('Erreur lors du décodage du token :'+error);
       return '';
    }
}


}
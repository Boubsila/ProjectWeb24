import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { jwtDecode } from 'jwt-decode';

@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivate {
    public roleId: string='';
    public Name: string='';
    public token: string | null = null;
    constructor(private router: Router) { }
   
    canActivate(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

        this.token = localStorage.getItem('token');

        if (!!this.token) {
            try {
                // Décodage du token
                const decodedToken: any = jwtDecode(this.token);

                // Vérification de l'expiration du token
                const tokenExpiration = decodedToken.exp * 1000; // Conversion en millisecondes
                const currentTime = Date.now();
                if (currentTime < tokenExpiration) {
                    // Le token n'a pas expiré, autoriser l'accès
                    return true;
                } else {
                    // Le token a expiré, rediriger vers la page de connexion
                    this.router.navigate(['/home']);
                    localStorage.clear();
                    location.reload();
                    alert('Session expirée');
                    return false;
                }
            } catch (error) {
                console.error('Erreur lors du décodage du token :', error);
                return false;
            }
        } else {
            // Aucun token trouvé, rediriger vers la page de connexion
            this.router.navigate(['/home']);
            return false;
        }
    }

    
    decodeTokenAndGetRoleId(token: string): string {
        try {
            // Décodage du token
            const decodedToken: any = jwtDecode(token);
            
            // Récupération de la valeur du roleId dans le token
       this.roleId= decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
      
            return this.roleId;
        } catch (error) {
            console.error('Erreur lors du décodage du token :', error);
            return '';
        }
    }


    decodeTokenAndGetName(token: string): string {
        try {
            // Décodage du token
            const decodedToken: any = jwtDecode(token);
            
            // Récupération de la valeur du roleId dans le token
       this.Name= decodedToken['sub'];
      
            return this.Name;
        } catch (error) {
            console.error('Erreur lors du décodage du token :', error);
            return '';
        }
    }
}








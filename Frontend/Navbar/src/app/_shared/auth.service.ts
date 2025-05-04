import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  login(username: string, password: string) {
    const url = 'https://localhost:7114/api/Authentification/Login';
    const loginUrl = `${url}?login=${username}&password=${password}`;

    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    return this.http.post(loginUrl, null,{ headers: headers, responseType: 'text' });
  }

  

}

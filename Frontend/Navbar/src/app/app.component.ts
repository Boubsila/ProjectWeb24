import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { AuthGuard } from './_shared/auth.guard';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'navbar2';
  courses: any = []; // Declare the 'courses' property
  constructor(private http: HttpClient,public authguard:AuthGuard) {
    http.get("https://localhost:7114/api/course").subscribe((data)=>this.courses=data);
  }

  
  
}





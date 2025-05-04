import { Injectable } from '@angular/core';
import { AuthGuard } from './auth.guard';
import { AuthService } from './auth.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class StudentsService {

  students: { userId: number; name: string; firstName: string; userName: string; }[] = [];

constructor(private http: HttpClient,public authGuard: AuthGuard,public auth :AuthService) {} 


private url = 'https://localhost:7114/api/Student';

private url2=this.auth.login;



ngOnInit() {

this.http.get(this.url).subscribe(
  res=>{
    this.students = res as { userId: number; name: string; firstName: string; userName: string; }[];
    console.log(res);
  },
)
}

getAllStudents() {
  return this.http.get(this.url);
}

delete(userId: number) {
  this.http.delete(this.url2 + userId.toString())
  .subscribe(
    res=>{
      this.ngOnInit();
      console.log(res);
    },
    err=>{
    
      alert('User Id not found : '+err); 
    }
  );
}
}

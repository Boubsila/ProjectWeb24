import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { StudentsService } from '../_shared/students.service';
import { AuthGuard } from '../_shared/auth.guard';

@Component({
  selector: 'app-student',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.css']
})
export class StudentComponent {
  
  students: any[] = [];
  roleId: string='';

  constructor(
    private router:Router,
    private studentService:StudentsService,
    private authGuard:AuthGuard) { }

  ngOnInit(): void {

    
    const token = localStorage.getItem('token');
    if (token) {
      this.roleId = this.authGuard.decodeTokenAndGetRoleId(token);
    }

    if (this.roleId === '1') {
      this.studentService.getAllStudents().subscribe(
        (res: any[]) => {
          this.students = res;
        },
        err => {
          console.log(err);
      });




  }   

  

  }

}

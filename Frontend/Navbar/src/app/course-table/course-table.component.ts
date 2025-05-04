import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CourseService } from '../_shared/course.service';
import { CourseModel } from '../_shared/course.model';
import { AuthGuard } from '../_shared/auth.guard';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-course-table',
  templateUrl: './course-table.component.html',
  styleUrls: ['./course-table.component.css']
})
export class CourseTableComponent {
courseId : number;
courses : any;
public student : any = [];
courseForm: FormGroup;


EditForm:boolean = false;
AddAssignment:boolean = false;
AddCourseForm:boolean = false;
DeleteCourse:boolean = false;
GetStudentByCourse:boolean = false;
AddNewCourse:boolean = false;




constructor ( 
  private http :HttpClient,
   private courseService:CourseService,
   private router:Router,
   public authGuard:AuthGuard,
   private fb:FormBuilder 
   
  ){
    this.courseForm = this.fb.group({
      courseId: [''],
      name: [''],
      description: [''],
      assignmentDeadline: ['']
    });
}


  ngOnInit (){
    this.courseService.Get().subscribe(
      (x:any)=>{this.courses=x;},
      error => alert(error)
    ) ;

    

    }


toggleAddNewCourse()
{
  this.AddNewCourse = !this.AddNewCourse;
  this.EditForm = false;
  this.GetStudentByCourse = false;
  this.AddAssignment = false;
}

  toggleAssignement(course: any) {
    this.EditForm = false;
    this.GetStudentByCourse = false;
    this.AddCourseForm = false;

    this.AddAssignment = true;
    this.courseForm.patchValue({
        courseId: course.courseId,
        assignmentDeadline :course.assignmentDeadline.substring(0, 16)
       
    });
}
  

  toggleEdit(course: any) {
    this.AddAssignment = false;
    this.GetStudentByCourse = false;
    this.AddNewCourse = false;
    this.EditForm = true;
    this.courseForm.patchValue({
      courseId: course.courseId,
      name: course.name,
      description: course.description,
      
      
    });
  
  }

  toggleDelete(){
    this.DeleteCourse= !this.DeleteCourse; 
    this.DeleteCourseById(this.courseId);
    //location.reload();
  }

  toggleStudentByCourse(courseId: number) {

   this.GetStudentByCourse= !this.GetStudentByCourse;

    if(this.GetStudentByCourse ){this.GetStudentByCourse = false;}
    this.GetStudentByCourse = true;
      this.student = this.courseService.GetStudentByCourse(courseId).subscribe(res=>{this.student=res;
       this.AddAssignment = false;
      this.EditForm = false;
    this.AddNewCourse = false;

    });
   
  }




  savePut(form: FormGroup) {
    let model = new CourseModel();
    model.description = form.value.description;
    model.name = form.value.name;
    model.courseId = form.value.courseId;
    
    
    this.courseService.Put(model,model.courseId).subscribe();

    alert("Course Updated Successfully");
   
   this.EditForm = false;
   location.reload();
  }

  
  

  saveAssignment(form: FormGroup) {
    const courseId = form.value.courseId;
    const assignmentDeadline = form.value.assignmentDeadline;
  
    
    const deadlineDate = new Date(assignmentDeadline);
  
    this.courseService.PostAssignment(courseId, deadlineDate).subscribe(
      (response: string) => {
        alert(response); 
        
        
        location.reload();
      },
      error => {
        if (error.message === 'Session expired') {
          this.router.navigate(['/login']); 
        } else {
          alert('An error occurred: ' + error.message);
        }
      }
    );
    
  }
  
 

  AddCourse(form: FormGroup): void {
    if (form.invalid) {
      return;
    }

    

    let model = new CourseModel();
    model.name = form.value.name;
    model.description = form.value.description;
    model.assignmentDeadline = form.value.assignmentDeadline;
    model.courseUsers = []; 
    model.courseNotes = []; 

    this.courseService.Post(model).subscribe(
      response => {
        alert("Course Added Successfully");
        form.reset(); 
        this.AddCourseForm = false;
      },
      error => {
        console.error("Error adding course", error);
        alert("Failed to add course. Please try again.");
      }
    );
    location.reload();
  }


DeleteCourseById(courseId:number){
  // courseId = this.courseForm.get('courseId')?.value;
  this.courseService.DeleteCourse(courseId).subscribe(
  
    response => {
      
      this.DeleteCourse = false;
      console.log(courseId);
    },
    error => {
      console.error("Error deleting course", error);
      alert("Failed to delete course. Please try again.");
    }
  );
  
  location.reload();
}
   
}
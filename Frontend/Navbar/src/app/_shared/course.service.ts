import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CourseModel, CourseModelAssignmentPut } from "./course.model";
import { StudentsService } from './students.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CourseService {
  private baseUrl = 'https://localhost:7114/api/Course';
  constructor(private http: HttpClient,student:StudentsService) { }

 

  Post(course: CourseModel) {
    return this.http.post("https://localhost:7114/api/Course/AddCourse", course);
  }
  

  Get(){
    return this.http.get<Array<CourseService>>("https://localhost:7114/api/Course");
   
   
  }


 GetByName(name:string){  
  return this.http.get<CourseModel>("https://localhost:7114/api/Course/"+name);
}

  GetById(courseId:number){
    return this.http.get<CourseModel>("https://localhost:7114/api/Course/"+courseId);
  }

  Put(course: CourseModelAssignmentPut, courseId:number){
    return this.http.put('https://localhost:7114/api/Course/update/'+ courseId, course);
  }



PostAssignment(courseId: number, assignmentDeadline: string | Date) {
  
  const deadline = new Date(assignmentDeadline);
  const formattedDeadline = encodeURIComponent(deadline.toISOString());
  const url = `https://localhost:7114/api/Course/create-assignment?courseId=${courseId}&deadline=${formattedDeadline}`;
  return this.http.post(url, {}, { responseType: 'text' });
}

GetStudentByCourse(courseId:number){
  return this.http.get<any>("https://localhost:7114/api/Course/student/"+courseId);
}
 
DeleteCourse(courseId: number) {
  return this.http.delete(`${this.baseUrl}/delete/${courseId}`);
}
}




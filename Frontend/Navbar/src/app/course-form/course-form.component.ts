import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators } from '@angular/forms';
import { FormControl } from '@angular/forms';
import { CourseModel } from '../_shared/course.model';
import { CourseService } from '../_shared/course.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-course-form',
  templateUrl: './course-form.component.html',
  styleUrls: ['./course-form.component.css']
})
export class CourseFormComponent implements OnInit {
  model: CourseModel;
  courseForm: FormGroup;
 

  constructor(
    private courseService: CourseService,
    private activatedRoute: ActivatedRoute,
    private route: Router
  ) {

    this.courseForm = new FormGroup({
      courseId: new FormControl('', Validators.required),
      name: new FormControl('', Validators.required),
      description: new FormControl('', Validators.required),
      assignmentDeadline: new FormControl('', Validators.required),

    });

  }



  ngOnInit() {
    this.activatedRoute.params.subscribe(params => {
      const courseId = params['courseId'];
      if (courseId) {
        this.courseService.GetById(courseId).subscribe(course => {
          if (course) {
            this.courseForm.patchValue({
              courseId: course.courseId,
              name: course.name,
              description: course.description,
              assignmentDeadline: new Date(course.assignmentDeadline).toISOString().slice(0, 16) //course.assignmentDeadline // Format datetime-local
            });
          }
        });
      }
    });
  }
  savePost(form: FormGroup) {
    let model = new CourseModel();
    model.description = form.value.description;
    model.name = form.value.name;
    model.courseId = form.value.courseId;
   // model.assignmentDeadline = form.value.assignment;
    this.courseService.Post(model).subscribe();

  }

  savePut(form: FormGroup) {
    let model = new CourseModel();
    model.description = form.value.description;
    model.name = form.value.name;
    model.courseId = form.value.courseId;
   // model.assignmentDeadline = form.value.assignmentDeadline;
    this.courseService.Put(model,model.courseId).subscribe();
   
    this.route.navigate(['/CourseTable']);
  }



}
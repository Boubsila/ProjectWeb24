import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CourseFormComponent } from './course-form/course-form.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './_shared/auth.guard';
import { InscriptionComponent } from './inscription/inscription.component';
import { AdminComponent } from './admin/admin.component';
import { StudentComponent } from './student/student.component';
import { CourseTableComponent } from './course-table/course-table.component';


const routes: Routes = [

  
  
  {path : "CourseForm/:userId", component : CourseFormComponent, canActivate: [AuthGuard]},
  {path : "CourseTable", component : CourseTableComponent, canActivate: [AuthGuard]},
  {path : "home",component : HomeComponent}, 
  {path : "course/:name",component : CourseFormComponent},
  {path : "login",component:LoginComponent},
  {path : "student",component:StudentComponent },
  {path : "inscription",component : InscriptionComponent},
  {path : "admin",component : AdminComponent  ,canActivate: [AuthGuard] },
  {path : 'courseForm/:courseId',component : CourseFormComponent},
  {path : "", component : HomeComponent},
  {path : '**', component : HomeComponent},

  



];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

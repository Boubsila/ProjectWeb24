import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { Navbar2Component } from './Navbar/navbar2.component';
import { CourseFormComponent } from './course-form/course-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { HomeComponent } from './home/home.component';
import { CourseTableComponent } from './course-table/course-table.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { LoginComponent } from './login/login.component';
import { InscriptionComponent } from './inscription/inscription.component';
import { AdminComponent } from './admin/admin.component';
import { TokenInterceptorProvider } from './_shared/token.interceptor';
import { StudentComponent } from './student/student.component';

@NgModule({
  declarations: [
    AppComponent,
    Navbar2Component,
    CourseFormComponent,
    HomeComponent,
    CourseTableComponent,
    LoginComponent,
    InscriptionComponent,
    AdminComponent,
    StudentComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgbModule,
    FormsModule
    
  ],
  providers: [TokenInterceptorProvider],
  bootstrap: [AppComponent]
})
export class AppModule { }

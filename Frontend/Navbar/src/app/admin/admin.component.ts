import { Component } from '@angular/core';
import { AuthGuard } from '../_shared/auth.guard';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent {
  constructor(public authGuard : AuthGuard,router:Router) { }


  ngOnInit(): void {

   
  }

}

import { Component } from '@angular/core';
import { AuthGuard } from '../_shared/auth.guard';
import { Router } from '@angular/router';
import { TokenService } from '../_shared/token.service';

@Component({
  selector: 'app-navbar2',
  templateUrl: './navbar2.component.html',
  styleUrls: ['./navbar2.component.css']
})
export class Navbar2Component {

  router:Router;
  //roleId: string='';
  constructor(router:Router ,public authGuard : AuthGuard, tokenservice : TokenService) { }

  ngOnInit(): void {
  }

  logout() {
    localStorage.clear();
    location.reload();
    this.router.navigate(['/login']);
  }

}

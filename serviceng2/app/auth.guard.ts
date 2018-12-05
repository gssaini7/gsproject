// auth.guard.ts
import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import { UserService } from './Services/user.service';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private user: UserService,private router: Router) {}

  canActivate() {
     
    if(!this.user.isLoggedIn())
    {
        window.location.href = "/home/account";

       //this.router.navigate(['/login']);
       return false;
    }
    
    return true;
  }
}
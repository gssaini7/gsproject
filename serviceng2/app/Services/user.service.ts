import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';

import { UserRegistration } from '../models/user.registration.interface';
//import { ConfigService } from '../Shared/config.service';

import { BaseService } from "./base.service";
import { Global } from '../Shared/global';

import { Observable } from 'rxjs/Observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';

//import * as _ from 'lodash';

// Add the RxJS Observable operators we need in this app.
//import '../../rxjs-operators';

@Injectable()

export class UserService extends BaseService {

  baseUrl: string = '';
  private loggedIn = this.gs_hasToken();
  //// Observable navItem source
  private _authNavStatusSource = new BehaviorSubject<boolean>(this.loggedIn);
  //// Observable navItem stream
  authNavStatus$ = this._authNavStatusSource.asObservable();

  

  //constructor(private http: Http, private configService: ConfigService) {
  constructor(private http: Http) {
    
      super();
    //this.loggedIn = !!localStorage.getItem('auth_token');
    // ?? not sure if this the best way to broadcast the status but seems to resolve issue on page refresh where auth status is lost in
    // header component resulting in authed user nav links disappearing despite the fact user is still logged in
    //this._authNavStatusSource.next(this.loggedIn);
    
    //this.baseUrl = configService.getApiURI();
    this.baseUrl = Global.BASE_USER_ENDPOINT;
  }

  register(email: string, password: string, ConfirmPassword: string): Observable<UserRegistration> {
      let body = JSON.stringify({ email, password, ConfirmPassword  });
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });

    return this.http.post("api/account/Register", body, options)
      .map(res => true)
        .catch(this.handleError);
  }  

    login(userName: string, password: string) {
    let headers = new Headers();
    headers.append('Content-Type', 'application/x-www-form-urlencoded');
    let bodyrequest = "userName=" + encodeURIComponent(userName) +
        "&password=" + encodeURIComponent(password) +
        "&grant_type=password";
    return this.http
      .post(
        //this.baseUrl + 'Account/login',
       'token',
        bodyrequest,{ headers }
      )
      .map(res => res.json())
      .map(res => {
          localStorage.setItem('auth_token', res.access_token);
        this.loggedIn = true;
        this._authNavStatusSource.next(true);
        //this.gs_isLoginSubject.next(true);
        return true;
      })
      .catch(this.handleError);
  }

    

    logout() {
        localStorage.removeItem('auth_token');
        localStorage.removeItem('dbcodeid');
        sessionStorage.removeItem('isMainAdmin');
        sessionStorage.removeItem('isLegitimate');

        this.loggedIn = false;
        this._authNavStatusSource.next(false);
        //this.gs_isLoginSubject.next(false);
    }

  isLoggedIn() {
    return this.loggedIn;
  }  
  private gs_hasToken(): boolean {
      return !!localStorage.getItem('auth_token');
  }

  
}


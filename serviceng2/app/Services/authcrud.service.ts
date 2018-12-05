import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { BaseService } from "./base.service";

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';

@Injectable()
export class AuthcrudService extends BaseService {
    constructor(private _http: Http) {
        super();
    }

    get(url: string): Observable<any> {
        //let headers = new Headers();
        //headers.append('Content-Type', 'application/json');
        //let authToken = localStorage.getItem('auth_token');
        //headers.append('Authorization', `Bearer ${authToken}`);
        return this._http.get(url, { headers: this.setheader() })
            .map((response: Response) => <any>response.json())
            // .do(data => console.log("All: " + JSON.stringify(data)))
            .catch(this.handleError);
    }

    post(url: string, model: any): Observable<any> {
        let body = JSON.stringify(model);
        //let headers = new Headers({ 'Content-Type': 'application/json' });
        //let authToken = localStorage.getItem('auth_token');
        //headers.append('Authorization', `Bearer ${authToken}`);
        let options = new RequestOptions({ headers: this.setheader() });
        return this._http.post(url, body, options)
            .map(res => true)
            .catch(this.handleError);
    }

    postwithresponse(url: string, model: any): Observable<any> {
        let body = JSON.stringify(model);
        let options = new RequestOptions({ headers: this.setheader() });
        return this._http.post(url, body, options)
            .map((response: Response) => response)
            .catch(this.handleError);
    }

    put(url: string, model: any): Observable<any> {
        let body = JSON.stringify(model);
        //let headers = new Headers({ 'Content-Type': 'application/json' });
        //let authToken = localStorage.getItem('auth_token');
        //headers.append('Authorization', `Bearer ${authToken}`);
        let options = new RequestOptions({ headers: this.setheader() });
        return this._http.put(url, body, options)
            .map(res => true)
            //.map((response: Response) => <any>response.json())
            .catch(this.handleError);
    }

    delete(url: string, id: string): Observable<any> {
        //let headers = new Headers({ 'Content-Type': 'application/json' });
        //let authToken = localStorage.getItem('auth_token');
        //headers.append('Authorization', `Bearer ${authToken}`);
        let options = new RequestOptions({ headers: this.setheader() });
        return this._http.delete(url, options)
            .map((response: Response) => <any>response.json())
            .catch(this.handleError);
    }

    setheader() {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let authToken = localStorage.getItem('auth_token');
        let dbcodeid = localStorage.getItem('dbcodeid');
        headers.append('Authorization', `Bearer ${authToken}`);
        headers.append('dbcodeid', dbcodeid);

        return headers;
    }

    //private handleError(error: Response) {
    //    console.error(error);
    //    return Observable.throw(error.json().error || 'Server error');
    //}
    unique(arr: any, prop: any) {

        return arr.map(function (e: any) { return e[prop]; }).filter(function (e: any, i: any, a: any) {
            return i === a.indexOf(e);
        });
    }

    removeDuplicates(arr: any, prop: any) {
        var newArray = [];
        var lookupObject = {};

        for (var i in arr) {
            lookupObject[arr[i][prop]] = arr[i];
        }

        for (i in lookupObject) {
            newArray.push(lookupObject[i]);
        }
        return newArray;
    }

}
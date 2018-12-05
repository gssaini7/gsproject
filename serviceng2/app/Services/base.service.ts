import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/throw';



export abstract class BaseService {
   
    constructor() {
    }

    protected handleError(error: any) {
       
        var applicationError = error.headers.get('Application-Error');

        

         
    // either applicationError in header or model error in body
        if (applicationError) {
           

      return Observable.throw(applicationError);
        }

        if (error.status === 401) {
        window.location.href = "/home/error";

        }   

    var modelStateErrors: string = '';
    var serverError = error.json();
 
    if (!serverError.type) {
        for (var key in serverError) {
           
            if (serverError[key]) {
                if (typeof serverError[key] === "string")
                    modelStateErrors += serverError[key] + '\n';
                if (typeof serverError[key] === "object") {
                    var newobj = serverError[key];
                    for (var innerkey in newobj) {
                        modelStateErrors += newobj[innerkey] + '\n';
                    }
                }
            }
        }
    }
    
    modelStateErrors = modelStateErrors = '' ? null : modelStateErrors;
    return Observable.throw(modelStateErrors || 'Server error');
  }
}
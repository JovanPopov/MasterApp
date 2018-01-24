import { Injectable, Inject} from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';

import { User } from '../_models/user';

@Injectable()
export class UserService {
    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string) { }
    

    create(user: User) {
        return this.http.post(this.baseUrl + '/api/account/register', user);
    }
}
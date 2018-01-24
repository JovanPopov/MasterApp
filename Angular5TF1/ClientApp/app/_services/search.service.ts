import { Injectable, Inject } from '@angular/core';
import { Http, Headers, RequestOptions, Response, URLSearchParams } from '@angular/http';

import { User } from '../_models/user';

@Injectable()
export class SearchService {
    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string) { }


    search(title: string) {
       
        return this.http.get(this.baseUrl + 'api/search/search', { search: this.params(title), headers: this.jwt() });
    }

    private params(title: string) {
        let params: URLSearchParams = new URLSearchParams();
        params.set('data', title);
        return params;
    }

    private jwt() {
        // create authorization header with jwt token
        const userStorage = localStorage.getItem('currentUser');
        let currentUser = userStorage !== null ? JSON.parse(userStorage) : null;
        if (currentUser && currentUser.token) {
            let headers = new Headers({ 'Authorization': 'Bearer ' + currentUser.token });
            return headers;
        }
    }

   
}
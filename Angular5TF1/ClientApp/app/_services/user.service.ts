import { Injectable, Inject} from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';

import { User } from '../_models/user';

@Injectable()
export class UserService {
    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string) { }

    getAll() {
        return this.http.get(this.baseUrl + '/users', this.jwt()).map((response: Response) => response.json());
    }

    getById(id: number) {
        return this.http.get(this.baseUrl + '/users/' + id, this.jwt()).map((response: Response) => response.json());
    }

    create(user: User) {
        return this.http.post(this.baseUrl + '/api/account/register', user, this.jwt());
    }

    update(user: User) {
        return this.http.put(this.baseUrl + '/users/' + user.id, user, this.jwt());
    }

    delete(id: number) {
        return this.http.delete(this.baseUrl + '/users/' + id, this.jwt());
    }

    // private helper methods

    private jwt() {
        // create authorization header with jwt token
        const userStorage = localStorage.getItem('currentUser');
        let currentUser = userStorage !== null ? JSON.parse(userStorage) : null;
        if (currentUser && currentUser.token) {
            let headers = new Headers({ 'Authorization': 'Bearer ' + currentUser.token });
            return new RequestOptions({ headers: headers });
        }
    }
}
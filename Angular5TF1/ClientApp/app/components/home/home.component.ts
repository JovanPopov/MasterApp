import { Component, Inject } from '@angular/core';
import { Http, URLSearchParams, Headers } from '@angular/http';
import { Router } from '@angular/router';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent {

    searchResult: SearchResult;
    key: string = 'name'; //set default
    reverse: boolean = false;
    p1: number = 1;
    p2: number = 1;
    loading = false;
   
   

    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string, private router: Router) { }

    ngOnInit() {
        this.searchResult = {
            wiki: "",
            allEvents: [] as Event[],
            tweets: [] as Tweet[]
        };
    }


    search(title: string) {
        if (title){
            this.loading = true;
            let params: URLSearchParams = new URLSearchParams();
            params.set('data', title);
            this.http.get(this.baseUrl + 'api/search/search', { search: params, headers : this.jwt() })
                .subscribe(result => {
                    this.searchResult = result.json() as SearchResult;
                    this.loading = false;
                }, error => {
                    console.error(error);
                    this.loading = false;
                    if (error.status == 401) {
                        this.router.navigate(['login']);
                    }
                }
            );
        }
    }

    sort(key: string) {
        this.key = key;
        this.reverse = !this.reverse;
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

interface SearchResult {
    wiki: string;
    allEvents: Event[];
    tweets: Tweet[];
}

interface Event {
    name: string;
    time: string;
    location: string;
    venue: string;
    url: string;
}

interface Tweet {
    Author: string;
    Date: string;
    Text: string;
    Url: string;
}
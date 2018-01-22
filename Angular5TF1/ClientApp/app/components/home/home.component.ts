import { Component, Inject } from '@angular/core';
import { Http, URLSearchParams } from '@angular/http';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent {

    searchResult: SearchResult;
    key: string = 'name'; //set default
    reverse: boolean = false;
    p: number = 1;
    loading = false;
   
   

    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string) { }

    ngOnInit() {
        this.searchResult = {
            wiki: "",
            allEvents : [] as Event[]
        };
    }


    search(title: string) {
        this.loading = true;
        let params: URLSearchParams = new URLSearchParams();
        params.set('data', title);
        this.http.get(this.baseUrl + 'api/search/search', { search: params })
            .subscribe(result => {
                this.searchResult = result.json() as SearchResult;
                this.loading = false;
            }, error => {
                console.error(error);
                this.loading = false;
            }
        );

    }

    sort(key: string) {
        this.key = key;
        this.reverse = !this.reverse;
    }



}

interface SearchResult {
    wiki: string;
    allEvents: Event[];
}

interface Event {
    name: string;
    time: string;
    location: string;
    venue: string;
    url: string;
}
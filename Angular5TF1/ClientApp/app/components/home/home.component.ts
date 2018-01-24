import { Component, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { SearchResult } from '../../_models/SearchResult';
import { Tweet } from '../../_models/Tweet';
import { Event } from '../../_models/Event';
import { SearchService } from '../../_services/search.service';

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
   
   

    constructor(@Inject('BASE_URL') private baseUrl: string, private router: Router, private searchService: SearchService) { }

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

            this.searchService.search(title)
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

    //sort(key: string) {
    //    this.key = key;
    //    this.reverse = !this.reverse;
    //}

  

}
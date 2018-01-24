import { Tweet } from './Tweet';
import { Event } from './Event';

export class SearchResult {
    wiki: string;
    allEvents: Event[];
    tweets: Tweet[];
}
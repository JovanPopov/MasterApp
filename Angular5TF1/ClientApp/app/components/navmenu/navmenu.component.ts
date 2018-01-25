import { Component } from '@angular/core';

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent {

    userLogedIn() {
        if (typeof window !== 'undefined' && localStorage.getItem('currentUser')) {
            // logged in so return true
            return true;
        } else {
            return false;
        }
    }

}

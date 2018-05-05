import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { AuthenticationService } from '../_services/authentication.service';
import { AlertService } from '../_services/alert.service';
import { UserService } from '../_services/user.service';

@Component({
    templateUrl: 'register.component.html'
})

export class RegisterComponent {
    model: any = {};
    loading = false;

    constructor(
        private router: Router,
        private userService: UserService,
        private authenticationService: AuthenticationService,
        private alertService: AlertService) { }

    register() {
        this.loading = true;
        this.userService.create(this.model)
            .subscribe(
                data => {
                    this.alertService.success('Registration successful', true);
                    this.authenticationService.login(this.model.username, this.model.password)
                        .subscribe(
                        data => {
                            this.router.navigate(['/search']);
                        },
                        error => {
                            this.loading = false;
                            this.alertService.error('Error logging in');
                            this.router.navigate(['/login']);
                            
                        });
                  
                },
                error => {
                    this.alertService.error(error._body);
                    this.loading = false;
                });
    }
}

import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticateService } from '../services/authenticate.service';
import { FormsModule, NgForm } from '@angular/forms'; 
import { TogglePasswordDirective } from '../directives/toggle-password.directive';
import { CommonModule } from '@angular/common';
import { ToastrService } from 'ngx-toastr'; 
import { LoaderService } from '../services/loader.service';
@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, TogglePasswordDirective],
  templateUrl: './login.component.html',
})
export class LoginComponent implements  OnInit {
  username: string = '';
  password: string = '';
  errorMessage: string = '';

  constructor(
    private router: Router, 
    private authenticateService: AuthenticateService,
    private toastr: ToastrService,
    private loaderService: LoaderService
  ) { }
  ngOnInit() {
    this.loaderService.showLoader();
   console.log("Hii")
   this.loaderService.hideLoader();
  }
  onLogin(form: NgForm) {
    if (form.invalid) {
      this.toastr.warning('Please fill in all fields.');
      return;
    }

    const authentication = {
      username: this.username,
      password: this.password,
    };

    this.authenticateService.authenticateUser(authentication).subscribe(
      (response) => {
        this.toastr.success('Authentication Success');
        if (response && response.jwtToken) {
          sessionStorage.setItem('AccessToken', response.jwtToken);
          this.router.navigate(['/landing']);
        } else {
          this.errorMessage = response.statusMessage || 'Login failed. Please try again.';
          this.toastr.error(this.errorMessage);
        }
      },
      (error) => {
        this.toastr.error('An error occurred during login. Please try again.');
        this.errorMessage = 'An error occurred during login. Please try again.';
      }
    );
  }
}

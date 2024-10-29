import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticateService } from '../services/authenticate.service';
import { FormsModule } from '@angular/forms'; 
import { TogglePasswordDirective } from '../directives/toggle-password.directive';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, TogglePasswordDirective],
  templateUrl: './login.component.html',
})
export class LoginComponent {
  username: string = '';
  password: string = '';
  errorMessage: string = '';

  constructor(private router: Router, private authenticateService: AuthenticateService) { }

  onLogin() {
    const authentication = {
      username: this.username,
      password: this.password,
    };

    this.authenticateService.authenticateUser(authentication).subscribe(
      (response) => {
        if (response && response.jwtToken) {
          sessionStorage.setItem('AccessToken', response.jwtToken);
          this.router.navigate(['/landing']);
        } else {
          this.errorMessage = response.statusMessage || 'Login failed. Please try again.';
        }
      },
      (error) => {
        this.errorMessage = 'An error occurred during login. Please try again.';
      }
    );
  }
}

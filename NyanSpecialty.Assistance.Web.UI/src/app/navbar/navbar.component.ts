import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent {
  userName = 'John Doe';
  userRole = 'UX Designer';
  isUserMenuVisible = false;
  isSubmenuVisible: { [key: string]: boolean } = {};

  constructor(private router: Router) {}

  toggleSubmenu(menu: string) {
    this.isSubmenuVisible[menu] = !this.isSubmenuVisible[menu];
  }

  logout() {
    // Add logout logic here
    console.log('User logged out');
    this.router.navigate(['/login']);
  }
}

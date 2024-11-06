import { Component, OnInit } from '@angular/core';
import { AuthenticateService } from '../../services/authenticate.service';
import { Router } from '@angular/router';
import { ApplicationUser } from '../../models/appUser';



@Component({
  selector: 'site-header',
  standalone: true,
  imports: [],
  templateUrl: './site-header.component.html',
  styleUrl: './site-header.component.css'
})
export class SiteHeaderComponent implements OnInit {
  applicationUser: ApplicationUser = {} as ApplicationUser;

  constructor(private authenticateSerice: AuthenticateService, private router: Router) {

  }
  ngOnInit(): void {
    const appuser = sessionStorage.getItem("ApplicationUser");
    if (appuser) {
      this.applicationUser = JSON.parse(appuser);
    }

  }
  logout() {
    this.authenticateSerice.logout();
    this.router.navigate(['/login']);
  }
}

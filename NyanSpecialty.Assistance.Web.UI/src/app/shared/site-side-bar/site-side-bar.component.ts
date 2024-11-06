import { Component, OnInit } from '@angular/core';
import { ApplicationUser } from '../../models/appUser';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'site-side-bar',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './site-side-bar.component.html',
  styleUrl: './site-side-bar.component.css'
})
export class SiteSideBarComponent implements OnInit {
  applicationUser: ApplicationUser = {} as ApplicationUser;
  isPageLayoutsOpen = false;
  isBasicOpen = false;

  constructor() { }
  ngOnInit(): void {
    const appuser = sessionStorage.getItem("ApplicationUser");
    if (appuser) {
      this.applicationUser = JSON.parse(appuser);
    }
  }
 
  togglePageLayouts() {
    this.isPageLayoutsOpen = !this.isPageLayoutsOpen;
  }

  toggleBasic() {
    this.isBasicOpen = !this.isBasicOpen;
  }
}

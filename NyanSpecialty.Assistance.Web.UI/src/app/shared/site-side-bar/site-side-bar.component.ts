import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApplicationUser } from '../../models/appUser';
import { Router } from '@angular/router';

@Component({
  selector: 'site-side-bar',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './site-side-bar.component.html',
  styleUrls: ['./site-side-bar.component.css']
})
export class SiteSideBarComponent implements OnInit {
  applicationUser: ApplicationUser = {} as ApplicationUser;

  constructor(private router: Router) { }

  ngOnInit(): void {
    const appuser = sessionStorage.getItem("ApplicationUser");
    if (appuser) {
      this.applicationUser = JSON.parse(appuser);
    }

  }
  policytypeClick() {
    this.router.navigate(['/policytype']);
  }
  servicetypeClick(){
    this.router.navigate(['/servicetype']);
  }
  landingClick(){
    this.router.navigate(['/landing']);
  }
}
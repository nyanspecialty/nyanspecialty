// app.component.ts
import { Component, OnInit, OnDestroy } from '@angular/core';
import { RouterOutlet, Router } from '@angular/router';
import { Observable, Subject } from 'rxjs';
import { CommonModule } from '@angular/common';
import { SpinnerComponent } from './spinner/spinner.component';
import { LoaderService } from './services/loader.service';
import { SiteHeaderComponent } from './shared/site-header/site-header.component';
import { SiteSideBarComponent } from './shared/site-side-bar/site-side-bar.component';
import { AuthenticateService } from './services/authenticate.service';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, SpinnerComponent, CommonModule, SiteHeaderComponent, SiteSideBarComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, OnDestroy {
  title = 'nyan specialty';
  loading$: Observable<boolean>;
  isLoggedIn: boolean = false;
  private unsubscribe$ = new Subject<void>(); 

  constructor(
    private loadingService: LoaderService, 
    private authService: AuthenticateService,
    private router: Router 
  ) {
    this.loading$ = this.loadingService.loading$;
  }

  ngOnInit() {
    this.authService.isLoggedIn$
      .pipe(takeUntil(this.unsubscribe$)) 
      .subscribe(loggedIn => {
        this.isLoggedIn = loggedIn;
      });

    // Check if the user is authenticated
    if (!this.authService.isAuthenticated()) {
      this.router.navigate(['/login']); // Redirect to login if not authenticated
    }

    this.loadingService.showLoader();
    setTimeout(() => {
      this.loadingService.hideLoader();
    }, 10000);
  }

  ngOnDestroy() {
    this.unsubscribe$.next(); 
    this.unsubscribe$.complete(); 
  }
}
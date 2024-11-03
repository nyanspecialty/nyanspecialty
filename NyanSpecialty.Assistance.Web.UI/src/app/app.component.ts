// app.component.ts
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common'; // Import CommonModule
import { SpinnerComponent } from './spinner/spinner.component';
import { LoaderService } from './services/loader.service';
import { SiteHeaderComponent } from './shared/site-header/site-header.component';
import { SiteSideBarComponent } from './shared/site-side-bar/site-side-bar.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, SpinnerComponent, CommonModule,SiteHeaderComponent,SiteSideBarComponent], 
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'nyan specialty';
  loading$: Observable<boolean>;

  constructor(private loadingService: LoaderService) {
    this.loading$ = this.loadingService.loading$;
  }
}
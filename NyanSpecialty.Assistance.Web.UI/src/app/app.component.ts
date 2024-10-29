// app.component.ts
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common'; // Import CommonModule
import { SpinnerComponent } from './spinner/spinner.component';
import { LoaderService } from './services/loader.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, SpinnerComponent, CommonModule], // Add CommonModule here
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
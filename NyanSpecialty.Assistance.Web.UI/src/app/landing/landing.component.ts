import { AfterViewInit, Component } from '@angular/core';
import { LoaderService } from '../services/loader.service';

@Component({
  selector: 'app-landing',
  standalone: true,
  imports: [],
  templateUrl: './landing.component.html',
  styleUrl: './landing.component.css'
})
export class LandingComponent implements AfterViewInit {
  constructor(private loader: LoaderService) { }
  ngAfterViewInit(): void {
    this.loader.hideLoader();
  }
}

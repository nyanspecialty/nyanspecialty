import { bootstrapApplication } from '@angular/platform-browser';
import { provideRouter } from '@angular/router';
import { AppComponent } from './app/app.component';
import { provideHttpClient } from '@angular/common/http';
import { routes } from './app/app.routes';
import { provideAnimations } from '@angular/platform-browser/animations'; // Import provideAnimations
import { provideToastr, GlobalConfig } from 'ngx-toastr';

// Define the toastr configuration
const toastrConfig: Partial<GlobalConfig> = {
  positionClass: 'toast-top-right',
  timeOut: 3000,
  preventDuplicates: true,
};

bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(routes),
    provideHttpClient(),
    provideAnimations(), 
    provideToastr(toastrConfig)
  ]
})
  .catch(err => console.error(err));

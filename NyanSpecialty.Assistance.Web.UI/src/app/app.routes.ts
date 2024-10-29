// app.routes.ts
import { Routes } from '@angular/router';
import { LandingComponent } from './landing/landing.component';
import { LoginComponent } from './login/login.component';

export const routes: Routes = [
  { path: '', component: LoginComponent }, 
  { path: 'landing', component: LandingComponent},
  { path: '**', redirectTo: '' } 
];

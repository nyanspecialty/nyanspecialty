// app.routes.ts
import { Routes } from '@angular/router';
import { LandingComponent } from './landing/landing.component';
import { LoginComponent } from './login/login.component';
import { PolicytypeComponent } from './policytype/policytype.component';
import { ServicetypeComponent } from './servicetype/servicetype.component';

export const routes: Routes = [
  { path: '', component: LoginComponent }, 
  { path: 'landing', component: LandingComponent},
  { path: 'policytype', component: PolicytypeComponent},
  { path: 'servicetype', component: ServicetypeComponent},
  { path: '**', redirectTo: '' } 
];

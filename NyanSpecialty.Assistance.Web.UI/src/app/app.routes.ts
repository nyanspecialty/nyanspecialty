// app.routes.ts
import { Routes } from '@angular/router';
import { LandingComponent } from './landing/landing.component';
import { LoginComponent } from './login/login.component';
import { PolicytypeComponent } from './policytype/policytype.component';
import { ServicetypeComponent } from './servicetype/servicetype.component';
import { AuthGuard } from './guards/auth.guard';

export const routes: Routes = [
  { path: '', component: LoginComponent }, 
  { path: 'landing', component: LandingComponent,canActivate:[AuthGuard]},
  { path: 'policytype', component: PolicytypeComponent,canActivate:[AuthGuard]},
  { path: 'servicetype', component: ServicetypeComponent,canActivate:[AuthGuard]},
  { path: '**', redirectTo: '' } 
];

// app.routes.ts
import { Routes } from '@angular/router';
import { LandingComponent } from './landing/landing.component';
import { LoginComponent } from './login/login.component';
import { PolicytypeComponent } from './policytype/policytype.component';
import { ServicetypeComponent } from './servicetype/servicetype.component';
import { AuthGuard } from './guards/auth.guard';
import { PolicyCategoryComponent } from './policycategory/policycategory.component';
import { WorkFlowComponent } from './work-flow/work-flow.component';
import { VehiclesizeComponent } from './vehiclesize/vehiclesize.component';
import { VehicleClassComponent } from './vehicleclass/vehicleclass.component';
import { UserComponent } from './user/user.component';
import { RoleComponent } from './role/role.component';
import { ServiceProviderComponent } from './serviceprovider/serviceprovider.component';
import { CustomerComponent } from './customer/customer.component';
import { InsurancePolicyComponent } from './insurancepolicy/insurancepolicy.component';
import { CaselistComponent } from './casemanagement/caselist.component';

export const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'landing', component: LandingComponent, canActivate: [AuthGuard] },
  { path: 'policytype', component: PolicytypeComponent, canActivate: [AuthGuard] },
  { path: 'servicetype', component: ServicetypeComponent, canActivate: [AuthGuard] },
  { path: 'policycategory', component: PolicyCategoryComponent, canActivate: [AuthGuard] },
  { path: 'workflow', component: WorkFlowComponent, canActivate: [AuthGuard] },
  { path: 'vehiclesize', component: VehiclesizeComponent, canActivate: [AuthGuard] },
  { path: 'vehicleclass', component: VehicleClassComponent, canActivate: [AuthGuard] },
  { path: 'serviceprovider', component: ServiceProviderComponent, canActivate: [AuthGuard] },
  { path: 'users', component: UserComponent, canActivate: [AuthGuard] },
  { path: 'roles', component: RoleComponent, canActivate: [AuthGuard] },
  { path: 'customer', component: CustomerComponent, canActivate: [AuthGuard] },
  { path: 'insurencepolicy', component: InsurancePolicyComponent, canActivate: [AuthGuard] },
  { path: 'insurencecase', component: CaselistComponent, canActivate: [AuthGuard] },
  { path: '**', redirectTo: '' }
];

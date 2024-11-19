import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { LandingComponent } from './landing/landing.component';
import { AuthenticateService } from './services/authenticate.service';
import { RepositoryFactory } from './factory/repositoryfactory.service';
import { TokenService } from './factory/token.service';
import { TogglePasswordDirective } from './directives/toggle-password.directive';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { LoaderService } from './services/loader.service';
import { SpinnerComponent } from './spinner/spinner.component';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { PolicyTypeService } from './services/policytype.service';
import { CommonModule } from '@angular/common';
import { GridHeaderComponent } from './shared/grid-header/grid-header.component';
import { SiteHeaderComponent } from './shared/site-header/site-header.component';
import { SiteSideBarComponent } from './shared/site-side-bar/site-side-bar.component';
import { AddEditPolicyTypeComponent } from './policytype/addeditpolicytype.component';
import { DeleteconfirmComponent } from './shared/deleteconfirm/deleteconfirm.component';
import { ServiceTypeService } from './services/servicetype.service';
import { AddEditServiceTypeComponent } from './servicetype/add-edit-servicetype.component';
import { PolicyCategoryService } from './services/policycategory.service';
import { AddEditPolicyCategoryComponent } from './policycategory/add-edit-policycategory.component';
import { CommonPropsService } from './services/common.service';
import { AddEditWorkflowComponent } from './work-flow/add-edit-workflow.component';
import { VehicleClassService } from './services/vehicleclass.service';
import { AddEditVehicleClassComponent } from './vehicleclass/add-edit-vehicleclass.component';
import { UserService } from './services/user.service';
import { CustomerService } from './services/customers.service';
import { InsurancePolicyService } from './services/insurancepolicy.service';
import { RoleService } from './services/role.service';
import { AddEditRoleComponent } from './role/add-edit-role.component';
import { ServiceProviderService } from './services/serviceprovider.service';
import { AddEditPolicyComponent } from './insurancepolicy/add-edit-policy.component';

@NgModule({
  declarations: [
    AppComponent,
    LandingComponent,
    TogglePasswordDirective,
    SpinnerComponent,
    GridHeaderComponent,
    SiteHeaderComponent,
    SiteSideBarComponent,
    AddEditPolicyTypeComponent,
    DeleteconfirmComponent,
    AddEditServiceTypeComponent,
    AddEditPolicyCategoryComponent,
    AddEditWorkflowComponent,
    AddEditVehicleClassComponent,
    AddEditRoleComponent,
    AddEditPolicyComponent
  ],
  imports: [
    BrowserModule,
    NgxDatatableModule,
    FormsModule,
    CommonModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      positionClass: 'toast-top-right',
      timeOut: 3000,
      preventDuplicates: true,
    }),
    // PolicyTypeModule
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  providers: [AuthenticateService, RepositoryFactory, TokenService, LoaderService,
    ServiceTypeService, PolicyCategoryService, CommonPropsService, PolicyTypeService,
    VehicleClassService, CustomerService, InsurancePolicyService, RoleService, UserService,
    ServiceProviderService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

import { NgModule } from '@angular/core';
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

@NgModule({
  declarations: [
    AppComponent,
    LandingComponent,
    TogglePasswordDirective ,
    SpinnerComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule, 
    ToastrModule.forRoot({ 
      positionClass: 'toast-top-right', 
      timeOut: 3000, 
      preventDuplicates: true,
    }),
  ],
  providers: [AuthenticateService, RepositoryFactory, TokenService,LoaderService],
  bootstrap: [AppComponent]
})
export class AppModule { }

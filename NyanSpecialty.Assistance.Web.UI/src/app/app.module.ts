import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { LandingComponent } from './landing/landing.component';
import { LoginComponent } from './login/login.component';
import { AuthenticateService } from './services/authenticate.service';
import { RepositoryFactory } from './factory/repositoryfactory.service';
import { TokenService } from './factory/token.service';
import { TogglePasswordDirective } from './directives/toggle-password.directive';

@NgModule({
  declarations: [
    AppComponent,
    LandingComponent,
    LoginComponent,
    TogglePasswordDirective
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [AuthenticateService, RepositoryFactory, TokenService],
  bootstrap: [AppComponent]
})
export class AppModule { }

import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {HTTP_INTERCEPTORS, HttpClientModule, provideHttpClient, withInterceptors} from '@angular/common/http';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { BrowserAnimationsModule, provideAnimations } from '@angular/platform-browser/animations';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { HomeComponent } from './home/home.component'
import { RegisterComponent } from './Accounts/register/register.component';
import { LoginComponent } from './Accounts/login/login.component';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import { TextInputComponent } from './_forms/text-input/text-input.component';
import {JwtInterceptor} from "./_interceptors/jwt.interceptor";
import {ToastrModule} from "ngx-toastr";
import {NgxSpinnerModule} from "ngx-spinner";
import {loadingInterceptor} from "./_interceptors/loading.interceptor";

@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent,
    HomeComponent,
    RegisterComponent,
    LoginComponent,
    TextInputComponent
  ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        TooltipModule.forRoot(),
        HttpClientModule,
        BrowserAnimationsModule,
        BsDropdownModule.forRoot(),
        ToastrModule.forRoot({
          positionClass: 'toast-bottom-right',
        }),
        NgxSpinnerModule.forRoot({
          type: 'ball-spin-clockwise-fade',
        }),
        FormsModule,
        ReactiveFormsModule
    ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
    provideHttpClient(withInterceptors([loadingInterceptor])),
    provideAnimations()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

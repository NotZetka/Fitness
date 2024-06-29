import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { BrowserAnimationsModule, provideAnimations } from '@angular/platform-browser/animations';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { HomeComponent } from './home/home.component'
import { RegisterComponent } from './Accounts/register/register.component';
import { LoginComponent } from './Accounts/login/login.component';
import {FormsModule} from "@angular/forms";

@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent,
    HomeComponent,
    RegisterComponent,
    LoginComponent
  ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        TooltipModule.forRoot(),
        HttpClientModule,
        BrowserAnimationsModule,
        BsDropdownModule.forRoot(),
        FormsModule
    ],
  providers: [provideAnimations()],
  bootstrap: [AppComponent]
})
export class AppModule { }

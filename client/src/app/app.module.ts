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
import { RegisterMemberComponent } from './Accounts/register-member/register-member.component';
import { LoginComponent } from './Accounts/login/login.component';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import { TextInputComponent } from './_forms/text-input/text-input.component';
import {JwtInterceptor} from "./_interceptors/jwt.interceptor";
import {ToastrModule} from "ngx-toastr";
import {NgxSpinnerModule} from "ngx-spinner";
import {loadingInterceptor} from "./_interceptors/loading.interceptor";
import { PlansMainComponent } from './Plans/plans-main/plans-main.component';
import { PlansCreateComponent } from './Plans/plans-create/plans-create.component';
import { PlansMarketComponent } from './Plans/plans-market/plans-market.component';
import { PlansListComponent } from './Plans/plans-list/plans-list.component';
import { PlansDetailComponent } from './Plans/plans-detail/plans-detail.component';
import { AccountsListComponent } from './Accounts/list/accounts-list.component';
import { UserCardComponent } from './Accounts/user-card/user-card.component';
import { UserDetailComponent } from './Accounts/user-detail/user-detail.component';
import { MessagesChatComponent } from './Messages/chat/messages-chat.component';
import { BodyMainComponent } from './Body/body-main/body-main.component';
import { BodyRecordDetailsComponent } from './Body/body-record-details/body-record-details.component';
import { RegisterTrainerComponent } from './Accounts/register-trainer/register-trainer.component';
import { RegisterComponent } from './Accounts/register/register.component';

@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent,
    HomeComponent,
    RegisterMemberComponent,
    LoginComponent,
    TextInputComponent,
    PlansMainComponent,
    PlansCreateComponent,
    PlansMarketComponent,
    PlansListComponent,
    PlansDetailComponent,
    AccountsListComponent,
    UserCardComponent,
    UserDetailComponent,
    MessagesChatComponent,
    BodyMainComponent,
    BodyRecordDetailsComponent,
    RegisterTrainerComponent,
    RegisterComponent
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

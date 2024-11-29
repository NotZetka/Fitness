import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './Accounts/login/login.component';
import {PlansMainComponent} from "./Plans/plans-main/plans-main.component";
import {PlansListComponent} from "./Plans/plans-list/plans-list.component";
import {PlansCreateComponent} from "./Plans/plans-create/plans-create.component";
import {PlansMarketComponent} from "./Plans/plans-market/plans-market.component";
import {PlansDetailComponent} from "./Plans/plans-detail/plans-detail.component";
import {AccountsListComponent} from "./Accounts/list/accounts-list.component";
import {UserDetailComponent} from "./Accounts/user-detail/user-detail.component";
import {MessagesChatComponent} from "./Messages/chat/messages-chat.component";
import {BodyMainComponent} from "./Body/body-main/body-main.component";
import {BodyRecordDetailsComponent} from "./Body/body-record-details/body-record-details.component";
import {RegisterComponent} from "./Accounts/register/register.component";
import {PlansCreateTemplateComponent} from "./Plans/plans-create-template/plans-create-template.component";
import {YourTemplatesComponent} from "./Plans/your-templates/your-templates.component";
import {EditTemplateComponent} from "./Plans/edit-template/edit-template.component";

const routes: Routes = [
  {path:'', component: HomeComponent},
  {path:'register', component: RegisterComponent},
  {path:'login', component: LoginComponent},
  {path:'accounts/list', component: AccountsListComponent},
  {path:'accounts/:id', component: UserDetailComponent},
  {path:'messages/:userId', component: MessagesChatComponent},
  {path:'plans', component: PlansMainComponent},
  {path:'plans/list', component: PlansListComponent},
  {path:'plans/list/:id', component: PlansDetailComponent},
  {path:'plans/create', component: PlansCreateComponent},
  {path:'plans/create-template', component: PlansCreateTemplateComponent},
  {path:'plans/your-templates', component: YourTemplatesComponent},
  {path:'plans/edit', component: EditTemplateComponent},
  {path:'plans/market', component: PlansMarketComponent},
  {path:'body', component: BodyMainComponent},
  { path: 'body-record-details', component: BodyRecordDetailsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

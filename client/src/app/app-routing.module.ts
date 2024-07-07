import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './Accounts/register/register.component';
import { LoginComponent } from './Accounts/login/login.component';
import {PlansMainComponent} from "./Plans/plans-main/plans-main.component";
import {PlansListComponent} from "./Plans/plans-list/plans-list.component";
import {PlansCreateComponent} from "./Plans/plans-create/plans-create.component";
import {PlansMarketComponent} from "./Plans/plans-market/plans-market.component";
import {PlansDetailComponent} from "./Plans/plans-detail/plans-detail.component";

const routes: Routes = [
  {path:'', component: HomeComponent},
  {path:'register', component: RegisterComponent},
  {path:'login', component: LoginComponent},
  {path:'plans', component: PlansMainComponent},
  {path:'plans/list', component: PlansListComponent},
  {path:'plans/list/:id', component: PlansDetailComponent},
  {path:'plans/create', component: PlansCreateComponent},
  {path:'plans/market', component: PlansMarketComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

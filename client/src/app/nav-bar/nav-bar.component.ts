import {Component} from '@angular/core';
import { BsDropdownConfig } from 'ngx-bootstrap/dropdown';
import {AccountService} from "../_services/account.service";

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.css',
  providers: [{ provide: BsDropdownConfig, useValue: { isAnimated: true, autoClose: true } }]
})
export class NavBarComponent {

  constructor(public accountService: AccountService) {}

  logout(){
    this.accountService.logout()
  }
}

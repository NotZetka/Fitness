import { Component } from '@angular/core';
import {AccountService} from "../../_services/account.service";

@Component({
  selector: 'app-plans-main',
  templateUrl: './plans-main.component.html',
  styleUrl: './plans-main.component.css'
})
export class PlansMainComponent {

  constructor(public accountService: AccountService) {
  }
}

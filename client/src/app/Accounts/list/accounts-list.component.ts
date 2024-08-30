import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {UserDto} from "../_models/userDto";
import {GetAccountsListResponse} from "../_models/GetAccountsListResponse";
import {environment} from "../../../environments/environment";

@Component({
  selector: 'app-list',
  templateUrl: './accounts-list.component.html',
  styleUrl: './accounts-list.component.css'
})
export class AccountsListComponent implements OnInit {
  baseUrl = environment.baseUrl;
  users : UserDto[] = new Array<UserDto>()

  constructor(private http: HttpClient) {
  }

  ngOnInit(): void {
    this.http.get<GetAccountsListResponse>(this.baseUrl+'Accounts/List/').subscribe({
      next: response => {
        this.users = response.items;
      }
    })
  }
}

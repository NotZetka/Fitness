import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import {AccountService} from "./_services/account.service";
import {User} from "./_models/User";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'client';

  constructor(private accountService : AccountService) {}

  ngOnInit(): void {
    const userString = localStorage.getItem('user');
    if(userString){
      const user : User = JSON.parse(userString);
      this.accountService.setCurrentUser(user);
    }
  }


}

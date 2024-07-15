import { Component } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {AccountService} from "../../_services/account.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginForm : FormGroup = new FormGroup({});

  constructor(private formBuilder: FormBuilder, private accountService: AccountService) {
  }

  ngOnInit(): void {
    this.initializeForm()
  }

  login(){
    this.accountService.login(this.loginForm.value)
  }

  initializeForm(){
    this.loginForm = this.formBuilder.group({
      usernameOrEmail: ['', Validators.required],
      password: ['', [Validators.required]]
    })
  }

}

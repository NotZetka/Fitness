import {Component, OnInit} from '@angular/core';
import {AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators} from "@angular/forms";
import {AccountService} from "../../_services/account.service";

@Component({
  selector: 'app-register-member',
  templateUrl: './register-member.component.html',
  styleUrl: './register-member.component.css'
})
export class RegisterMemberComponent implements OnInit{
    registerForm : FormGroup = new FormGroup({});

  constructor(private formBuilder: FormBuilder, private accountService: AccountService) {
  }

  ngOnInit(): void {
        this.initializeForm()
    }

  register(){
   this.registerForm.value.role = 'Member';
   this.accountService.register(this.registerForm.value)
  }

  initializeForm(){
    this.registerForm = this.formBuilder.group({
      username: ['', Validators.required],
      dateOfBirth:['', Validators.required],
      gender:['male'],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required, this.matchValues('password')]],
    })
    this.registerForm.controls['password'].valueChanges.subscribe({
      next: () => this.registerForm.controls['confirmPassword'].updateValueAndValidity()
    })
  }

  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control.value === control.parent?.get(matchTo)?.value ? null : {isMatching: true}
    }
  }

}

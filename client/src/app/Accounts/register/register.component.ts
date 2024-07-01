import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {AbstractControl, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators} from "@angular/forms";
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit{
    registerForm : FormGroup = new FormGroup({});

  constructor(private http: HttpClient, private formBuilder: FormBuilder, private router: Router) {
  }

  ngOnInit(): void {
        this.initializeForm()
    }

  register(){
    console.log(this.registerForm)
    this.http.post('https://localhost:7186/Accounts/Register', this.registerForm.value).subscribe({
      next: () => this.router.navigateByUrl('/')
    })
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

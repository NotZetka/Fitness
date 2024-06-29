import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent{
    model:any = {}

  constructor(private http: HttpClient) {
  }

    register(){
      this.http.post('https://localhost:7186/Accounts/Register', this.model).subscribe(

      )
    }

}

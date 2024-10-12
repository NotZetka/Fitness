import { Component } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  activeForm: string = 'member'; // Default to member form

  setActiveForm(formType: string) {
    this.activeForm = formType;
  }
}

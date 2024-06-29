import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'client';

  constructor(private http : HttpClient){}

  ngOnInit(): void {
    // this.http.get('https://localhost:7186/Accounts/Register', { responseType: 'text' }).subscribe({
    // this.http.get('https://localhost:7186/Accounts/Register', { responseType: 'text' }).subscribe({
    //   next: res  => this.title = res
    // })
  }


}

import {Injectable, OnInit} from '@angular/core';
import {User} from "../_models/User";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {BehaviorSubject} from "rxjs";
import {ToastrService} from "ngx-toastr";

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private baseUrl : string = 'https://localhost:7186/Accounts/'
  private currentUser = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUser.asObservable();

  constructor(private http: HttpClient, private router: Router,private toastr: ToastrService) { }

  login(form : any){
    this.http.post<User>(this.baseUrl + 'Login', form).subscribe({
      next: response => {
        if(response){
          this.setCurrentUser(response)
          this.toastr.success('Successfully logged in')
        }
        this.router.navigateByUrl('/')
      },
      error: error =>{
        this.toastr.error(error.error.error);
      }
    })
  }

  register(form : any){
    this.http.post<User>(this.baseUrl + 'Register', form).subscribe({
      next: response => {
        if(response){
          this.setCurrentUser(response)
          this.toastr.success('Successfully registered')
        }
        this.router.navigateByUrl('/')
      },
      error: error =>{
        this.toastr.error(error.error.error);
      }
    })
  }

  logout(){
    localStorage.removeItem('user')
    this.currentUser.next(null)
    this.toastr.success('Successfully logout')
  }

  setCurrentUser(user: User){
    localStorage.setItem('user', JSON.stringify(user))
    this.currentUser.next(user)
  }
}

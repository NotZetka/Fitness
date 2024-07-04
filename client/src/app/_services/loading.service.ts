import { Injectable } from '@angular/core';
import {NgxSpinnerService} from "ngx-spinner";

@Injectable({
  providedIn: 'root'
})
export class LoadingService {
  requestCount = 0;

  constructor(private spinner: NgxSpinnerService) { }

  busy(){
    this.requestCount++
    this.spinner.show(undefined,{
      bdColor: 'rgba(0, 0, 0, 0.8)',
      color: '#fff',
    })
  }

  idle(){
    this.requestCount--
    if(this.requestCount <= 0){
      this.requestCount = 0
      this.spinner.hide()
    }
  }
}

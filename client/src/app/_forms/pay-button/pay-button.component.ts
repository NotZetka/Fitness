import {Component, Input,  OnInit} from '@angular/core';
import {environment} from "../../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";

@Component({
  selector: 'app-pay-button',
  templateUrl: './pay-button.component.html',
  styleUrl: './pay-button.component.css'
})
export class PayButtonComponent implements OnInit {
  @Input() amount: number = 0;
  @Input() id: number = 0;
  baseUrl = environment.baseUrl;

  constructor(private http: HttpClient, private router: Router) { }
  handler:any = null;

  ngOnInit() {
    this.loadStripe();
  }

  getFree(){
    return this.http.post(environment.baseUrl + 'Payments/checkout', {
      tokenId: "",
      email: "",
      price: this.amount,
      planId: this.id,
    }).subscribe({
      next: response => this.router.navigateByUrl("/plans/list"),
    })
  }

  pay() {
    let tokenTemp: any;

    const createToken = (): Promise<any> => {
      return new Promise((resolve, reject) => {
        const handler = (<any>window).StripeCheckout.configure({
          key: environment.stripePK,
          locale: 'auto',
          token: (token: any) => {
            tokenTemp = token;
            resolve(token);
          },
        });

        handler.open({
          name: 'Demo Site',
          description: '2 widgets',
          amount: this.amount * 100,
        });
      });
    };

    createToken()
      .then((token) => {
        return this.http.post(environment.baseUrl + 'Payments/checkout', {
          tokenId: token.id,
          email: token.email,
          price: this.amount,
          planId: this.id,
        }).subscribe({
          next: response => this.router.navigateByUrl("/plans/list")
        });
      })
      .catch((err) => {
        console.error('Error creating token:', err);
      });
  }

  loadStripe() {

    if(!window.document.getElementById('stripe-script')) {
      var s = window.document.createElement("script");
      s.id = "stripe-script";
      s.type = "text/javascript";
      s.src = "https://checkout.stripe.com/checkout.js";
      s.onload = () => {
        this.handler = (<any>window).StripeCheckout.configure({
          key: 'pk_test_51HxRkiCumzEESdU2Z1FzfCVAJyiVHyHifo0GeCMAyzHPFme6v6ahYeYbQPpD9BvXbAacO2yFQ8ETlKjo4pkHSHSh00qKzqUVK9',
          locale: 'auto',
          token: function (token: any) {
            console.log(token)
            alert('Payment Success!!');
          }
        });
      }

      window.document.body.appendChild(s);
    }
  }
}

import {Component, Input,  OnInit} from '@angular/core';
import {environment} from "../../../environments/environment";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-pay-button',
  templateUrl: './pay-button.component.html',
  styleUrl: './pay-button.component.css'
})
export class PayButtonComponent implements OnInit {
  @Input() amount: number = 0;
  @Input() id: number = 0;
  baseUrl = environment.baseUrl;

  constructor(private http: HttpClient) { }
  handler:any = null;

  ngOnInit() {
    this.loadStripe();
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
          next: (response) => console.log('Payment Success:', response),
          error: (error) => console.error('Payment Failed:', error),
        });
      })
      .catch((err) => {
        console.error('Error creating token:', err);
      });
  }

  loadStripe() {
    console.log(window.document.getElementById('stripe-script'))
    if (!window.document.getElementById('stripe-script')) {
      const s = window.document.createElement('script') as HTMLScriptElement;
      s.id = 'stripe-script';
      s.type = 'text/javascript';
      s.src = 'https://js.stripe.com/v3/';
      s.onload = () => {
        this.handler = (window as any).StripeCheckout.configure({
          key: environment.stripePK,
          locale: 'auto',
          token: (token: any) => {
            console.log(this.baseUrl + 'payments/checkout')
            console.log({
              tokenId: token.id,
              email: token.email,
              price: this.amount,
              planId: this.id,
            })
            this.http.post<PaymentResponse>(this.baseUrl + 'payments/checkout', {
              tokenId: token.id,
              email: token.email,
              price: this.amount,
              planId: this.id,
            }).subscribe(
              (response: PaymentResponse) => {
                console.log('Payment successful:', response);
                alert('Payment Success!!');
              },
              (error) => {
                console.error('Payment error:', error);
                alert('Payment failed! Please try again.');
              }
            );
          }
        });
      };
      window.document.body.appendChild(s);
    }
  }
}

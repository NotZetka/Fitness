import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {HubConnection, HubConnectionBuilder} from "@microsoft/signalr";
import {User} from "../Accounts/_models/User";
import {Message} from "../Messages/_models/Message";
import {GetMessageThreadQueryResponse} from "../Messages/_models/GetMessageThreadQueryResponse";
import {BehaviorSubject, take} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  baseUrl = environment.baseUrl;
  private hubConnection? : HubConnection;
  private messageThreadSource =  new BehaviorSubject<Message[]>([]);
  messageThread$ = this.messageThreadSource.asObservable();

  constructor(private http: HttpClient) { }

  createHubConnection(user: User, otherUserId : number){
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.baseUrl+'hubs/message?userId='+otherUserId, {
        accessTokenFactory: () => user.token
      })
      .withAutomaticReconnect()
      .build()

    this.hubConnection.start().catch(error => console.log(error));

    this.hubConnection.on('ReceiveMessageThread', messages =>{
      this.messageThreadSource.next(messages);
    })

    this.hubConnection.on('NewMessage', message =>{
      this.messageThread$.pipe(take(1)).subscribe({
          next: messages =>{
            this.messageThreadSource.next([...messages, message]);
          }
      })
    })
  }

  stopHubConnection(){
    if(this.hubConnection){
      this.hubConnection.stop().catch(error => console.log(error));
    }
  }

  async sendMessage(message:any){
    return this.hubConnection?.invoke('SendMessage', message)
      .catch(error => console.log(error));
  }

  getMesssageThread(userId: number){
    return  this.http.get<GetMessageThreadQueryResponse>(this.baseUrl + 'Messages/' + userId);
  }
}

import {Component, OnInit, AfterViewChecked, ElementRef, ViewChild, OnDestroy} from '@angular/core';
import { Message } from "../_models/Message";
import { MessageService } from "../../_services/message.service";
import { ActivatedRoute } from "@angular/router";
import { AccountService } from "../../_services/account.service";
import { User } from "../../Accounts/_models/User";
import { take } from "rxjs";

@Component({
  selector: 'app-chat',
  templateUrl: './messages-chat.component.html',
  styleUrls: ['./messages-chat.component.css']
})
export class MessagesChatComponent implements OnInit, OnDestroy, AfterViewChecked {
  messages: Message[] = new Array<Message>();
  secondUserId?: number;
  currentUser?: User;
  newMessageContent: string = '';

  @ViewChild('messagesContainer') private messagesContainer!: ElementRef;

  constructor(public messageService: MessageService, private route: ActivatedRoute, public accountService: AccountService) {

    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: response => {
        if (response) {
          this.currentUser = response;
        }
      }
    })
  }

  ngOnDestroy(): void {
        this.messageService.stopHubConnection()
    }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.secondUserId = Number(params.get('userId'));
    });
    if(this.currentUser && this.secondUserId){
      this.messageService.createHubConnection(this.currentUser, this.secondUserId)
      this.messageService.getMesssageThread(this.secondUserId).subscribe({
        next: messages=> this.messages = messages.messages
      })
    }
  }

  ngAfterViewChecked(): void {
    this.scrollToBottom();
  }

  scrollToBottom(): void {
    try {
      this.messagesContainer.nativeElement.scrollTop = this.messagesContainer.nativeElement.scrollHeight;
    } catch (err) {
      console.error('Could not scroll to bottom:', err);
    }
  }

  sendMessage(): void {
    if (this.newMessageContent.trim() !== '') {
      const newMessage = {
        content: this.newMessageContent,
        receiverId: this.secondUserId
      };
      this.messageService.sendMessage(newMessage).then(()=>{})
      this.scrollToBottom();
    }
    this.newMessageContent = '';
  }

  getClassForMessage(message: Message): string {
    return message.senderUsername === this.currentUser?.username ? 'message-left' : 'message-right';
  }
}

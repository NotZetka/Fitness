import {Component, Input} from '@angular/core';
import {UserDto} from "../_models/userDto";
import {PresenceService} from "../../_services/presence.service";

@Component({
  selector: 'app-user-card',
  templateUrl: './user-card.component.html',
  styleUrl: './user-card.component.css'
})
export class UserCardComponent {
  @Input() user: UserDto | undefined;

  constructor(public presenceService: PresenceService) { }

}

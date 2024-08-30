import {UserDto} from "./userDto";

export interface GetAccountsListResponse {
  items: UserDto[],
  totalPages: number,
  totalCount: number,
  itemsFrom: number,
  itemsTo: number
}

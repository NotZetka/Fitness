import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {UserDto} from "../_models/userDto";
import {GetAccountsListResponse} from "../_models/GetAccountsListResponse";
import {environment} from "../../../environments/environment";

@Component({
  selector: 'app-list',
  templateUrl: './accounts-list.component.html',
  styleUrl: './accounts-list.component.css'
})
export class AccountsListComponent implements OnInit {
  baseUrl = environment.baseUrl;
  users : UserDto[] = new Array<UserDto>();
  totalPages?: number;
  totalCount?: number;
  itemsFrom?: number;
  itemsTo?: number;
  pageSize: number = 10;
  currentPage: number = 1;
  filter? : string;

  constructor(private http: HttpClient) {
  }

  ngOnInit(): void {
    this.getList(this.currentPage,this.pageSize,this.filter)
  }

  public getList(pageNumber: number, pageSize: number, filter?: string): void{
    let url = this.baseUrl+'Accounts/List?' + 'PageNumber=' + pageNumber + '&PageSize=' + pageSize;
    if(filter !== undefined){
      url += '&Role=' + filter;
    }
    this.http.get<GetAccountsListResponse>(url).subscribe({
      next: response => {
        this.users = response.items;
        this.totalPages = response.totalPages;
        this.totalCount = response.totalCount;
        this.itemsFrom = response.itemsFrom;
        this.itemsTo = response.itemsTo;
        this.currentPage = pageNumber;
        this.filter = filter;
      }
    })
  }

  goToPage(pageNumber: number): void {
    if (pageNumber >= 1 && pageNumber <= (this.totalPages || 1)) {
      this.getList(pageNumber, this.pageSize);
    }
  }

  nextPage(): void {
    if (this.currentPage < (this.totalPages || 1)) {
      this.goToPage(this.currentPage + 1);
    }
  }

  previousPage(): void {
    if (this.currentPage > 1) {
      this.goToPage(this.currentPage - 1);
    }
  }
}

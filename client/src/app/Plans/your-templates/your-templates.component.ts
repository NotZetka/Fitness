import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../environments/environment";
import {PlanTemplate} from "../.models/planTemplate";
import {GetPlanTemplatesQueryResult} from "../.models/GetPlanTemplatesQueryResult";

@Component({
  selector: 'app-your-templates',
  templateUrl: './your-templates.component.html',
  styleUrl: './your-templates.component.css'
})
export class YourTemplatesComponent implements OnInit{
  baseUrl = environment.baseUrl;
  plans: Array<PlanTemplate> = new Array<PlanTemplate>();
  totalPages?: number;
  totalCount?: number;
  itemsFrom: number = 1;
  itemsTo: number = 1;
  pageSize: number = 10;
  currentPage: number = 1;


  constructor(private http : HttpClient) {}

  ngOnInit(): void {
        this.getList(this.currentPage, this.pageSize);
    }

  getList(pageNumber: number, pageSize: number): void {
    this.http.get<GetPlanTemplatesQueryResult>(`${this.baseUrl}Plans/Your-Templates?PageNumber=${pageNumber}&PageSize=${pageSize}`).subscribe({
      next: response => {
        this.plans = response.items;
        this.totalPages = response.totalPages;
        this.totalCount = response.totalCount;
        this.itemsFrom = (pageNumber - 1) * pageSize + 1;
        this.itemsTo = Math.min(pageNumber * pageSize, this.totalCount || 0);
        this.currentPage = pageNumber;
      }
    });
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

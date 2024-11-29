import { Component, OnInit } from '@angular/core';
import { PlanTemplate } from "../.models/planTemplate";
import { HttpClient } from "@angular/common/http";
import { GetPlanTemplatesQueryResult } from "../.models/GetPlanTemplatesQueryResult";
import { Router } from "@angular/router";
import { environment } from "../../../environments/environment";

@Component({
  selector: 'app-plans-market',
  templateUrl: './plans-market.component.html',
  styleUrl: './plans-market.component.css'
})
export class PlansMarketComponent implements OnInit {
  baseUrl = environment.baseUrl;
  plans: Array<PlanTemplate> = new Array<PlanTemplate>();
  totalPages?: number;
  totalCount?: number;
  itemsFrom: number = 1;
  itemsTo: number = 1;
  pageSize: number = 10;
  currentPage: number = 1;

  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit(): void {
    this.getList(this.currentPage, this.pageSize);
  }

  getList(pageNumber: number, pageSize: number): void {
    this.http.get<GetPlanTemplatesQueryResult>(`${this.baseUrl}Plans/Templates?PageNumber=${pageNumber}&PageSize=${pageSize}`).subscribe({
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

  addPlan(id: number) {
    this.http.get(this.baseUrl + 'Plans/add/' + id).subscribe({
      next: response => {
        this.router.navigateByUrl('/plans/list');
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

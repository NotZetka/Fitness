import {Component, OnInit} from '@angular/core';
import {PlanTemplate} from "../models/planTemplate";
import {HttpClient} from "@angular/common/http";
import {GetPlanTemplatesQueryResult} from "../models/GetPlanTemplatesQueryResult";
import {Router} from "@angular/router";
import {add} from "ngx-bootstrap/chronos";
import {environment} from "../../../environments/environment";

@Component({
  selector: 'app-plans-market',
  templateUrl: './plans-market.component.html',
  styleUrl: './plans-market.component.css'
})
export class PlansMarketComponent implements OnInit {
  baseUrl = environment.baseUrl;
    plans : Array<PlanTemplate> = new Array<PlanTemplate>();

    constructor(private http : HttpClient, private router: Router) {}

    ngOnInit(): void {
      this.http.get<GetPlanTemplatesQueryResult>(this.baseUrl + 'Plans/Templates/').subscribe({
        next: response => {
          this.plans = response.items;
        }
      })
    }

    addPlan(id: number){
      this.http.get(this.baseUrl + 'Plans/add/'+id).subscribe({
        next: response => {
          this.router.navigateByUrl('/plans/list')
        }
      })
    }

  protected readonly add = add;
}

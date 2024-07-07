import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {Plan} from "../models/plan";
import {HttpClient} from "@angular/common/http";
import {GetPlanQueryResult} from "../models/GetPlanQueryResult";
import {Record} from "../models/record";

@Component({
  selector: 'app-plans-detail',
  templateUrl: './plans-detail.component.html',
  styleUrl: './plans-detail.component.css'
})
export class PlansDetailComponent implements OnInit {
  planId? : number;
  plan? : Plan;

  constructor(private route: ActivatedRoute, private http: HttpClient) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.planId = Number(params.get('id'));
    });
    this.http.get<GetPlanQueryResult>('https://localhost:7186/plans/'+this.planId).subscribe({
      next: response => {
        this.plan = response.plan;
      }
    })
  }

  recordWeight(records: Record[]){
    let weights = records.map(x=>x.weight)
    let maxValue =  Math.max(...weights);
    let record  = records.filter(x=>x.weight == maxValue).pop()
    return record;
  }
}

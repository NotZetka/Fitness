import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {Plan} from "../models/plan";
import {GetPlansQueryResult} from "../models/GetPlansQueryResult";

@Component({
  selector: 'app-plans-list',
  templateUrl: './plans-list.component.html',
  styleUrl: './plans-list.component.css'
})
export class PlansListComponent implements OnInit{
  plans : Plan[] = new Array<Plan>();
  showArchived = false;

  constructor(private http : HttpClient, private router: Router) {}

  ngOnInit(): void {
      this.http.get<GetPlansQueryResult>('https://localhost:7186/Plans/').subscribe({
        next: response => {
          console.log(response);
          this.plans = response.plans;
        }
      })
    }
  archivePlan(id:number){
    this.http.patch(`https://localhost:7186/Plans/archive/${id}`,{}).subscribe({
      next: response => {
        this.ngOnInit()
      }
    })
  }

}

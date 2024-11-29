import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {Plan} from "../.models/plan";
import {HttpClient} from "@angular/common/http";
import {GetPlanQueryResult} from "../.models/GetPlanQueryResult";
import {FormArray, FormBuilder, FormGroup, Validators} from "@angular/forms";
import {AddRecordsQuery} from "../.models/AddRecordsQuery";
import {Rec} from "../.models/rec";
import {environment} from "../../../environments/environment";

@Component({
  selector: 'app-plans-detail',
  templateUrl: './plans-detail.component.html',
  styleUrl: './plans-detail.component.css'
})
export class PlansDetailComponent implements OnInit {
  baseUrl = environment.baseUrl;
  exerciseForm: FormGroup = new FormGroup({});
  planId? : number;
  plan? : Plan;
  showRecord : Map<number,boolean> = new Map<number, boolean>();

  constructor(private route: ActivatedRoute, private http: HttpClient, private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.planId = Number(params.get('id'));
    });
    this.http.get<GetPlanQueryResult>(this.baseUrl + 'plans/'+this.planId).subscribe({
      next: response => {
        this.plan = response.plan
        this.plan.exercises.map(exercise => {
          this.showRecord.set(exercise.id, false);
        })
        this.initializeForm();
      },
      error: (err) => {
        console.error(err);
      }
    })
  }

  initializeForm(): void {
    const exerciseFormGroups = this.plan?.exercises.map(exercise => this.formBuilder.group({
      weight: ['',[Validators.required]],
      reps: ['1', [Validators.required, Validators.min(1)]],
      exerciseId: [exercise.id]
    })) || [];

    this.exerciseForm = this.formBuilder.group({
      exercises: this.formBuilder.array(exerciseFormGroups)
    });
  }

  get exercises(): FormArray {
    return this.exerciseForm.get('exercises') as FormArray;
  }

  recordWeight(records: Rec[]){
    let weights = records.map(x=>x.weight)
    let maxValue =  Math.max(...weights);
    let record  = records.filter(x=>x.weight == maxValue).pop()
    return record;
  }

  onSubmit(): void {
    const submittedData = this.exerciseForm.value.exercises.map((exercise: any, index: number) => ({
      exerciseId: this.plan?.exercises[index].id,
      weight: exercise.weight,
      repetitions: exercise.reps
    }));
    const query : AddRecordsQuery =  {
      records: submittedData,
    }
    console.log(query)
    this.http.post(this.baseUrl + 'plans/add', query).subscribe({
      next: _ => {
        this.ngOnInit()
      }
    })
  }
}

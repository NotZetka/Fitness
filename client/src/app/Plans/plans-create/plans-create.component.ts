import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {HttpClient} from "@angular/common/http";
import {PlanTemplateResponse} from "../models/planTemplateResponse";
import {Router} from "@angular/router";
import {environment} from "../../../environments/environment";

@Component({
  selector: 'app-plans-create',
  templateUrl: './plans-create.component.html',
  styleUrl: './plans-create.component.css'
})
export class PlansCreateComponent implements OnInit {
  baseUrl  = environment.baseUrl;
  createPlanForm : FormGroup = new FormGroup({})
  exerciseForm: FormGroup = new FormGroup({})
  exercises : any[] = new Array<any>();

  constructor(private formBuilder: FormBuilder, private http: HttpClient, private router: Router) {
  }

  ngOnInit(): void {
    this.initializeForm()
  }

  submit(){
    let result = {
      ...this.createPlanForm.value,
      'exercises' : this.exercises,
      'public': this.createPlanForm.value['public']=='public'
    }
    console.log(result)
    this.http.post<PlanTemplateResponse>(this.baseUrl + 'Plans/Create',result).subscribe({
      next: response => {
        this.http.get(this.baseUrl + 'Plans/add/'+response.id).subscribe({})
        this.router.navigateByUrl('/plans/list')
      }
    })
  }

  initializeForm(){
    this.createPlanForm = this.formBuilder.group({
      'name': ['', Validators.required],
      'public': ['private']
    })
    this.exerciseForm = this.formBuilder.group({
      'name': ['', Validators.required],
      'description': [''],
      'sets': ['1', Validators.min(1)],
    })
  }

  addExercise() {
    this.exercises.push({...this.exerciseForm.value});
    this.exerciseForm.reset();
  }

  removeExercise(i: number) {
    this.exercises.splice(i, 1);
  }
}

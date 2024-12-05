import {Component, OnInit} from '@angular/core';
import {environment} from "../../../environments/environment";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {PlanTemplateResponse} from "../.models/planTemplateResponse";

@Component({
  selector: 'app-plans-create-template',
  templateUrl: './plans-create-template.component.html',
  styleUrl: './plans-create-template.component.css'
})
export class PlansCreateTemplateComponent implements OnInit{
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
      'public': this.createPlanForm.value['public']=='public',
    }
    console.log(result)
    this.http.post<PlanTemplateResponse>(this.baseUrl + 'Plans/Create-Template',result).subscribe({
     next: response => {
      this.router.navigateByUrl('/plans/your-templates')
    }
    })
  }

  initializeForm(){
    this.createPlanForm = this.formBuilder.group({
      'name': ['', Validators.required],
      'public': ['private'],
      'price': [{ value: '', disabled: true }, [Validators.required, Validators.min(0)]],
    })
    this.exerciseForm = this.formBuilder.group({
      'name': ['', Validators.required],
      'description': [''],
      'sets': ['1', Validators.min(1)],
    })
    this.createPlanForm.get('public')?.valueChanges.subscribe((value) => {
      if (value === 'public') {
        this.createPlanForm.get('price')?.enable();
      } else {
        this.createPlanForm.get('price')?.disable();
        this.createPlanForm.get('price')?.reset();
      }
    });
  }

  addExercise() {
    this.exercises.push({...this.exerciseForm.value});
    this.exerciseForm.reset();
  }

  removeExercise(i: number) {
    this.exercises.splice(i, 1);
  }
}

import {Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {PlanTemplate} from "../.models/planTemplate";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {PlanTemplateResponse} from "../.models/planTemplateResponse";
import {environment} from "../../../environments/environment";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-edit-template',
  templateUrl: './edit-template.component.html',
  styleUrl: './edit-template.component.css'
})
export class EditTemplateComponent implements OnInit {
  baseUrl  = environment.baseUrl;
  editPlanForm : FormGroup = new FormGroup({})
  exerciseForm: FormGroup = new FormGroup({})
  exercises : any[] = new Array<any>();

  constructor(private formBuilder: FormBuilder, private http: HttpClient, private router: Router) { }

  ngOnInit(): void {
      this.initializeForm(history.state.plan);
      console.log(history.state.plan);
  }

  submit(){
    let result = {
      ...this.editPlanForm.value,
      'exercises' : this.exercises,
      'public': this.editPlanForm.value['public']=='public',
    }
    this.http.put<PlanTemplateResponse>(this.baseUrl + 'Plans/Edit-Template/' + history.state.plan.planId,result).subscribe({
      next: response => {
        this.router.navigateByUrl('/plans/your-templates')
      }
    })
  }


  initializeForm(plan: PlanTemplate){
    let _public = plan.public ? 'public' : 'private';
    let _price = [{ value: plan.price.toString(), disabled: !_public }, [Validators.required, Validators.min(0)]];
    this.exercises = plan.exercises;
    console.log(_public)
    this.editPlanForm = this.formBuilder.group({
      'name': [plan.name, Validators.required],
      'public': [_public],
      'price': _price,
    })
    this.exerciseForm = this.formBuilder.group({
      'name': ['', Validators.required],
      'description': [''],
      'sets': ['1', Validators.min(1)],
    })
    this.editPlanForm.get('public')?.valueChanges.subscribe((value) => {
      if (value === 'public') {
        this.editPlanForm.get('price')?.enable();
      } else {
        this.editPlanForm.get('price')?.disable();
        this.editPlanForm.get('price')?.reset();
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

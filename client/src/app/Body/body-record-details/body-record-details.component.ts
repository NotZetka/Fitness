import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BodyService } from '../../_services/body.service';

@Component({
  selector: 'app-body-record-details',
  templateUrl: './body-record-details.component.html',
  styleUrls: ['./body-record-details.component.css']
})
export class BodyRecordDetailsComponent implements OnInit {
  recordForm: FormGroup;
  genderMale: boolean;
  height: number;
  isViewMode: boolean;

  constructor(private fb: FormBuilder,
              private route: ActivatedRoute,
              private bodyService: BodyService,
              private router: Router) {
    this.recordForm = this.fb.group({
      weight: ['', [Validators.required, Validators.min(1)]],
      neck: [null, [Validators.min(1)]],
      chest: [null, [Validators.min(1)]],
      arm: [null, [Validators.min(1)]],
      forearm: [null, [Validators.min(1)]],
      waist: [null, [Validators.min(1)]],
      hip: [null, [Validators.min(1)]],
      thigh: [null, [Validators.min(1)]],
      calf: [null, [Validators.min(1)]],
      bmi: [],
      status: [],
      bodyFat: []
    });

    this.genderMale = this.route.snapshot.queryParams['genderMale'] === 'true';
    this.height = this.route.snapshot.queryParams['height'];
    this.isViewMode = this.route.snapshot.queryParams['isViewMode'] === 'true';
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.recordForm.patchValue(params);
      if (this.isViewMode) {
        this.recordForm.disable();
      }
    });

    this.recordForm.valueChanges.subscribe(() => {
      this.calculateBMI();
      this.calculateStatus();
      this.calculateBodyFat();
    });
  }

  onSubmit() {
    if (this.isViewMode) {
      return;
    }
    const formValue = this.convertEmptyToNull(this.recordForm.value);
    this.bodyService.addRecord(formValue).subscribe({
      next: () => {
        this.router.navigateByUrl('/body');
      },
      error: error => console.log(error),
    });
  }

  convertEmptyToNull(formValue: any): any {
    const result: any = {};
    for (const key in formValue) {
      if (formValue[key] === '') {
        result[key] = null;
      } else {
        result[key] = formValue[key];
      }
    }
    return result;
  }

  calculateBMI(): void {
    const weight = this.recordForm.get('weight')?.value;
    const height = this.height;

    if (weight && height) {
      const bmi = (10000 * weight / (height * height)).toFixed(2);
      this.recordForm.get('bmi')?.setValue(bmi, { emitEvent: false });
    }
  }

  calculateStatus(): void {
    const bmi = this.recordForm.get('bmi')?.value;

    if (bmi) {
      let status = '';
      if (bmi < 18.5) {
        status = 'Underweight';
      } else if (bmi < 24.9) {
        status = 'Normal weight';
      } else if (bmi < 29.9) {
        status = 'Overweight';
      } else {
        status = 'Obesity';
      }
      this.recordForm.get('status')?.setValue(status, { emitEvent: false });
    }
  }

  calculateBodyFat(): void {
    const weight = this.recordForm.get('weight')?.value;
    const height = this.height;
    const neck = this.recordForm.get('neck')?.value;
    const waist = this.recordForm.get('waist')?.value;
    const hip = this.recordForm.get('hip')?.value;

    if (weight && height && neck && waist) {
      let bodyFat = 0;
      if (this.genderMale) {
        bodyFat = 86.010 * Math.log10(waist - neck) - 70.041 * Math.log10(height) + 36.76;
      } else if (hip) {
        bodyFat = 163.205 * Math.log10(waist + hip - neck) - 97.684 * Math.log10(height) - 78.387;
      }
      this.recordForm.get('bodyFat')?.setValue(bodyFat.toFixed(2), { emitEvent: false });
    }
  }

  navigateBack(): void {
    this.router.navigateByUrl('/body');
  }
}

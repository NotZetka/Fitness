import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BodyService } from '../../_services/body.service';
import { BodyWeight } from '../models/BodyWeight';

@Component({
  selector: 'app-body-main',
  templateUrl: './body-main.component.html',
  styleUrls: ['./body-main.component.css']
})
export class BodyMainComponent implements OnInit {
  bodyWeight?: BodyWeight;
  genderMale: boolean = false;
  isEditingHeight: boolean = true;
  newHeight?: number;

  constructor(private bodyService: BodyService, private router: Router) {}

  ngOnInit(): void {
    this.bodyService.getBodyWeight().subscribe({
      next: response => {
        this.bodyWeight = response.bodyWeight;
        if (this.bodyWeight.height) {
          this.isEditingHeight = false;
        }
        this.genderMale = response.genderMale;
      }
    });
  }

  enableHeightEdit(): void {
    this.isEditingHeight = true;
    this.newHeight = this.bodyWeight?.height || undefined;
  }

  saveHeight(): void {
    if (this.newHeight != null) {
      this.bodyService.saveHeight(this.newHeight).subscribe({
        next: () => {
          if (this.bodyWeight && this.newHeight) {
            this.bodyWeight.height = this.newHeight;
          }
          this.isEditingHeight = !this.isEditingHeight;
        }
      });
    }
  }

  cancelEdit(): void {
    this.isEditingHeight = !this.isEditingHeight;
  }

  calculateBMI(weight: number, height?: number): string {
    if (!height) return '--';
    return (10000 * weight / (height * height)).toFixed(2);
  }

  getStatus(weight: number, height?: number): string {
    const bmi = this.calculateBMI(weight, height);
    if (bmi === '--') return '--';
    const bmiValue = parseFloat(bmi);
    if (bmiValue < 18.5) return 'Underweight';
    if (bmiValue < 24.9) return 'Normal weight';
    if (bmiValue < 29.9) return 'Overweight';
    return 'Obesity';
  }

  addRecord(): void {
    this.router.navigate(['/body-record-details'], { queryParams: { genderMale: this.genderMale, height: this.bodyWeight?.height, isViewMode: false } });
  }

  viewDetails(record: any): void {
    this.router.navigate(['/body-record-details'], { queryParams: { ...record, genderMale: this.genderMale, height: this.bodyWeight?.height, isViewMode: true } });
  }
}

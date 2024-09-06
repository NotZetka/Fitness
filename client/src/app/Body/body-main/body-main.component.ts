import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BodyService } from '../../_services/body.service';
import {BodyWeightRecord} from "../models/BodyWeightRecord";
import {PagedResult} from "../../_common/PagedResult";

@Component({
  selector: 'app-body-main',
  templateUrl: './body-main.component.html',
  styleUrls: ['./body-main.component.css']
})
export class BodyMainComponent implements OnInit {
  bodyWeightRecords?: PagedResult<BodyWeightRecord>;
  height?: number;
  genderMale: boolean = false;
  isEditingHeight: boolean = true;
  newHeight?: number;
  isLoaded: boolean = false;
  currentPage = 1;
  pageSize = 10;

  constructor(private bodyService: BodyService, private router: Router) {}

  ngOnInit(): void {
    this.loadBodyWeightRecords(this.currentPage, this.pageSize)
  }

  loadBodyWeightRecords(pageNumber: number, pageSize: number): void {
    this.bodyService.getBodyWeight(pageNumber, pageSize).subscribe({
      next: response => {
        this.bodyWeightRecords = response.bodyWeightRecords;
        console.log(this.bodyWeightRecords);
        if (response.height) {
          this.height = response.height;
          this.isEditingHeight = false;
        }
        this.genderMale = response.genderMale;
        this.isLoaded = true;
      },
      error: () => {
        this.isLoaded = false;
      }
    });
  }

  enableHeightEdit(): void {
    this.isEditingHeight = true;
    this.newHeight = this.height || undefined;
  }

  saveHeight(): void {
    if (this.newHeight != null) {
      this.bodyService.saveHeight(this.newHeight).subscribe({
        next: () => {
          if (this.bodyWeightRecords && this.newHeight) {
            this.height = this.newHeight;
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
    this.router.navigate(['/body-record-details'], { queryParams: { genderMale: this.genderMale, height: this.height, isViewMode: false } });
  }

  viewDetails(record: any): void {
    this.router.navigate(['/body-record-details'], { queryParams: { ...record, genderMale: this.genderMale, height: this.height, isViewMode: true } });
  }

  goToPreviousPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.loadBodyWeightRecords(this.currentPage, this.pageSize);
    }
  }

  goToNextPage(): void {
    if (this.bodyWeightRecords && this.currentPage < this.bodyWeightRecords.totalPages) {
      this.currentPage++;
      this.loadBodyWeightRecords(this.currentPage, this.pageSize);
    }
  }
}

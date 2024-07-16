import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {GetBodyWeightQueryResponse} from "../Body/models/GetBodyWeightQueryResponse";
import {AddBodyWeightRecordQuery} from "../Body/models/AddBodyWeightRecordQuery";

@Injectable({
  providedIn: 'root'
})
export class BodyService {
  baseUrl = environment.baseUrl;

  constructor(private http : HttpClient) { }

  getBodyWeight(){
    return this.http.get<GetBodyWeightQueryResponse>(this.baseUrl+'Body');
  }

  saveHeight(height: number){
    return this.http.put(this.baseUrl+'Body', {
      height:height
    });
  }

  addRecord(record:AddBodyWeightRecordQuery){
    return this.http.post(this.baseUrl+'Body',record);
  }
}

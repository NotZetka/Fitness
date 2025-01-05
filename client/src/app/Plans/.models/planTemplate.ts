import {ExerciseTemplate} from "./exerciseTemplate";

export interface PlanTemplate {
  name: string;
  authorName: string;
  description: string;
  exercises: ExerciseTemplate[];
  planId: number;
  show: boolean;
  public: boolean;
  price: number;
}

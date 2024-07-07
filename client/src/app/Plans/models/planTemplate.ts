import {ExerciseTemplate} from "./exerciseTemplate";

export interface PlanTemplate {
  name: string;
  authorName: string;
  exercises: ExerciseTemplate[];
  planId: number;
  show: boolean;
}

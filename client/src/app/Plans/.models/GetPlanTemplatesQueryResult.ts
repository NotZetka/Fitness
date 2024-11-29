import {PlanTemplate} from "./planTemplate";

export interface GetPlanTemplatesQueryResult {
  items: PlanTemplate[];
  totalPages: number,
  totalCount: number,
  itemsFrom: number,
  itemsTo: number
}

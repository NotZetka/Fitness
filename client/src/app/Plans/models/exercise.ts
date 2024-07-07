import {Record} from "./record";

export interface Exercise {
  name: string;
  description: string;
  records: Record[];
}

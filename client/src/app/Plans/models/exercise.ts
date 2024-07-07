import {Rec} from "./rec";

export interface Exercise {
  id: number;
  name: string;
  description: string;
  records: Rec[];
}

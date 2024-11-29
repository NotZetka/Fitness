import {Exercise} from "./exercise";

export interface Plan {
  id: number;
  archived: boolean;
  name: string;
  exercises: Exercise[];
}

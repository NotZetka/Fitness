import {BodyWeightRecord} from "./BodyWeightRecord";
import {PagedResult} from "../../_common/PagedResult";

export interface GetBodyWeightQueryResponse {
  bodyWeightRecords: PagedResult<BodyWeightRecord>
  height: number;
  genderMale: boolean;
}

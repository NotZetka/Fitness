export interface PagedResult<T> {
  items: T[];
  totalPages: number;
  totalCount: number;
  itemsFrom: number;
  itemsTo: number;
}

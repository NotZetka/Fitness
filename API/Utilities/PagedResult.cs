using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API.Utilities
{
    public class PagedResult<T>
    {
        public PagedResult(List<T> items)
        {
            Items = items;
            TotalPages = 1;
            TotalCount = items.Count;
            ItemsFrom = items.IsNullOrEmpty() ? 0 : 1;
            ItemsTo = items.Count;
        }
        public PagedResult(List<T> items, int totalPages, int totalCount, int itemsFrom, int itemsTo)
        {
            Items = items;
            TotalPages = totalPages;
            TotalCount = totalCount;
            ItemsFrom = itemsFrom;
            ItemsTo = itemsTo;
        }
        public List<T> Items { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int ItemsFrom { get; set; }
        public int ItemsTo { get; set; }
        public static async Task<PagedResult<T>> CreateFromQueryAsync(IQueryable<T> items, int pageNumber, int pageSize)
        {
            var totalCount = items.Count();
            var toatlPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            var itemsFrom = (pageNumber - 1) * pageSize + 1;
            var itemsTo = itemsFrom + pageSize - 1 < totalCount ? itemsFrom + pageSize - 1 : totalCount;
            return new PagedResult<T>(await items.ToListAsync(), toatlPages, totalCount, itemsFrom, itemsTo);
        }
    }
}

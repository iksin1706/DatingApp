using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Helpers
{
    public class PagedList<T> : List<T>
    {
        public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            this.CurrentPage = pageNumber;
            this.PageSize = pageSize;
            this.TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.TotalCount = count;
            this.AddRange(items);
        }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public static async Task<PagedList<T>> CreateAsync(
            IQueryable<T> source,
            int pageNumber,
            int pageSize
        ) {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedList<T>(items,count,pageNumber,pageSize);
         }
    }
}

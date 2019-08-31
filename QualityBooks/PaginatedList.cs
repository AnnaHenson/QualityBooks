﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace QualityBooks
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

        public PaginatedList(List<T> items, int count, int pageindex, int pageSize)
        {
            PageIndex = pageindex;
            TotalPages = (int) Math.Ceiling(count / (double) pageSize);

            this.AddRange(items);

        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);

            }
        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageindex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageindex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageindex, pageSize);
        }
    }
}
    
       


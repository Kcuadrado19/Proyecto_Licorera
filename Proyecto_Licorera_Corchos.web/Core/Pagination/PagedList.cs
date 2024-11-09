﻿using Microsoft.EntityFrameworkCore;
using Proyecto_Licorera_Corchos.web.Core.Extensions;

namespace Proyecto_Licorera_Corchos.web.Core.Pagination
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int RecordsPerPage { get; private set; }
        public int TotalCount { get; private set; }

        public PagedList(List<T> items, int count, int pageNumber, int recordsPerPage)
        {
            TotalCount = count;
            RecordsPerPage = recordsPerPage;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)recordsPerPage);
        }

        public static async Task<PagedList<T>> ToPagedListAsync(IQueryable<T> query, PaginationRequest request) 
        {
            int count = await query. CountAsync();

            List<T> items = await query.Paginate<T>(request)
                                        .ToListAsync();

            return new PagedList<T>(items, count, request.Page, request.RecordPerPage);
        }
    }
}
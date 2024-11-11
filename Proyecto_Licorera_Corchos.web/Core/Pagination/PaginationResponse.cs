﻿namespace Proyecto_Licorera_Corchos.web.Core.Pagination
{
    public class PaginationResponse<T> where T : class
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int RecordsPerPage { get; set; }
        public int TotalCount { get; set; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        public int VisiblePages => 5;
        public string? Filter { get; set; }

        // Inicializamos PagedList con una lista vacía y argumentos requeridos
        public PagedList<T> List { get; set; } = new PagedList<T>(new List<T>(), 0, 1, 1);

        public List<int> Pages
        {
            get
            {
                List<int> pages = new List<int>();
                int half = VisiblePages / 2;
                int start = CurrentPage - half + 1 - (VisiblePages % 2);
                int end = CurrentPage + half;

                int vPages = VisiblePages;

                if (vPages > TotalPages)
                {
                    vPages = TotalPages;
                }

                if (start <= 0)
                {
                    start = 1;
                    end = vPages;
                }

                if (end > TotalPages)
                {
                    start = TotalPages - vPages + 1;
                    end = TotalPages;
                }

                for (int itPage = start; itPage <= end; itPage++)
                {
                    pages.Add(itPage);
                }

                return pages;
            }
        }
    }
}



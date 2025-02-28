﻿namespace aHoang.Services
{
    public class PagedResult<T>
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public List<T> Items { get; set; } = new List<T>();
    }
}

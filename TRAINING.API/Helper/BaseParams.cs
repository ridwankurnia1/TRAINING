using System;

namespace TRAINING.API.Helper
{
    public abstract class BaseParams
    {
        private const int MaxPageSize = 1000;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 10;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
    }
}
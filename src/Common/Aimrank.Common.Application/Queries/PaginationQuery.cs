using System;

namespace Aimrank.Common.Application.Queries
{
    public class PaginationQuery
    {
        private readonly int _page;
        private readonly int _size;

        public int Page
        {
            get => _page;
            private init => _page = Math.Max(1, value);
        }

        public int Size
        {
            get => _size;
            private init => _size = Math.Max(1, value);
        }

        public PaginationQuery(int page = 1, int size = 1)
        {
            Page = page;
            Size = size;
        }

        public int Offset => (Page - 1) * Size;
        public int Fetch => Size;
    }
}
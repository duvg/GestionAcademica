using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GestionAcademica.Helpers
{
    public class Pagination<T>: List<T>
    {
        #region Properties

        public int _pageIndex  { get; private set; }
        public int _totalPages { get; private set; }
        public int _totalR { get; private set; }

        #endregion

        public Pagination(List<T> items, int count, int pageIndex, int pageSize)
        {
            _pageIndex = pageIndex;
            _totalPages = (int)Math.Ceiling(count / (double)pageSize);

            _totalR = count;
            this.AddRange(items);
        }

        #region Methods

        public bool HasPreviousPage
        {
            get
            {
                return (_pageIndex > 1);
            }   
        }

        public bool HasNextPage
        {
            get
            {
                return (_pageIndex < _totalPages);
            }
        }


        public static async Task<Pagination<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).ToListAsync();
            return new Pagination<T>(items, count, pageIndex, pageSize);
        }
        #endregion
    }
}

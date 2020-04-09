using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DetonaFit.Controllers
{
    [Serializable]
    public class PaginatedData<T> : List<T>
    {
        public Int32 PageIndex { get; private set; }
        public Int32 PageSize { get; private set; }
        public Int32 TotalCount { get; private set; }
        public Int32 TotalPages { get; private set; }
       
        public PaginatedData(IEnumerable<T> source, Int32 pageIndex, Int32 pageSize, ref Int32 TotalRegistros)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = 0;
            TotalPages = 0;

            try
            {
                TotalCount = source.Count();                
                if (PageSize == -1)
                {
                    PageSize = TotalCount;
                }

                TotalPages = (Int32)Math.Ceiling(TotalCount / (double)PageSize);
                this.AddRange(source.Skip(PageIndex).Take(PageSize));

                TotalRegistros = TotalCount;
            }
            catch (System.NotSupportedException ex)
            {
                Debug.WriteLine(ex.Message);

            }
            catch (Exception e)
            {
                string x = e.ToString();

            }
        }

        public PaginatedData(IQueryable<T> source, Int32 pageIndex, Int32 pageSize, ref Int32 TotalRegistros)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = 0;
            TotalPages = 0;

            try
            {
                TotalCount = source.Count();
                if (PageSize == -1)
                {
                    PageSize = TotalCount;
                }

                TotalPages = (Int32)Math.Ceiling(TotalCount / (double)PageSize);


                this.AddRange(source.Skip(PageIndex).Take(PageSize));

                TotalRegistros = TotalCount;
            }
            catch (System.NotSupportedException ex)
            {
                Debug.WriteLine(ex.Message);

            }
            catch (Exception e)
            {
                string x = e.ToString();

            }
        }

        public PaginatedData(List<T> lista, Int32 pageIndex, Int32 pageSize, ref Int32 TotalRegistros)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = 0;
            TotalPages = 0;
            try
            {
                TotalCount = lista.Count();

                if (PageSize == -1)
                {
                    PageSize = TotalCount;
                }

                TotalPages = (Int32)Math.Ceiling(TotalCount / (double)PageSize);
                this.AddRange(lista.Skip(PageIndex).Take(PageSize));

                TotalRegistros = TotalCount;
            }
            catch { }
        }

        public PaginatedData(List<T> source, Int32 pageIndex, Int32 pageSize, ref Int32 TotalRegistros, bool faznada)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = 0;
            TotalPages = 0;

            try
            {
                TotalCount = source.Count();
                if (PageSize == -1)
                {
                    PageSize = TotalCount;
                }

                TotalPages = (Int32)Math.Ceiling(TotalCount / (double)PageSize);

                this.AddRange(source.Skip(PageIndex).Take(PageSize));

                TotalRegistros = TotalCount;
            }
            catch (System.NotSupportedException ex)
            {
                Debug.WriteLine(ex.Message);

            }
            catch (Exception e)
            {
                string x = e.ToString();

            }
        }
    }
}
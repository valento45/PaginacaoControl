using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaginacaoControl.BusinessEntities.Interfaces
{
    public interface IPaginacaoControl
    {

        int PageCount { get; set; }

        int TotalItemCount { get; }

        int PageNumber { get; set; }

        int PageSize { get; set; }

        bool HasPreviousPage { get; set; }

        bool HasNextPage { get; set; }

        bool IsFirstPage { get; set; }

        bool IsLastPage { get; set; }

        int FirstItemOnPage { get; set; }

        int LastItemOnPage { get; set; }

       protected void ProximaPagina();
       protected void RetrocederPagina();
       protected void PrimeiraPagina();
       protected void UltimaPagina();
    }
}

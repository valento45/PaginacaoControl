using PaginacaoControl.BusinessEntities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaginacaoControl.BusinessEntities.Objetos
{
    /// <summary>
    /// Classe para paginacao de objetos do tipo genérico T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PaginacaoControl<T> : IPaginacaoControl
    {

        public PaginacaoControl()
        {

        }

        public PaginacaoControl(IEnumerable<T> itens, int quantidadePorPagina)
        {
            _itens = itens;
            PageSize = quantidadePorPagina;
            ItensFormatados = new List<T>();
        }


        /// <summary>
        /// Quantidade de paginas
        /// </summary>
        public int PageCount { get; set; }

        public int TotalItemCount
        {
            get
            {
                return _itens.Count();
            }
        }

        /// <summary>
        /// Numero atual da pagina
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Tamanho de pagina ( quantidade de itens por pagina )
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Flag indica se retrocederá página
        /// </summary>
        public bool HasPreviousPage { get; set; }

        /// <summary>
        ///  Flag indica avanço de página
        /// </summary>
        public bool HasNextPage { get; set; }

        /// <summary>
        ///  Flag indica se vai para primeira pagina
        /// </summary>
        public bool IsFirstPage { get; set; }

        /// <summary>
        ///  Flag indica se vai para ultima pagina
        /// </summary>
        public bool IsLastPage { get; set; }

        /// <summary>
        /// Primeiro item da pagina atual
        /// </summary>
        public int FirstItemOnPage { get; set; }

        /// <summary>
        /// Ultimo item da pagina atual
        /// </summary>
        public int LastItemOnPage { get; set; }

        /// <summary>
        /// Itens formatados com aplicação da paginação para exibir na pagina
        /// </summary>
        public IEnumerable<T> ItensFormatados { get; set; }


        /// <summary>
        /// Todos os itens ( Sem aplicação da paginação )
        /// </summary>
        private IEnumerable<T> _itens { get; set; }

        public void PrimeiraPagina()
        {
            PageNumber = 1;
            FirstItemOnPage = 0;

            ItensFormatados = _itens?.Skip(FirstItemOnPage)?.Take(PageSize) ?? ItensFormatados;

            LastItemOnPage = ItensFormatados.Count() > PageSize ? PageSize : ItensFormatados.Count();
        }

        public void ProximaPagina()
        {

            PageNumber = PageNumber < PageCount ? PageNumber + 1 : PageCount;


            FirstItemOnPage = (FirstItemOnPage + PageSize) < _itens.Count() ? (FirstItemOnPage + PageSize) : FirstItemOnPage;
            LastItemOnPage = (FirstItemOnPage + PageSize) > _itens.Count() ? _itens.Count() : (FirstItemOnPage + PageSize);

            ItensFormatados = _itens?.Skip(FirstItemOnPage)?.Take(PageSize) ?? ItensFormatados;
        }

        public void RetrocederPagina()
        {
            if (PageNumber == 1)
            {
                FirstItemOnPage = 0;
                LastItemOnPage = _itens.Count() > PageSize ? PageSize : _itens.Count();
            }
            else
            {
                PageNumber--;
                LastItemOnPage = LastItemOnPage - PageSize;
                FirstItemOnPage = FirstItemOnPage - PageSize;
            }

            ItensFormatados = _itens?.Skip(FirstItemOnPage)?.Take(PageSize) ?? ItensFormatados;
        }

        public void UltimaPagina()
        {
            PageNumber = PageCount;
            LastItemOnPage = _itens.Count();
            FirstItemOnPage = LastItemOnPage > PageSize ? LastItemOnPage - PageSize : 0;

            ItensFormatados = _itens?.Skip(FirstItemOnPage)?.Take(PageSize) ?? ItensFormatados;
        }

        /// <summary>
        /// Atualiza os dados da lista de paginacao
        /// </summary>
        public void Refresh()
        {
            if (HasNextPage)
            {
                ProximaPagina();
                HasNextPage = false;
                return;
            }
            else if (HasPreviousPage)
            {
                RetrocederPagina();
                HasPreviousPage = false;
                return;
            }
            else if (IsLastPage)
            {
                UltimaPagina();
                IsLastPage = false;
                return;
            }
            else if (IsFirstPage)
            {
                PrimeiraPagina();
                IsFirstPage = false;
                return;
            }

            PrimeiraPagina();

        }
    }
}

// <copyright>
// Copyright (c) 2022 All Rights Reserved
// </copyright>
// <author>Sabyasachi Senapati</author>

using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Shared.Models.Responses
{
    public class PaginatedResponse<T> : ICloneable where T : ICloneable
    {
        public PaginatedResponse()
        {

        }
        /// <summary>
        /// Informações
        /// </summary>
        public IEnumerable<T> Data { get; set; }

        /// <summary>
        /// Índice da pagina
        /// </summary>
        public int PageIndex { get; private set; }

        /// <summary>
        /// Total de páginas
        /// </summary>
        public int TotalPages { get; private set; }

        /// <summary>
        /// Total de itens
        /// </summary>
        public int TotalItems { get; private set; }

        public PaginatedResponse(
            List<T> items, 
            int count, 
            int pageIndex, 
            int pageSize
        )
        {
            PageIndex = pageIndex;
            TotalItems = count;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            Data = items;
        }

        /// <summary>
        /// Tem página anterior?
        /// </summary>
        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        /// <summary>
        /// Tem próxima página?
        /// </summary>
        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        /// <summary>
        /// Cria busca
        /// </summary>
        /// <param name="source"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static PaginatedResponse<T> Create(
            IList<T> source, 
            int pageIndex,
            int pageSize
        )
        {
            var count = source.Count;
            var items = source.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PaginatedResponse<T>(
                items, 
                count, 
                pageIndex, 
                pageSize
            );
        }

        public object Clone()
        {
            var paginated = (PaginatedResponse<T>)MemberwiseClone();
            var t = new List<T>();
            paginated.Data.ToList().ForEach(p => t.Add((T)p.Clone()));
            paginated.Data = t;
            return paginated;
        }

        public PaginatedResponse<T> CloneTyped()
        {
            return (PaginatedResponse<T>)Clone();
        }
    }
}

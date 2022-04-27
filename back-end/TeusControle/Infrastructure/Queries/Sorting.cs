// <copyright>
// Copyright (c) 2022 All Rights Reserved
// </copyright>
// <author>Sabyasachi Senapati</author>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TeusControle.Infrastructure.Dtos;
using TeusControle.Infrastructure.Enums;

namespace TeusControle.Infrastructure.Queries
{
    public class Sorting<T>
    {
        /// <summary>
        /// Actual grouping will be done in ui,   
        /// from api we will send sorted data based on grouping columns  
        /// </summary>  
        /// <param name="data"></param>  
        /// <param name="groupingColumns"></param>  
        /// <returns></returns>  
        public static IEnumerable<T> GroupingData(
            IEnumerable<T> data,
            IEnumerable<string> groupingColumns
        )
        {
            IOrderedEnumerable<T> groupedData = null;

            foreach (string grpCol in groupingColumns.Where(x => !String.IsNullOrEmpty(x)))
            {
                var col = typeof(T).GetProperty(
                    grpCol, 
                    BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public
                );
                
                if (col != null)
                {
                    groupedData = groupedData == null 
                        ? data.OrderBy(x => col.GetValue(x, null))
                        : groupedData.ThenBy(x => col.GetValue(x, null));
                }
            }

            return groupedData ?? data;
        }

        /// <summary>
        ///  Sorting the data on multiple columns using reflection
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sortingParams"></param>
        /// <returns></returns>
        public static IEnumerable<T> SortData(
            IEnumerable<T> data, 
            IEnumerable<SortingParams> sortingParams
        )
        {
            IOrderedEnumerable<T> sortedData = null;
            
            foreach (var sortingParam in sortingParams.Where(x => !String.IsNullOrEmpty(x.ColumnName)))
            {
                var col = typeof(T).GetProperty(
                    sortingParam.ColumnName, 
                    BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public
                );

                if (col != null)
                {
                    sortedData = sortedData == null 
                        ? sortingParam.SortOrder == SortEnum.Asc 
                            ? data.OrderBy(x => col.GetValue(x, null))
                            : data.OrderByDescending(x => col.GetValue(x, null))
                        : sortingParam.SortOrder == SortEnum.Asc 
                            ? sortedData.ThenBy(x => col.GetValue(x, null))
                            : sortedData.ThenByDescending(x => col.GetValue(x, null));
                }
            }
            return sortedData ?? data;
        }
    }
}

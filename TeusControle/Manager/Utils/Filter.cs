// <copyright>
// Copyright (c) 2022 All Rights Reserved
// </copyright>
// <author>Sabyasachi Senapati</author>

using Core.Shared.Models.Enums;
using Core.Shared.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Manager.Utils
{
    /// <summary>
    /// Classe genérica responsável por filtrar informações
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Filter<T>
    {
        public static IEnumerable<T> FilteredData(
            IEnumerable<FilterParamsRequest> filterParams,
            IEnumerable<T> data
        )
        {

            IEnumerable<string> distinctColumns = filterParams.Where(x => !String.IsNullOrEmpty(x.ColumnName))
                .Select(x => x.ColumnName)
                .Distinct();

            foreach (string colName in distinctColumns)
            {
                var filterColumn = typeof(T).GetProperty(
                    colName, 
                    BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public
                );
                if (filterColumn != null)
                {
                    IEnumerable<FilterParamsRequest> filterValues = filterParams
                        .Where(x => x.ColumnName.Equals(colName))
                        .Distinct();

                    if (filterValues.Count() > 1)
                    {
                        IEnumerable<T> sameColData = Enumerable.Empty<T>();

                        foreach (var val in filterValues)
                        {
                            sameColData = sameColData.Concat(FilterData(
                                val.FilterOption, 
                                data, 
                                filterColumn, 
                                val.FilterValue
                            ));
                        }

                        data = data.Intersect(sameColData);
                    }
                    else
                    {
                        data = FilterData(
                            filterValues.FirstOrDefault().FilterOption,
                            data,
                            filterColumn,
                            filterValues.FirstOrDefault().FilterValue
                        );
                    }
                }
            }
            return data;
        }

        private static IEnumerable<T> FilterData(
            FilterEnum filterOption, 
            IEnumerable<T> data,
            PropertyInfo filterColumn, 
            string filterValue
        )
        {
            int outValue;
            DateTime dateValue;
            switch (filterOption)
            {
                #region [StringDataType]  

                case FilterEnum.StartsWith:
                    data = data.Where(x => 
                        filterColumn.GetValue(x, null) != null && 
                        filterColumn.GetValue(x, null).ToString().ToLower().StartsWith(
                            filterValue.ToString().ToLower()
                        )
                    ).ToList();
                    break;
                case FilterEnum.EndsWith:
                    data = data.Where(x => 
                        filterColumn.GetValue(x, null) != null && 
                        filterColumn.GetValue(x, null).ToString().ToLower().EndsWith(
                            filterValue.ToString().ToLower()
                        )
                    ).ToList();
                    break;
                case FilterEnum.Contains:
                    data = data.Where(x => 
                        filterColumn.GetValue(x, null) != null && 
                        filterColumn.GetValue(x, null).ToString().ToLower().Contains(
                            filterValue.ToString().ToLower()
                        )
                     ).ToList();
                    break;
                case FilterEnum.DoesNotContain:
                    data = data.Where(x => 
                        filterColumn.GetValue(x, null) == null || (
                            filterColumn.GetValue(x, null) != null &&
                            !filterColumn.GetValue(x, null).ToString().ToLower().Contains(
                                filterValue.ToString().ToLower()
                            )
                        )
                    ).ToList();
                    break;
                case FilterEnum.IsEmpty:
                    data = data.Where(x => 
                        filterColumn.GetValue(x, null) == null || (
                            filterColumn.GetValue(x, null) != null && 
                            filterColumn.GetValue(x, null).ToString() == string.Empty
                        )
                    ).ToList();
                    break;
                case FilterEnum.IsNotEmpty:
                    data = data.Where(x => 
                        filterColumn.GetValue(x, null) != null && 
                        filterColumn.GetValue(x, null).ToString() != string.Empty
                    ).ToList();
                    break;
                #endregion

                #region [Custom]  

                case FilterEnum.IsGreaterThan:
                    if ((
                        filterColumn.PropertyType == typeof(Int32) || 
                        filterColumn.PropertyType == typeof(Nullable<Int32>)) && 
                        Int32.TryParse(filterValue, out outValue)
                    )
                    {
                        data = data.Where(x => 
                            Convert.ToInt32(filterColumn.GetValue(x, null)) > outValue
                        ).ToList();
                    }
                    else if ((
                        filterColumn.PropertyType == typeof(Nullable<DateTime>)) && 
                        DateTime.TryParse(filterValue, out dateValue)
                    )
                    {
                        data = data.Where(x => 
                            Convert.ToDateTime(filterColumn.GetValue(x, null)) > dateValue
                        ).ToList();
                    }
                    break;

                case FilterEnum.IsGreaterThanOrEqualTo:
                    if ((
                        filterColumn.PropertyType == typeof(Int32) || 
                        filterColumn.PropertyType == typeof(Nullable<Int32>)) && 
                        Int32.TryParse(filterValue, out outValue)
                    )
                    {
                        data = data.Where(x => 
                            Convert.ToInt32(filterColumn.GetValue(x, null)) >= outValue
                        ).ToList();
                    }
                    else if ((
                        filterColumn.PropertyType == typeof(Nullable<DateTime>)) &&
                        DateTime.TryParse(filterValue, out dateValue)
                    )
                    {
                        data = data.Where(x => 
                            Convert.ToDateTime(filterColumn.GetValue(x, null)) >= dateValue
                        ).ToList();
                        break;
                    }
                    break;

                case FilterEnum.IsLessThan:
                    if ((
                        filterColumn.PropertyType == typeof(Int32) || 
                        filterColumn.PropertyType == typeof(Nullable<Int32>)) && 
                        Int32.TryParse(filterValue, out outValue)
                    )
                    {
                        data = data.Where(x => 
                            Convert.ToInt32(filterColumn.GetValue(x, null)) < outValue
                        ).ToList();
                    }
                    else if ((
                        filterColumn.PropertyType == typeof(Nullable<DateTime>)) &&
                        DateTime.TryParse(filterValue, out dateValue)
                    )
                    {
                        data = data.Where(x => 
                            Convert.ToDateTime(filterColumn.GetValue(x, null)) < dateValue
                        ).ToList();
                        break;
                    }
                    break;

                case FilterEnum.IsLessThanOrEqualTo:
                    if ((
                        filterColumn.PropertyType == typeof(Int32) ||
                        filterColumn.PropertyType == typeof(Nullable<Int32>)) &&
                        Int32.TryParse(filterValue, out outValue)
                    )
                    {
                        data = data.Where(x => 
                            Convert.ToInt32(filterColumn.GetValue(x, null)) <= outValue
                        ).ToList();
                    }
                    else if ((
                        filterColumn.PropertyType == typeof(Nullable<DateTime>)) && 
                        DateTime.TryParse(filterValue, out dateValue)
                    )
                    {
                        data = data.Where(x => 
                            Convert.ToDateTime(filterColumn.GetValue(x, null)) <= dateValue
                        ).ToList();
                        break;
                    }
                    break;

                case FilterEnum.IsEqualTo:
                    if (filterValue == string.Empty)
                    {
                        data = data.Where(x => 
                            filterColumn.GetValue(x, null) == null || (
                                filterColumn.GetValue(x, null) != null && 
                                filterColumn.GetValue(x, null).ToString().ToLower() == string.Empty
                            )
                        ).ToList();
                    }
                    else
                    {
                        if ((
                            filterColumn.PropertyType == typeof(Int32) || 
                            filterColumn.PropertyType == typeof(Nullable<Int32>)) && 
                            Int32.TryParse(filterValue, out outValue)
                        )
                        {
                            data = data.Where(x => 
                                Convert.ToInt32(filterColumn.GetValue(x, null)) == outValue
                            ).ToList();
                        }
                        else if ((
                            filterColumn.PropertyType == typeof(Nullable<DateTime>)) &&
                            DateTime.TryParse(filterValue, out dateValue)
                        )
                        {
                            data = data.Where(x => 
                                Convert.ToDateTime(filterColumn.GetValue(x, null)) == dateValue
                            ).ToList();
                            break;
                        }
                        else
                        {
                            data = data.Where(x => 
                                filterColumn.GetValue(x, null) != null && 
                                filterColumn.GetValue(x, null).ToString().ToLower() == filterValue.ToLower()
                            ).ToList();
                        }
                    }
                    break;

                case FilterEnum.IsNotEqualTo:
                    if ((
                        filterColumn.PropertyType == typeof(Int32) ||
                        filterColumn.PropertyType == typeof(Nullable<Int32>)) && 
                        Int32.TryParse(filterValue, out outValue)
                    )
                    {
                        data = data.Where(x => 
                            Convert.ToInt32(filterColumn.GetValue(x, null)) != outValue
                        ).ToList();
                    }
                    else if ((
                        filterColumn.PropertyType == typeof(Nullable<DateTime>)) &&
                        DateTime.TryParse(filterValue, out dateValue)
                    )
                    {
                        data = data.Where(x => 
                            Convert.ToDateTime(filterColumn.GetValue(x, null)) != dateValue
                        ).ToList();
                        break;
                    }
                    else
                    {
                        data = data.Where(x => 
                            filterColumn.GetValue(x, null) == null || (
                                filterColumn.GetValue(x, null) != null &&
                                filterColumn.GetValue(x, null).ToString().ToLower() != filterValue.ToLower()
                            )
                        ).ToList();

                    }
                    break;
                    #endregion
            }
            return data;
        }
    }
}

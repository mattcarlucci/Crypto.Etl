// ***********************************************************************
// Assembly         : Crypto.Utils
// Author           : mcarlucci
// Created          : 03-11-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-11-2018
// ***********************************************************************
// <copyright file="JsonEtl.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;

namespace Crypto.Utils
{
    /// <summary>
    /// Class CsvFile.
    /// </summary>
    /// <seealso cref="Crypto.Utils.OutputEtl" />
    public class JsonEtl : OutputEtl
    {       
        /// <summary>
        /// Save as CSV.
        /// </summary>
        /// <param name="model">The model.</param>
        public override void ToCsv(UrlModel model)
        {  
            var content = model.GetUrlData();
            var dataSet = Transform(content);
                        
            TransformTime(dataSet);
            TransformData(dataSet, model);          
        }

        /// <summary>
        /// Saves the file.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="model">The model.</param>
        /// <param name="file">The file.</param>
        private void SaveFile(DataTable table, UrlModel model, string file)
        {
            var lines = new List<string>();
           
            var header = string.Join(",", table.ColumnNames());
            lines.Add(header);

            var values = table.AsEnumerable().Select(row => FormatRow(row));
            lines.AddRange(values);

            UrlModel.DeleteFile(file);
            File.WriteAllLines(file, lines);
        }

        /// <summary>
        /// Transforms the data.
        /// </summary>
        /// <param name="ds">The ds.</param>
        /// <param name="model">The model.</param>
        private void TransformData(DataSet ds, UrlModel model)
        {
            List<DataTable> tables = ds.Tables.Cast<DataTable>().ToList();
            var results = tables.GroupBy(g => GroupByUnique(g)).ToList();

            foreach (var result in results)
            {
                for (int i = 1; i < result.Count(); i++)
                {
                    result.First().Merge(result.Skip(i).Take(1).SingleOrDefault());
                }
                var file = string.Format("{0}_{1}.csv", model.GetRootFileName(""), result.Key);
                SaveFile(result.First(), model, file);
            }
        }
        /// <summary>
        /// Groups the by unique.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <returns>System.String.</returns>
        private static string GroupByUnique(DataTable g)
        {
            return g.PrimaryKey.Count() > 0
                     ? g.PrimaryKey.First().ColumnName
                     : g.Columns.Cast<DataColumn>().
                         First().ColumnName;
        }
        /// <summary>
        /// Formats the row.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <returns>System.String.</returns>
        private static string FormatRow(DataRow row)
        {
            return "\"" + string.Join("\",\"", row.ItemArray.Select(s => FormatCol(s))) + "\"";
        }
        /// <summary>
        /// Formats the col.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        private static string FormatCol(object value)
        {
            return value.ToString().Replace("\"", "\"\"");
        }
    }



}

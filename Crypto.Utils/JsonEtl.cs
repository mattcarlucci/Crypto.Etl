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
        /// To the CSV.
        /// </summary>
        /// <param name="content">Content of the json.</param>
        /// <param name="file">The file.</param>
        public override void ToCsv(string content, string file)
        {
            var dataSet = Transform(content);
            var dataTable = Transform(dataSet);

           
                      
            var lines = new List<string>();
            string[] columnNames = dataTable.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName).
                                              ToArray();
            var header = string.Join(",", columnNames);
            lines.Add(header);
            var valueLines = dataTable.AsEnumerable()
                               .Select(row => string.Join(",", row.ItemArray));
            lines.AddRange(valueLines);
            File.WriteAllLines(file, lines);
        }
       
    }



}

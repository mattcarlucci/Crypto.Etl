﻿// ***********************************************************************
// Assembly         : Crypto.Utils
// Author           : mcarlucci
// Created          : 03-11-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-11-2018
// ***********************************************************************
// <copyright file="OutputEtl.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml;

namespace Crypto.Utils
{
    /// <summary>
    /// Class OutputEtl.
    /// </summary>
    public abstract class OutputEtl
    {             
        /// <summary>
        /// To the CSV.
        /// </summary>
        /// <param name="model">The model.</param>
        public abstract void ToCsv(UrlModel model);
        
        /// <summary>
        /// To the data set.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>DataSet.</returns>
        protected virtual DataSet Transform(string content)
        {
            XmlNode xml = JsonConvert.DeserializeXmlNode("{records:{record:" + content + "}}");
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(xml.InnerXml);

            using (XmlReader xmlReader = new XmlNodeReader(xml))
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXml(xmlReader);
                return dataSet;
            }
        }

        #region Private Static Methods
        /// <summary>
        /// Fixes the time.
        /// </summary>
        /// <param name="ds">The ds.</param>
        protected static void TransformTime(DataSet ds)
        {
            string[] dateFields = { "time", "created_on", "last_updated", "lastupdate", "date", "enddate" };

            foreach (DataTable table in ds.Tables)
            {
                var cols = table.Columns.Cast<DataColumn>().Select(column => column.ColumnName).
                      Where(w => dateFields.Contains(w.ToLower())).ToArray();

                foreach (var col in cols)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        var value = row[col] as string;
                        if (string.IsNullOrEmpty(value)) continue;
                        if (long.TryParse(value, out long result))
                        {
                            var dt = UnixTimeStampToDateTime(result);
                            row[col] = dt.ToString();
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// Unixes the time stamp to date time.
        /// </summary>
        /// <param name="unixTimeStamp">The unix time stamp.</param>
        /// <returns>DateTime.</returns>
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
        /// <summary>
        /// Unixes the time stamp to date time.
        /// </summary>
        /// <param name="unixTimeStamp">The unix time stamp.</param>
        /// <returns>DateTime.</returns>
        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            return UnixTimeStampToDateTime((double)unixTimeStamp);
        }
        #endregion
    }



}

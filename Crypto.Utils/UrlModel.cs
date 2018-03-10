// ***********************************************************************
// Assembly         : Crypto.Utils
// Author           : mcarlucci
// Created          : 03-10-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-10-2018
// ***********************************************************************
// <copyright file="UrlModel.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;

namespace Crypto.Utils
{
    /// <summary>
    /// Class UrlModel.
    /// </summary>
    public class UrlModel
    {
        /// <summary>
        /// Gets or sets the s URL.
        /// </summary>
        /// <value>The s URL.</value>
        public string sUrl { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlModel" /> class.
        /// </summary>
        /// <param name="url">The URL.</param>
        public UrlModel(string url)
        {
            this.sUrl = url;
        }

        /// <summary>
        /// Gets the query element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>System.String.</returns>
        public string GetQueryElement(string element)
        {
            var uriBuilder = new UriBuilder(sUrl);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            return query[element];
        }
        /// <summary>
        /// Gets the query element.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>System.String.</returns>
        public string GetQueryElement(int index)
        {
            var uriBuilder = new UriBuilder(sUrl);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            return query[index];
        }
        /// <summary>
        /// Sets the query element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public void SetQueryElement(string element, string value)
        {
            var uriBuilder = new UriBuilder(sUrl);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query[element] = value;
            uriBuilder.Query = query.ToString();
            sUrl = uriBuilder.ToString();
        }

        /// <summary>
        /// Gets the URL path.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetUrlPath()
        {
            var uriBuilder = new UriBuilder(sUrl);
            return uriBuilder.Path;
        }
        /// <summary>
        /// Gets the type of the URL.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetUrlType()
        {
            var uriBuilder = new UriBuilder(sUrl);
            return uriBuilder.Path.
                Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).
                Last();
        }
        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <param name="extension">The extension.</param>
        /// <returns>System.String.</returns>
        public string GetFileName(string extension = ".csv")
        {

            var uriBuilder = new UriBuilder(sUrl);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            StringBuilder sb = new StringBuilder();
            List<string> values = new List<string>();

            values.Add(GetUrlType());
            foreach (var key in query.AllKeys)
            {
                values.Add(query[key]);

            }
            return string.Join("_", values).ToFileName() + extension;
        }
        /// <summary>
        /// Gets the URL data.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetUrlData()
        {
            using (WebClient wc = new WebClient())
            {
                return wc.DownloadString(sUrl);
            }
        }
    }

    /// <summary>
    /// Class CryptoCompareUrl.
    /// </summary>
    /// <seealso cref="Crypto.Utils.UrlModel" />
    public class CryptoCompareUrl : UrlModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CryptoCompareUrl" /> class.
        /// </summary>
        /// <param name="url">The URL.</param>
        public CryptoCompareUrl(string url) : base(url)
        {
        }


        /// <summary>
        /// Gets or sets from symbol.
        /// </summary>
        /// <value>From symbol.</value>
        public string FromSymbol
        {
            get
            {
                return GetQueryElement("fsym");
            }
            set
            {
                SetQueryElement("fsym", value);
            }
        }
        /// <summary>
        /// Gets or sets from symbols.
        /// </summary>
        /// <value>From symbols.</value>
        public string FromSymbols
        {
            get
            {
                return GetQueryElement("fsyms");
            }
            set
            {
                SetQueryElement("fsyms", value);
            }
        }
        /// <summary>
        /// Gets or sets to symbol.
        /// </summary>
        /// <value>To symbol.</value>
        public string ToSymbol
        {
            get
            {
                return GetQueryElement("tsym");
            }
            set
            {
                SetQueryElement("tSym", value);
            }
        }
        /// <summary>
        /// Gets or sets to symbos.
        /// </summary>
        /// <value>To symbos.</value>
        public string ToSymbos
        {
            get
            {
                return GetQueryElement("tsyms");
            }
            set
            {
                SetQueryElement("tSyms", value);
            }
        }

        /// <summary>
        /// Gets or sets the exchange.
        /// </summary>
        /// <value>The exchange.</value>
        public string Exchange
        {
            get
            {
                return GetQueryElement("exchange");
            }
            set
            {
                SetQueryElement("exchange", value);
            }
        }
        /// <summary>
        /// Gets or sets the limit.
        /// </summary>
        /// <value>The limit.</value>
        public string Limit
        {
            get
            {
                return GetQueryElement("limit");
            }
            set
            {
                SetQueryElement("limit", value);
            }

        }
        #region Lists
        //public List<string> FromSymbols
        //{
        //    get
        //    {
        //        return GetQueryElement("fsyms").
        //            Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).
        //            ToList();
        //    }
        //    set
        //    {
        //        SetQueryElement("fsym", string.Join(",", value));
        //    }
        //}
        //public List<string> ToSymbols
        //{
        //    get
        //    {
        //        return GetQueryElement("tsyms").
        //            Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).
        //            ToList();
        //    }
        //    set
        //    {
        //        SetQueryElement("tsyms", string.Join(",", value));
        //    }
        //}
        #endregion
    }

    /// <summary>
    /// Class GobalExtensions.
    /// </summary>
    public static class GobalExtensions
    {
        /// <summary>
        /// To the name of the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>System.String.</returns>
        public static string ToFileName(this string file)
        {
            var result = Path.GetInvalidFileNameChars().
            Aggregate(file, (current, c) => current.Replace(c.ToString(), "@"));

            return result;
        }
    }

    /// <summary>
    /// Class UrlFileManager.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.List{System.String}" />
    public class UrlFileManager : List<string>
    {
        /// <summary>
        /// The file
        /// </summary>
        public string File { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlFileManager"/> class.
        /// </summary>
        /// <param name="file">The file.</param>
        public UrlFileManager(string file)
        {
            this.File = file;          
            Load();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlFileManager"/> class.
        /// </summary>
        public UrlFileManager()
        {
        }

        /// <summary>
        /// Gets the urls.
        /// </summary>
        /// <value>The urls.</value>
        public System.Collections.Generic.IEnumerable<string> Urls
        {
            get
            {
                foreach (var item in this)
                {
                    if (item.Trim().Length > 0 && item.Trim().StartsWith("#") == false)
                        yield return item;
                }
            }
        }
        /// <summary>
        /// Loads this instance.
        /// </summary>
        public void Load()
        {
            Clear();
            AddRange(System.IO.File.ReadAllLines(File));
        }
    }

    public abstract class OutputEtl
    {
        /// <summary>
        /// Transforms the specified ds.
        /// </summary>
        /// <param name="ds">The ds.</param>
        /// <returns>System.Data.DataTable.</returns>
        protected virtual DataTable Transform(DataSet ds)
        {
            if (ds.Tables.Count == 1)
                return ds.Tables[0];

            return ds.Tables[1];
        }
        public abstract void ToCsv(string jsonContent, string file);

        #region Private Static Methods
        /// <summary>
        /// Fixes the time.
        /// </summary>
        /// <param name="ds">The ds.</param>
        protected static void FixTime(DataSet ds)
        {
            foreach (DataTable table in ds.Tables)
            {
                foreach (DataColumn col in table.Columns)
                {
                    if (col.ColumnName == "time" || col.ColumnName == "created_on" || col.ColumnName == "last_updated")
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            var time = row[col.ColumnName] as string;
                            if (string.IsNullOrEmpty(time)) continue;

                            var dt = UnixTimeStampToDateTime(int.Parse(time));
                            row[col.ColumnName] = dt.ToString();
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
        public static DateTime UnixTimeStampToDateTime(int unixTimeStamp)
        {
            return UnixTimeStampToDateTime((double)unixTimeStamp);
        }
        #endregion
    }


    /// <summary>
    /// Class CsvFile.
    /// </summary>
    public class JsonEtl : OutputEtl
    {

        /// <summary>
        /// To the CSV.
        /// </summary>
        /// <param name="jsonContent">Content of the json.</param>
        /// <param name="file">The file.</param>
        public override void ToCsv(string jsonContent, string file)
        {
            //used NewtonSoft json nuget package
            XmlNode xml = JsonConvert.DeserializeXmlNode("{records:{record:" + jsonContent + "}}");
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(xml.InnerXml);
            XmlReader xmlReader = new XmlNodeReader(xml);
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(xmlReader);
            var dataTable = Transform(dataSet);
            FixTime(dataSet);

            //Datatable to CSV
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

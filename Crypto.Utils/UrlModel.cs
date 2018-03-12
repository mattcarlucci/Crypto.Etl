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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

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
            return uriBuilder.Path.List('/').Last();
        }
        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <param name="extension">The extension.</param>
        /// <returns>System.String.</returns>
        public string GetRootFileName(string extension = ".csv")
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
        /// Deletes the file.
        /// </summary>
        /// <param name="file">The file.</param>
        public static void DeleteFile(string file)
        {
            if (File.Exists(file))
                File.Delete(file);           
        }
        /// <summary>
        /// Deletes the file.
        /// </summary>
        public void DeleteFile()
        {
            DeleteFile(GetRootFileName());
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
}

// ***********************************************************************
// Assembly         : Crypto.Convert.Tests
// Author           : mcarlucci
// Created          : 03-10-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-10-2018
// ***********************************************************************
// <copyright file="UnitTest1.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.IO;
using System.Linq;
using Crypto.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Crypto.Convert.Tests
{
    /// <summary>
    /// Class UnitTest1.
    /// </summary>
    [TestClass]
    public class UnitTest1
    {

        /// <summary>
        /// The URL
        /// </summary>
        const string URL = "https://min-api.cryptocompare.com/data/top/pairs?fsym=ACT*&limit=100";

        /// <summary>
        /// Tests the URL manager.
        /// </summary>
        [TestMethod]
        public void test_url_manager()
        {
            UrlFileManager manager = new UrlFileManager();
            manager.Add(URL);
            manager.Add("#this is a comment");

            Assert.AreEqual(manager.Urls.Count(), 1);            
        }
        /// <summary>
        /// Tests the type of the URL.
        /// </summary>
        [TestMethod]
        public void test_url_type()
        {
            UrlFileManager manager = new UrlFileManager();
            manager.Add(URL);

            CryptoCompareUrl model = new CryptoCompareUrl(manager.Urls.First());
            Assert.AreEqual(model.GetUrlType(), "pairs");
        }
        /// <summary>
        /// Tests the URL path.
        /// </summary>
        [TestMethod]
        public void test_url_path()
        {
            UrlFileManager manager = new UrlFileManager();
            manager.Add(URL);

            CryptoCompareUrl model = new CryptoCompareUrl(manager.Urls.First());
            Assert.AreEqual(model.GetUrlPath(), "/data/top/pairs");
        }
        /// <summary>
        /// Tests the URL filename.
        /// </summary>
        [TestMethod]
        public void test_url_filename()
        {
            UrlFileManager manager = new UrlFileManager();
            manager.Add(URL);

            CryptoCompareUrl model = new CryptoCompareUrl(manager.Urls.First());
            Assert.AreEqual(model.GetRootFileName(), "pairs_ACT@_100.csv");
        }

        /// <summary>
        /// Tests the URL data.
        /// </summary>
        [TestMethod]
        public void test_url_data()
        {
            UrlFileManager manager = new UrlFileManager();
            manager.Add(URL);

            CryptoCompareUrl model = new CryptoCompareUrl(manager.Urls.First());
            Assert.IsTrue(model.GetUrlData().StartsWith("{\"Response\":\"Success\""));           
        }
        /// <summary>
        /// Tests the output file.
        /// </summary>
        [TestMethod]
        public void test_output_files()
        {
            UrlFileManager manager = new UrlFileManager();
            manager.Add(URL);

            CryptoCompareUrl model = new CryptoCompareUrl(manager.Urls.First());
            JsonEtl etl = new JsonEtl();
            etl.ToCsv(model);

            Assert.IsTrue(File.Exists("pairs_ACT@_100_record_Id.csv"));
            Assert.IsTrue(File.Exists("pairs_ACT@_100_exchange.csv"));
           

        }
    }
}

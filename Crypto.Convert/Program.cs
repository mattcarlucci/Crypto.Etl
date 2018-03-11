// ***********************************************************************
// Assembly         : Crypto.Convert
// Author           : mcarlucci
// Created          : 03-10-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-10-2018
// ***********************************************************************
// <copyright file="Program.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using Crypto.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Convert
{
    /// <summary>
    /// Class Program.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {     
            UrlFileManager manager = new UrlFileManager("UrlList.txt");

            Console.WriteLine("Reading file {0}\r\n\tUrl Count: {1}\r\n{2}", 
                manager.File, manager.Urls.Count(), new String('*', 35));
           
            foreach(var url in manager.Urls)
            {
                try
                {
                    CryptoCompareUrl model = new CryptoCompareUrl(url);

                    Console.WriteLine("Processing Url {0}\r\n\tType: {1}\r\n\tFrom Symbol: {2}",
                        model.GetUrlPath(), model.GetUrlType(), model.FromSymbol);

                    var fileName = model.GetFileName();
                    var json = model.GetUrlData();

                    JsonEtl etl = new JsonEtl();
                    Console.WriteLine("\tOutput: {0}\r\n{1}", fileName, new String('*', 35));

                    model.DeleteFile();
                    etl.ToCsv(json, fileName);
                }
                catch(Exception ex)
                {
                    Console.Beep(500, 1000);
                    Console.WriteLine("Can't process url {0}\r\n{1}", url, ex.Message);
                    continue;
                }
            }
            Console.WriteLine("Finished processing urls");
            System.Threading.Thread.Sleep(1000 * 30);            
        }
    }
}

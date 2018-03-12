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
using System.IO;
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
            var urlFile = args.Count() > 0 && File.Exists(args[0]) ? args[0] : "UrlList.txt";

            UrlFileManager manager = new UrlFileManager(urlFile);

            Console.WriteLine("Reading file {0}\r\n\tUrl Count: {1}\r\n{2}", 
                manager.File, manager.Urls.Count(), new String('*', 35));
           
            foreach(var url in manager.Urls)
            {
                try
                {
                    CryptoCompareUrl model = new CryptoCompareUrl(url);

                    Console.WriteLine("Processing Url {0}\r\n\tType: {1}\r\n\tFrom Symbol: {2}",
                        model.GetUrlPath(), model.GetUrlType(), model.FromSymbol);
                    
                    OutputEtl etl = new JsonEtl();
                    Console.WriteLine("\tOutput: {0}\r\n{1}", model.GetRootFileName(), new String('*', 35));
                    etl.ToCsv(model);
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

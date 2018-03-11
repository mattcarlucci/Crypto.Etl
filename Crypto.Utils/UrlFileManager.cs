// ***********************************************************************
// Assembly         : Crypto.Utils
// Author           : mcarlucci
// Created          : 03-11-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-11-2018
// ***********************************************************************
// <copyright file="UrlFileManager.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace Crypto.Utils
{
    /// <summary>
    /// Class UrlFileManager.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.List{System.String}" />
    public class UrlFileManager : List<string>
    {
        /// <summary>
        /// The file
        /// </summary>
        /// <value>The file.</value>
        public string File { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlFileManager" /> class.
        /// </summary>
        /// <param name="file">The file.</param>
        public UrlFileManager(string file)
        {
            this.File = file;          
            Load();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlFileManager" /> class.
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



}

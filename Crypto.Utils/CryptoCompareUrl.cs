// ***********************************************************************
// Assembly         : Crypto.Utils
// Author           : mcarlucci
// Created          : 03-11-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-11-2018
// ***********************************************************************
// <copyright file="CryptoCompareUrl.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;


namespace Crypto.Utils
{
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
        /// Gets or sets to symbols.
        /// </summary>
        /// <value>To symbols.</value>
        public string ToSymbols
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
        //            List( ',').ToList();
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
        //              List( ',').ToList();
        //    }
        //    set
        //    {
        //        SetQueryElement("tsyms", string.Join(",", value));
        //    }
        //}
        #endregion
    }
}

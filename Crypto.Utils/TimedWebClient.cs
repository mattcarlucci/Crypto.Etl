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
using System.Net;

namespace Crypto.Utils
{
    public class TimedWebClient : WebClient
    {
        // Timeout in milliseconds, default = 600,000 msec
        public int Timeout { get; set; }

        public TimedWebClient()
        {
            this.Timeout = 1000 * 60;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var objWebRequest = base.GetWebRequest(address);
            objWebRequest.Timeout = this.Timeout;
            return objWebRequest;
        }
    }
}

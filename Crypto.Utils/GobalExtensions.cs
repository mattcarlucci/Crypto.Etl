// ***********************************************************************
// Assembly         : Crypto.Utils
// Author           : mcarlucci
// Created          : 03-11-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-11-2018
// ***********************************************************************
// <copyright file="GobalExtensions.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.IO;
using System.Linq;

namespace Crypto.Utils
{
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
        /// <summary>
        /// Splits the specified tokens.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="tokens">The tokens.</param>
        /// <returns>System.String[].</returns>
        public static string[] List(this string value, params string[] tokens)
        {
            return value.Split(tokens, StringSplitOptions.RemoveEmptyEntries);
        }
        /// <summary>
        /// Splits the specified tokens.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="tokens">The tokens.</param>
        /// <returns>System.String[].</returns>
        public static string[] List(this string value, params char[] tokens)
        {
            return value.Split(tokens, StringSplitOptions.RemoveEmptyEntries);
        }
    }



}

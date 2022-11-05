using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetIdentity.Models.DataModels
{
    /// <summary>
    /// class to define a SessionCache object
    /// </summary>
    public class SessionCache
    {
        /// <summary>
        /// proerty id
        /// </summary>
        /// <value>int</value>
        public string? id {get;set;}
        /// <summary>
        /// property value
        /// </summary>
        /// <value>byte[]</value>
        public byte[]? Value {get;set;}
        /// <summary>
        /// property ExpiresAtTime
        /// </summary>
        /// <value>DateTime</value>
        public DateTime ExpiresAtTime {get; set;}
        /// <summary>
        /// property SlidingExpirationInSeconds
        /// </summary>
        /// <value>int</value>
        public int SlidingExpirationInSeconds {get; set;}
        /// <summary>
        /// property AbsoluteExpiration
        /// </summary>
        /// <value>DateTime</value>
        public DateTime? AbsoluteExpiration {get; set;}
    }
}
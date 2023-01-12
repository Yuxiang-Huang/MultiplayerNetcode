//-----------------------------------------------------------------------------
// <auto-generated>
//     This file was generated by the C# SDK Code Generator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//-----------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Scripting;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Unity.Services.Relay.Http;



namespace Unity.Services.Relay.Models
{
    /// <summary>
    /// JoinCodeData model
    /// </summary>
    [Preserve]
    [DataContract(Name = "JoinCodeData")]
    public class JoinCodeData
    {
        /// <summary>
        /// Creates an instance of JoinCodeData.
        /// </summary>
        /// <param name="joinCode">The connecting player can use the join code with the &#x60;/join&#x60; endpoint to join the same Relay server as the host player who created the join code. The join code is case-insensitive.</param>
        [Preserve]
        public JoinCodeData(string joinCode)
        {
            JoinCode = joinCode;
        }

        /// <summary>
        /// The connecting player can use the join code with the &#x60;/join&#x60; endpoint to join the same Relay server as the host player who created the join code. The join code is case-insensitive.
        /// </summary>
        [Preserve]
        [DataMember(Name = "joinCode", IsRequired = true, EmitDefaultValue = true)]
        public string JoinCode{ get; }
    
        /// <summary>
        /// Formats a JoinCodeData into a string of key-value pairs for use as a path parameter.
        /// </summary>
        /// <returns>Returns a string representation of the key-value pairs.</returns>
        internal string SerializeAsPathParam()
        {
            var serializedModel = "";

            if (JoinCode != null)
            {
                serializedModel += "joinCode," + JoinCode;
            }
            return serializedModel;
        }

        /// <summary>
        /// Returns a JoinCodeData as a dictionary of key-value pairs for use as a query parameter.
        /// </summary>
        /// <returns>Returns a dictionary of string key-value pairs.</returns>
        internal Dictionary<string, string> GetAsQueryParam()
        {
            var dictionary = new Dictionary<string, string>();

            if (JoinCode != null)
            {
                var joinCodeStringValue = JoinCode.ToString();
                dictionary.Add("joinCode", joinCodeStringValue);
            }
            
            return dictionary;
        }
    }
}

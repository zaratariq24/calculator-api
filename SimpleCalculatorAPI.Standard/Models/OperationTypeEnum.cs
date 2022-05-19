// <copyright file="OperationTypeEnum.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
namespace SimpleCalculatorAPI.Standard.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using SimpleCalculatorAPI.Standard;
    using SimpleCalculatorAPI.Standard.Utilities;

    /// <summary>
    /// OperationTypeEnum.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OperationTypeEnum
    {
        /// <summary>
        /// SUM.
        /// </summary>
        [EnumMember(Value = "SUM")]
        SUM,

        /// <summary>
        /// SUBTRACT.
        /// </summary>
        [EnumMember(Value = "SUBTRACT")]
        SUBTRACT,

        /// <summary>
        /// MULTIPLY.
        /// </summary>
        [EnumMember(Value = "MULTIPLY")]
        MULTIPLY
    }
}
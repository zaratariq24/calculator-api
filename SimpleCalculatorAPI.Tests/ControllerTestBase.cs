// <copyright file="ControllerTestBase.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
namespace SimpleCalculatorAPI.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NUnit.Framework;
    using SimpleCalculatorAPI.Standard;
    using SimpleCalculatorAPI.Standard.Http.Client;
    using SimpleCalculatorAPI.Standard.Models;
    using SimpleCalculatorAPI.Tests.Helpers;

    /// <summary>
    /// ControllerTestBase Class.
    /// </summary>
    [TestFixture]
    public class ControllerTestBase
    {
        /// <summary>
        /// Assert precision.
        /// </summary>
        protected const double AssertPrecision = 0.1;

        /// <summary>
        /// Gets HttpCallBackHandler.
        /// </summary>
        internal HttpCallBack HttpCallBackHandler { get; private set; }

        /// <summary>
        /// Gets SimpleCalculatorAPIClient Client.
        /// </summary>
        protected SimpleCalculatorAPIClient Client { get; private set; }

        /// <summary>
        /// Set up the client.
        /// </summary>
        [OneTimeSetUp]
        public void SetUp()
        {
            this.HttpCallBackHandler = new HttpCallBack();
            this.Client = SimpleCalculatorAPIClient.CreateFromEnvironment().ToBuilder()
                .HttpCallBack(this.HttpCallBackHandler)
                .Build();
        }
    }
}
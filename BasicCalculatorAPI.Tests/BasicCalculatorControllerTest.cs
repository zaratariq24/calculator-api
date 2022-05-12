// <copyright file="BasicCalculatorControllerTest.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
namespace BasicCalculatorAPI.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Threading.Tasks;
    using BasicCalculatorAPI.Standard;
    using BasicCalculatorAPI.Standard.Controllers;
    using BasicCalculatorAPI.Standard.Exceptions;
    using BasicCalculatorAPI.Standard.Http.Client;
    using BasicCalculatorAPI.Standard.Http.Response;
    using BasicCalculatorAPI.Standard.Utilities;
    using BasicCalculatorAPI.Tests.Helpers;
    using Newtonsoft.Json.Converters;
    using NUnit.Framework;

    /// <summary>
    /// BasicCalculatorControllerTest.
    /// </summary>
    [TestFixture]
    public class BasicCalculatorControllerTest : ControllerTestBase
    {
        /// <summary>
        /// Controller instance (for all tests).
        /// </summary>
        private BasicCalculatorController controller;

        /// <summary>
        /// Setup test class.
        /// </summary>
        [OneTimeSetUp]
        public void SetUpDerived()
        {
            this.controller = this.Client.BasicCalculatorController;
        }

        /// <summary>
        /// Tests if the sum of two numbers is as expected..
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Test]
        public async Task TestAdditionTest()
        {
            // Parameters for the API call
            double x = 20;
            double y = 30;
            Standard.Models.OperationTypeEnum operation = ApiHelper.JsonDeserialize<Standard.Models.OperationTypeEnum>("\"SUM\"");

            // Perform API call
            double result = 0;
            try
            {
                result = await this.controller.CalculateAsync(x, y, operation);
            }
            catch (ApiException)
            {
            }

            // Test response code
            Assert.AreEqual(200, this.HttpCallBackHandler.Response.StatusCode, "Status should be 200");

            // Test whether the captured response is as we expected
            Assert.IsNotNull(result, "Result should exist");
            Assert.AreEqual("50.0", TestHelper.ConvertStreamToString(this.HttpCallBackHandler.Response.RawBody), "Response body should match exactly (string literal match)");
        }
    }
}
// <copyright file="BasicCalculatorController.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
namespace BasicCalculatorAPI.Standard.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using BasicCalculatorAPI.Standard;
    using BasicCalculatorAPI.Standard.Authentication;
    using BasicCalculatorAPI.Standard.Http.Client;
    using BasicCalculatorAPI.Standard.Http.Request;
    using BasicCalculatorAPI.Standard.Http.Request.Configuration;
    using BasicCalculatorAPI.Standard.Http.Response;
    using BasicCalculatorAPI.Standard.Utilities;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// BasicCalculatorController.
    /// </summary>
    public class BasicCalculatorController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasicCalculatorController"/> class.
        /// </summary>
        /// <param name="config"> config instance. </param>
        /// <param name="httpClient"> httpClient. </param>
        /// <param name="authManagers"> authManager. </param>
        /// <param name="httpCallBack"> httpCallBack. </param>
        internal BasicCalculatorController(IConfiguration config, IHttpClient httpClient, IDictionary<string, IAuthManager> authManagers, HttpCallBack httpCallBack = null)
            : base(config, httpClient, authManagers, httpCallBack)
        {
        }

        /// <summary>
        /// Calculate EndPoint.
        /// </summary>
        /// <param name="x">Required parameter: LHS Value.</param>
        /// <param name="y">Required parameter: RHS Value.</param>
        /// <param name="operation">Required parameter: Example: .</param>
        /// <returns>Returns the double response from the API call.</returns>
        public double Calculate(
                double x,
                double y,
                Models.OperationTypeEnum operation)
        {
            Task<double> t = this.CalculateAsync(x, y, operation);
            ApiHelper.RunTaskSynchronously(t);
            return t.Result;
        }

        /// <summary>
        /// Calculate EndPoint.
        /// </summary>
        /// <param name="x">Required parameter: LHS Value.</param>
        /// <param name="y">Required parameter: RHS Value.</param>
        /// <param name="operation">Required parameter: Example: .</param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the double response from the API call.</returns>
        public async Task<double> CalculateAsync(
                double x,
                double y,
                Models.OperationTypeEnum operation,
                CancellationToken cancellationToken = default)
        {
            // the base uri for api requests.
            string baseUri = this.Config.GetBaseUri();

            // prepare query string for API call.
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/{operation}");

            // process optional template parameters.
            ApiHelper.AppendUrlWithTemplateParameters(queryBuilder, new Dictionary<string, object>()
            {
                { "operation", ApiHelper.JsonSerialize(operation).Trim('\"') },
            });

            // prepare specfied query parameters.
            var queryParams = new Dictionary<string, object>()
            {
                { "x", x },
                { "y", y },
            };

            // append request with appropriate headers and parameters
            var headers = new Dictionary<string, string>()
            {
                { "user-agent", this.UserAgent },
            };

            // prepare the API call request to fetch the response.
            HttpRequest httpRequest = this.GetClientInstance().Get(queryBuilder.ToString(), headers, queryParameters: queryParams);

            if (this.HttpCallBack != null)
            {
                this.HttpCallBack.OnBeforeHttpRequestEventHandler(this.GetClientInstance(), httpRequest);
            }

            // invoke request and get response.
            HttpStringResponse response = await this.GetClientInstance().ExecuteAsStringAsync(httpRequest, cancellationToken: cancellationToken).ConfigureAwait(false);
            HttpContext context = new HttpContext(httpRequest, response);
            if (this.HttpCallBack != null)
            {
                this.HttpCallBack.OnAfterHttpResponseEventHandler(this.GetClientInstance(), response);
            }

            // handle errors defined at the API level.
            this.ValidateResponse(response, context);

            return double.Parse(response.Body);
        }
    }
}
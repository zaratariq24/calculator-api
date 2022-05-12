// <copyright file="APIController.cs" company="APIMatic">
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
    /// APIController.
    /// </summary>
    public class APIController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="APIController"/> class.
        /// </summary>
        /// <param name="config"> config instance. </param>
        /// <param name="httpClient"> httpClient. </param>
        /// <param name="authManagers"> authManager. </param>
        /// <param name="httpCallBack"> httpCallBack. </param>
        internal APIController(IConfiguration config, IHttpClient httpClient, IDictionary<string, IAuthManager> authManagers, HttpCallBack httpCallBack = null)
            : base(config, httpClient, authManagers, httpCallBack)
        {
        }

        /// <summary>
        /// Address EndPoint.
        /// </summary>
        /// <param name="appartmentno">Required parameter: Example: .</param>
        /// <param name="streetno">Required parameter: Example: .</param>
        public void Address(
                string appartmentno,
                int streetno)
        {
            Task t = this.AddressAsync(appartmentno, streetno);
            ApiHelper.RunTaskSynchronously(t);
        }

        /// <summary>
        /// Address EndPoint.
        /// </summary>
        /// <param name="appartmentno">Required parameter: Example: .</param>
        /// <param name="streetno">Required parameter: Example: .</param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the void response from the API call.</returns>
        public async Task AddressAsync(
                string appartmentno,
                int streetno,
                CancellationToken cancellationToken = default)
        {
            // the base uri for api requests.
            string baseUri = this.Config.GetBaseUri();

            // prepare query string for API call.
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/address");

            // prepare specfied query parameters.
            var queryParams = new Dictionary<string, object>()
            {
                { "Appartmentno", appartmentno },
                { "Streetno", streetno },
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
        }
    }
}
﻿#region License

/*
 * Copyright 2002-2012 the original author or authors.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#endregion

using System;
using System.Collections.Generic;

using DS.AFP.Communication.Http;
using DS.AFP.Communication.Http.Client;
using DS.AFP.Communication.Http.Converters;
using Common.Logging;

namespace DS.AFP.Communication.Rest.Client.Support
{
    /// <summary>
    /// Request callback implementation that prepares the request's accept headers.
    /// </summary>
    /// <author>Arjen Poutsma</author>
    /// <author>Bruno Baia (.NET)</author>
    public class AcceptHeaderRequestCallback : IRequestCallback
    {
        #region Logging
#if !SILVERLIGHT && !CF_3_5
        private static readonly ILog LOG = LogManager.GetLogger(typeof(AcceptHeaderRequestCallback));
#endif
        #endregion

        /// <summary>
        /// The expected response body type.
        /// </summary>
        protected Type responseType;

        /// <summary>
        /// The list of <see cref="IHttpMessageConverter"/> to use.
        /// </summary>
        protected IList<IHttpMessageConverter> messageConverters;

        /// <summary>
        /// Creates a new instance of <see cref="AcceptHeaderRequestCallback"/>.
        /// </summary>
        /// <param name="responseType">The expected response body type.</param>
        /// <param name="messageConverters">The list of <see cref="IHttpMessageConverter"/> to use.</param>
        public AcceptHeaderRequestCallback(Type responseType, IList<IHttpMessageConverter> messageConverters)
        {
            this.responseType = responseType;
            this.messageConverters = messageConverters;
        }

        #region IRequestCallback Members

        /// <summary>
        /// Gets called by <see cref="RestTemplate"/> with an <see cref="IClientHttpRequest"/> to write data. 
        /// </summary>
        /// <remarks>
        /// Does not need to care about closing the request or about handling errors: 
        /// this will all be handled by the <see cref="RestTemplate"/> class.
        /// </remarks>
        /// <param name="request">The active HTTP request.</param>
        public virtual void DoWithRequest(IClientHttpRequest request)
        {
            if (responseType != null)
            {
                List<MediaType> allSupportedMediaTypes = new List<MediaType>();
                foreach (IHttpMessageConverter messageConverter in this.messageConverters)
                {
                    if (messageConverter.CanRead(responseType, null))
                    {
                        foreach (MediaType supportedMediaType in messageConverter.SupportedMediaTypes)
                        {
                            if (supportedMediaType.CharSet != null)
                            {
                                allSupportedMediaTypes.Add(new MediaType(
                                    supportedMediaType.Type, supportedMediaType.Subtype));
                            }
                            else
                            {
                                allSupportedMediaTypes.Add(supportedMediaType);
                            }
                        }
                    }
                }
                if (allSupportedMediaTypes.Count > 0)
                {
                    MediaType.SortBySpecificity(allSupportedMediaTypes);

                    #region Instrumentation
#if !SILVERLIGHT && !CF_3_5
                    if (LOG.IsDebugEnabled)
                    {
                        LOG.Debug(String.Format(
                            "Setting request Accept header to '{0}'",
                            MediaType.ToString(allSupportedMediaTypes)));
                    }
#endif
                    #endregion

                    request.Headers.Accept = allSupportedMediaTypes.ToArray();
                }
            }
        }

        #endregion
    }
}
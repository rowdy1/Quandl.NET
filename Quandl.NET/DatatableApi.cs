﻿using Flurl;
using Flurl.Http;
using Quandl.NET.Helper;
using Quandl.NET.Model.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Quandl.NET
{
    [Obsolete("Quandl has recently reorganized the api, please use Timeseries or Tables class for api call, this class will be removed in later patch")]
    public class DatatableApi : QuandlApiBase
    {
        public DatatableApi(string apiKey) : base(apiKey)
        {
        }

        /// <summary>
        /// This API call returns a datatable, subject to a limit of 10,000 rows.
        /// <a href="https://www.quandl.com/docs/api?json#get-entire-datatable">Reference</a>
        /// </summary>
        /// <param name="databaseCode">short code for database</param>
        /// <param name="datatableCode">short code for datatable</param>
        /// <param name="rowFilter">Criteria to filter row</param>
        /// <param name="columnFilter">Criteria to filter column</param>
        /// <param name="nextCursorId">Next cursor id</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>Get datatable response</returns>
        [Obsolete("Please replace it with GetAsync() method from Tables class")]
        public async Task<TableResponse> GetAsync(string databaseCode, string datatableCode, Dictionary<string, List<string>> rowFilter = null,
            List<string> columnFilter = null, string nextCursorId = null, CancellationToken token = default(CancellationToken))
        {
            try
            {
                var massagedRowFilters = rowFilter?.ToDictionary(kvp => kvp.Key, kvp => string.Join(",", kvp.Value));
                string massagedColumnFilters = columnFilter != null ? string.Join(",", columnFilter) : null;
                return await $"{Constant.HostUri}/datatables/{databaseCode}/{datatableCode}.json"
                    .SetQueryParamForEach(massagedRowFilters)
                    .SetQueryParam("qopts.columns", massagedColumnFilters)
                    .SetQueryParam("qopts.cursor_id", nextCursorId)
                    .SetQueryParam("api_key", _apiKey)
                    .GetAsync(token)
                    .ReceiveJson<TableResponse>()
                    .ConfigureAwait(false);
            }
            catch (FlurlHttpException ex)
            {
                throw await ex.ToQuandlException();
            }
        }

        /// <summary>
        /// This API call returns a datatable, subject to a limit of 10,000 rows.
        /// <a href="https://www.quandl.com/docs/api?csv#get-entire-datatable">Reference</a>
        /// </summary>
        /// <param name="databaseCode">short code for database</param>
        /// <param name="datatableCode">short code for datatable</param>
        /// <param name="rowFilter">Criteria to filter row</param>
        /// <param name="columnFilter">Criteria to filter column</param>
        /// <param name="fullResult">Flag to display full result</param>
        /// <param name="nextCursorId">Next cursor id</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>Stream of csv file (.csv)</returns>
        [Obsolete("Please replace it with GetAsync() method from Tables class")]
        public async Task<Stream> GetCsvAsync(string databaseCode, string datatableCode, Dictionary<string, List<string>> rowFilter = null,
            List<string> columnFilter = null, bool? fullResult = null, string nextCursorId = null, CancellationToken token = default(CancellationToken))
        {
            try
            {
                var massagedRowFilters = rowFilter?.ToDictionary(kvp => kvp.Key, kvp => string.Join(",", kvp.Value));
                string massagedColumnFilters = columnFilter != null ? string.Join(",", columnFilter) : null;
                var massagedNextCursorId = fullResult == null ? nextCursorId : null;

                return await $"{Constant.HostUri}/datatables/{databaseCode}/{datatableCode}.csv"
                    .SetQueryParamForEach(massagedRowFilters)
                    .SetQueryParam("qopts.columns", massagedColumnFilters)
                    .SetQueryParam("qopts.export", fullResult)
                    .SetQueryParam("qopts.cursor_id", massagedNextCursorId)
                    .SetQueryParam("api_key", _apiKey)
                    .GetAsync(token)
                    .ReceiveStream()
                    .ConfigureAwait(false);
            }
            catch (FlurlHttpException ex)
            {
                throw await ex.ToQuandlException();
            }
        }

        /// <summary>
        /// This API call returns datatable's metadata
        /// </summary>
        /// <param name="databaseCode">short code for database</param>
        /// <param name="datatableCode">short code for datatable</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>Get datatable metadata response</returns>
        [Obsolete("Please replace it with GetMetadataAsync() method from Tables class")]
        public async Task<TableMetadataResponse> GetMetadataAsync(string databaseCode, string datatableCode, CancellationToken token = default(CancellationToken))
        {
            try
            {
                return await $"{Constant.HostUri}/datatables/{databaseCode}/{datatableCode}/metadata.json"
                    .SetQueryParam("api_key", _apiKey)
                    .GetAsync(token)
                    .ReceiveJson<TableMetadataResponse>()
                    .ConfigureAwait(false);
            }
            catch (FlurlHttpException ex)
            {
                throw await ex.ToQuandlException();
            }
        }

        /// <summary>
        /// This API call returns datatable's metadata
        /// </summary>
        /// <param name="databaseCode">short code for database</param>
        /// <param name="datatableCode">short code for datatable</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>Stream of csv file (.csv)</returns>
        [Obsolete("Please replace it with GetMetadataAsync() method from Tables class")]
        public async Task<Stream> GetMetadataCsvAsync(string databaseCode, string datatableCode, CancellationToken token = default(CancellationToken))
        {
            try
            {
                return await $"{Constant.HostUri}/datatables/{databaseCode}/{datatableCode}/metadata.csv"
                    .SetQueryParam("api_key", _apiKey)
                    .GetAsync(token)
                    .ReceiveStream()
                    .ConfigureAwait(false);
            }
            catch (FlurlHttpException ex)
            {
                throw await ex.ToQuandlException();
            }
        }
    }
}
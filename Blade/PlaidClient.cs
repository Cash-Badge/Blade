using Blade.Authentication;
using Blade.Balance;
using Blade.Category;
using Blade.Identity;
using Blade.Income;
using Blade.Institution;
using Blade.Management;
using Blade.Transactions;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Blade
{
    public class PlaidJsonPropertyNamingPolicy : JsonNamingPolicy
    {
        public static PlaidJsonPropertyNamingPolicy Instance { get; } = new PlaidJsonPropertyNamingPolicy { };

        /// <summary>
        /// Convert the property name to JSON using Plaid's naming conventions.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override string ConvertName(string name)
        {
            ReadOnlySpan<char> target = name;
            StringBuilder resultBuilder = new StringBuilder { };

            for (int index = 0; index < target.Length; index++)
            {
                ref readonly char character = ref target[index];

                if ((character & 32) == 0)
                {
                    if (index != 0)
                    {
                        resultBuilder.Append('_');
                    }

                    resultBuilder.Append((char)(character | 32));
                }
                else
                {
                    resultBuilder.Append(character);
                }
            }

            return resultBuilder.ToString();
        }
    }

    /// <summary>
    /// Provides methods for sending request to and receiving data from Plaid banking API.
    /// </summary>
    public sealed class PlaidClient : IDisposable
    {
        public static JsonSerializerOptions PlaidNullValuePropagatingJsonSerializerOptions { get; } = new JsonSerializerOptions
        {
            PropertyNamingPolicy = PlaidJsonPropertyNamingPolicy.Instance,
            IgnoreNullValues = false
        };

        public static JsonSerializerOptions PlaidJsonSerializerOptions { get; } = new JsonSerializerOptions
        {
            PropertyNamingPolicy = PlaidJsonPropertyNamingPolicy.Instance,
            IgnoreNullValues =  true
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaidClient"/> class.
        /// </summary>
        /// <param name="useNewClient">A boolean value for whether or not the default <see cref="HttpClient"/> instance should be recycled for this <see cref="PlaidClient"/> instance or if a new one should be created.</param>
        public PlaidClient(bool useNewClient = false)
        {
            // TODO: Make useNewClient set-only property that sets the value of Client to a new HttpClient instance.
            if (useNewClient)
            {
                Client = new HttpClient { };
            }
        }

        /// <summary>
        /// Retrieves information about the status of an <see cref="Entity.Item"/>. Endpoint '/item/get'.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Management.GetItemResponse&gt;.</returns>
        public Task<GetItemResponse> FetchItemAsync(GetItemRequest request) => PostAsync<GetItemRequest, GetItemResponse>("item/get", request);

        /// <summary>
        /// Delete an <see cref="Entity.Item"/>. Once deleted, the access_token associated with the <see cref="Entity.Item"/> is no longer valid and cannot be used to access any data that was associated with the <see cref="Entity.Item"/>.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Management.DeleteItemResponse&gt;.</returns>
        public Task<DeleteItemResponse> DeleteItemAsync(DeleteItemRequest request) => PostAsync<DeleteItemRequest, DeleteItemResponse>("item/delete", request);

        /// <summary>
        /// Updates the webhook associated with an <see cref="Entity.Item"/>. This request triggers a WEBHOOK_UPDATE_ACKNOWLEDGED webhook.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Management.UpdateWebhookResponse&gt;.</returns>
        public Task<UpdateWebhookResponse> UpdateWebhookAsync(UpdateWebhookRequest request) => PostAsync<UpdateWebhookRequest, UpdateWebhookResponse>("item/webhook/update", request);

        /// <summary>
        /// Exchanges a Link public_token for an API access_token.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Management.ExchangeTokenResponse&gt;.</returns>
        public Task<ExchangeTokenResponse> ExchangeTokenAsync(ExchangeTokenRequest request) => PostAsync<ExchangeTokenRequest, ExchangeTokenResponse>("item/public_token/exchange", request);

        /// <summary>
        /// Rotates the access_token associated with an <see cref="Entity.Item"/>. The endpoint returns a new access_token and immediately invalidates the previous access_token.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Management.RotateAccessTokenResponse&gt;.</returns>
        public Task<RotateAccessTokenResponse> RotateAccessTokenAsync(RotateAccessTokenRequest request) => PostAsync<RotateAccessTokenRequest, RotateAccessTokenResponse>("item/access_token/invalidate", request);

        /// <summary>
        /// Updates an access_token from the legacy version of Plaid’s API, you can use method to generate an access_token for the Item that works with the current API.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Management.UpdateAccessTokenVersionResponse&gt;.</returns>
        public Task<UpdateAccessTokenVersionResponse> UpdateAccessTokenVersion(UpdateAccessTokenVersionRequest request) => PostAsync<UpdateAccessTokenVersionRequest, UpdateAccessTokenVersionResponse>("item/access_token/update_version", request);

        /// <summary>
        /// Retrieves the institutions that match the query parameters.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Institution.SearchResponse&gt;.</returns>
        public Task<SearchResponse> FetchInstitutionsAsync(SearchRequest request) => PostAsync<SearchRequest, SearchResponse>("institutions/search", request);

        /// <summary>
        /// Retrieves the institutions that match the id.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Institution.SearchByIdResponse&gt;.</returns>
        public Task<SearchByIdResponse> FetchInstitutionByIdAsync(SearchByIdRequest request) => PostAsync<SearchByIdRequest, SearchByIdResponse>("institutions/get_by_id", request);

        /// <summary>
        /// Retrieves information pertaining to a <see cref="Entity.Item"/>’s income. In addition to the annual income, detailed information will be provided for each contributing income stream (or job).
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Income.GetIncomeResponse&gt;.</returns>
        public Task<GetIncomeResponse> FetchUserIncomeAsync(GetIncomeRequest request) => PostAsync<GetIncomeRequest, GetIncomeResponse>("income/get", request);

        /// <summary>
        /// Retrieves the bank account and routing numbers associated with an <see cref="Entity.Item"/>’s checking and savings accounts, along with high-level account data and balances.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Auth.GetAccountInfoResponse&gt;.</returns>
        public Task<GetAccountInfoResponse> FetchAccountInfoAsync(GetAccountInfoRequest request) => PostAsync<GetAccountInfoRequest, GetAccountInfoResponse>("auth/get", request);

        /// <summary>
        /// Retrieves the real-time balance for each of an <see cref="Entity.Item"/>’s accounts.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Balance.GetBalanceResponse&gt;.</returns>
        public Task<GetBalanceResponse> FetchAccountBalanceAsync(GetBalanceRequest request) => PostAsync<GetBalanceRequest, GetBalanceResponse>("accounts/balance/get", request);

        /// <summary>
        /// Retrieves detailed information on categories returned by Plaid.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Category.GetCategoriesResponse&gt;.</returns>
        public Task<GetCategoriesResponse> FetchCategoriesAsync(GetCategoriesRequest request) => PostAsync<GetCategoriesRequest, GetCategoriesResponse>("categories/get", request);

        /// <summary>
        /// Retrieves various account holder information on file with the financial institution, including names, emails, phone numbers, and addresses.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Identity.GetUserIdentityResponse&gt;.</returns>
        public Task<GetUserIdentityResponse> FetchUserIdentityAsync(GetUserIdentityRequest request) => PostAsync<GetUserIdentityRequest, GetUserIdentityResponse>("identity/get", request);

        /// <summary>
        /// Retrieves user-authorized transaction data for credit and depository-type <see cref="Entity.Account"/>.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Transactions.GetTransactionsResponse&gt;.</returns>
        public Task<GetTransactionsResponse> FetchTransactionsAsync(GetTransactionsRequest request) => PostAsync<GetTransactionsRequest, GetTransactionsResponse>("transactions/get", request);

        /// <summary>
        /// Exchanges a Link access_token for an Stripe API stripe_bank_account_token.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Management.StripeTokenResponse&gt;.</returns>
        public Task<StripeTokenResponse> FetchStripeTokenAsync(StripeTokenRequest request) => PostAsync<StripeTokenRequest, StripeTokenResponse>("processor/stripe/bank_account_token/create", request);

        /// <summary>
        /// Can be used to create a public_token which can be used to create an access_token without the use of Link, in the <see cref="Environment.Sandbox"/> environment.
        /// </summary>
        /// <param name="request">The request data needed for the '/sandbox/public_token/create' endpoint.</param>
        /// <returns>A <see cref="Task{CreateSandboxedPublicTokenResponse}"/> which represents the response to this request from the Plaid servers.</returns>
        public Task<CreateSandboxedPublicTokenResponse> CreateSandboxedPublicToken(CreateSandboxedPublicTokenRequest request) => PostAsync<CreateSandboxedPublicTokenRequest, CreateSandboxedPublicTokenResponse>("sandbox/public_token/create", request);

        internal string GetEndpoint(string path) => new UriBuilder
        {
            Scheme = Uri.UriSchemeHttps,
            Host = $"{Environment.ToString().ToLowerInvariant()}.plaid.com",
            Path = path.Trim(' ', '/', '\\')
        }.Uri.AbsoluteUri;

        internal async Task<TResponse> PostAsync<TRequest, TResponse>(string path, TRequest request) where TResponse : Response
        {
            SetFallbackValues(RequestFallbackData);
            if (!IgnoreDefaultFallbackData)
            {
                SetFallbackValues(DefaultRequestFallbackData);
            }

            string endpoint = GetEndpoint(path);

            using Utf8JsonContent requestContent = GetJsonContent(JsonSerializer.SerializeToUtf8Bytes(request, GetSerializerOptionsForRequest()));
            using HttpResponseMessage response = await Client.PostAsync(endpoint, requestContent);
            using Stream contentStream = await response.Content.ReadAsStreamAsync();

            Memory<byte> content = new Memory<byte>(new byte[contentStream.Length]);
            await contentStream.ReadAsync(content);
            TResponse result = JsonSerializer.Deserialize<TResponse>(content.Span, GetSerializerOptionsForRequest());
            result.Status = response.StatusCode;

            if (!response.IsSuccessStatusCode)
            {
                JsonElement data = JsonDocument.Parse(content).RootElement;
                result.Exception = new Exceptions.PlaidException(data.GetProperty("error_message").GetString())
                {
                    HelpLink = "https://plaid.com/docs/api/#errors-overview",
                    DisplayMessage = data.GetProperty("display_message").GetString(),
                    ErrorType = data.GetProperty("error_type").GetString(),
                    ErrorCode = data.GetProperty("error_code").GetString(),
                    Source = endpoint,
                };

                if (ThrowOnFailure)
                    throw result.Exception;
            }

            return result;

            JsonSerializerOptions GetSerializerOptionsForRequest() => request switch
            {
                SearchRequest { } => PlaidNullValuePropagatingJsonSerializerOptions,
                _ => PlaidJsonSerializerOptions
            };

            TRequest SetFallbackValues(CommonEndpointRequestData targetFallbackData)
            {
                if (targetFallbackData is { })
                {
                    SetProperty(nameof(CommonEndpointRequestData.PublicKey), targetFallbackData.PublicKey);
                    SetProperty(nameof(CommonEndpointRequestData.AccessToken), targetFallbackData.AccessToken);
                    SetProperty(nameof(CommonEndpointRequestData.PublicToken), targetFallbackData.PublicToken);
                    SetProperty(nameof(CommonEndpointRequestData.Client), targetFallbackData.Client);
                    SetProperty(nameof(CommonEndpointRequestData.Secret), targetFallbackData.Secret);
                }

                return request;

                void SetProperty(string name, object value)
                {
                    try
                    {
                        if (value is { } && typeof(TRequest).GetTypeInfo().GetRuntimeProperties().FirstOrDefault(property => property.Name.Equals(name)) is { } target && target.CanWrite && target.CanRead && target.GetValue(request) is null)
                        {
                            target.SetValue(request, value);
                        }
                    }
                    catch { }
                }
            }
        }

        public bool UseDefaultClient
        {
            set
            {
                if (value && Client != DefaultClient)
                {
                    Client = DefaultClient;
                }
                else if (!value && Client == DefaultClient)
                {
                    Client = new HttpClient { };
                }
            }
        }

        public bool IgnoreDefaultFallbackData { get; set; }

        public bool ThrowOnFailure { get; set; } = true;

        public Environment Environment { get; set; } = Environment.Development;

        HttpClient Client { get; set; } = DefaultClient ?? (DefaultClient ??= new HttpClient { });

        static HttpClient DefaultClient { get; set; }

        static Utf8JsonContent GetJsonContent(byte[] json) => new Utf8JsonContent(json);

        public void Dispose()
        {
            if (Client != DefaultClient)
            {
                Client.Dispose();
            }
        }

        public static void DisposeClient() => DefaultClient = null;

        public CommonEndpointRequestData RequestFallbackData { get; set; }

        public static CommonEndpointRequestData DefaultRequestFallbackData { get; set; }
    }

    /// <summary>
    /// A class to store values of datapoints common to many Plaid endpoint requests.
    /// </summary>
    public sealed class CommonEndpointRequestData
    {
        /// <summary>
        /// The client identifier.
        /// </summary>
        [JsonPropertyName("client_id")]
        public string Client { get; set; }

        /// <summary>
        /// The secret for the target <see cref="Environment"/>.
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// The target <see cref="Entity.Item"/> access token.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// The key needed to authorize endpoint access.
        /// </summary>
        public string PublicKey { get; set; }

        /// <summary>
        /// The target <see cref="Entity.Item"/> public token.
        /// </summary>
        public object PublicToken { get; set; }
    }

    public sealed class Utf8JsonContent : ByteArrayContent
    {
        public Utf8JsonContent(byte[] content) : base(content) => Headers.ContentType = new MediaTypeHeaderValue("application/json") { CharSet = Encoding.UTF8.WebName };
    }
}
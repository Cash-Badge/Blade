using Blade;
using Blade.Institution;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Blade.Test
{
    public static class Helper
    {
        public class CommonEndpointRequestData
        {
            public static CommonEndpointRequestData Instance { get; set; }

            [JsonPropertyName("client_id")]
            public string Client { get; set; }

            public string Secret { get; set; }

            public string AccessToken { get; set; }

            public string PublicKey { get; set; }

            public object PublicToken { get; set; }
        }

        static string CommonEndpointRequestDataFilePath { get; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "secrets.json");

        public static async Task InitializeAsync()
        {
            using Stream fileStream = new FileStream(CommonEndpointRequestDataFilePath, FileMode.Open, FileAccess.Read, FileShare.Read, 300, FileOptions.Asynchronous);
            CommonEndpointRequestData.Instance ??= await JsonSerializer.DeserializeAsync<CommonEndpointRequestData>(fileStream, PlaidClient.PlaidNullValuePropagatingJsonSerializerOptions);
        }

        public static async Task PersistCommonEndpointRequestDataAsync()
        {
            using Stream fileStream = new FileStream(CommonEndpointRequestDataFilePath, FileMode.Truncate, FileAccess.ReadWrite, FileShare.Read, 300, FileOptions.Asynchronous);
            await JsonSerializer.SerializeAsync(fileStream, CommonEndpointRequestData.Instance, PlaidClient.PlaidNullValuePropagatingJsonSerializerOptions);
        }

        public static TRequest UseDefaults<TRequest>(this TRequest request)
        {
            if (CommonEndpointRequestData.Instance is { })
            {
                SetProperty(nameof(CommonEndpointRequestData.PublicKey), CommonEndpointRequestData.Instance.PublicKey);
                SetProperty(nameof(CommonEndpointRequestData.AccessToken), CommonEndpointRequestData.Instance.AccessToken);
                SetProperty(nameof(CommonEndpointRequestData.PublicToken), CommonEndpointRequestData.Instance.PublicToken);
                SetProperty(nameof(CommonEndpointRequestData.Client), CommonEndpointRequestData.Instance.Client);
                SetProperty(nameof(CommonEndpointRequestData.Secret), CommonEndpointRequestData.Instance.Secret);
            }

            return request;

            void SetProperty(string name, object value)
            {
                try
                {
                    if (value is { } && request.GetType().GetTypeInfo().GetRuntimeProperties().FirstOrDefault(property => property.Name.Equals(name)) is { } target && target.CanWrite)
                    {
                        target.SetValue(request, value);
                    }
                }
                catch
                {
                    Debug.Print($"Failed to set {name} property on given request.");
                }
            }
        }
    }
}
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
        static string CommonEndpointRequestDataFilePath { get; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "secrets.json");

        public static async Task InitializeAsync()
        {
            using Stream fileStream = new FileStream(CommonEndpointRequestDataFilePath, FileMode.Open, FileAccess.Read, FileShare.Read, 300, FileOptions.Asynchronous);
            PlaidClient.DefaultRequestFallbackData ??= await JsonSerializer.DeserializeAsync<CommonEndpointRequestData>(fileStream, PlaidClient.PlaidNullValuePropagatingJsonSerializerOptions);
        }

        public static async Task PersistCommonEndpointRequestDataAsync()
        {
            using Stream fileStream = new FileStream(CommonEndpointRequestDataFilePath, FileMode.Truncate, FileAccess.Write, FileShare.None, 300, FileOptions.Asynchronous);
            await JsonSerializer.SerializeAsync(fileStream, PlaidClient.DefaultRequestFallbackData, PlaidClient.PlaidNullValuePropagatingJsonSerializerOptions);
        }
    }
}
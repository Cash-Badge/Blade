# Blade

Blade is an implementation of an SDK to interact with the [Plaid web APIs](https://plaid.com/docs/#endpoints) written in C# 8 targeting .NET Standard 2.1.

## Usage

In order to interact with Plaid endpoints using this SDK, an instance of the `PlaidClient` class must be created, and can be provided with certain fallback values to use for populating request bodies with commonly required data when not overriden such as the `client_id`, `public_key`, `secret` for the target environment, target item `public_token`, and target item `access_token`. The access secrets for the target account and environment can be found in [the Plaid account dashboard](https://dashboard.plaid.com/account/keys) and the identification data for items can be found by [using Plaid Link to create them](https://plaid.com/docs/#creating-items-with-plaid-link) and [exchanging the resultant `public_token` for an `access_token`](https://plaid.com/docs/#exchange-token-flow). The aforementioned token exchange functionality is provided in this SDK via the `ExchangeTokenAsync` method on a `PlaidClient` instance.

### Fallback Common Data Provisioning

Please use an instance of the `CommonEndpointRequestData` class to store common endpoint request data that should be used for fallback values when the data is not specified in a request body data storage class instance. In order to set a specific `CommonEndpointRequestData` instance to be used for fallback data, use the `PlaidClient` instance `RequestFallbackData` property to target a specific instance, or use the static `DefaultRequestFallbackData` property to set the default for all future instances. 

Refer to the following reference for as to which properties of the `CommonEndpointRequestData` class should be populated with which access secret.

| Property | Value |
|:--------:|:------|
| `Client` | `client_id` found in [the Plaid account dashboard](https://dashboard.plaid.com/account/keys) |
| `PublicKey` | `public_key` found in [the Plaid account dashboard](https://dashboard.plaid.com/account/keys) |
| `Secret` | `secret` value found in [the Plaid account dashboard](https://dashboard.plaid.com/account/keys)<sup>†</sup> |
| `PublicToken` | `public_token`<sup>‡</sup> for a specific Plaid item |
| `AccessToken` | `access_token`<sup>‡</sup> for a specific Plaid item  |

<sup>†</sup> The `Environment` property on the `PlaidClient` instance should match the environment of the provided `secret`. The default value is `Environment.Development`.
<sup>‡</sup> Token values require API interactions or the use of Plaid Link to acquire.

All access secrets should be provided as strings to the SDK.

### Example

The following is a sample usage of the SDK in C# 8 which retrives data regarding institution 14 in the sandbox environment.

```csharp
using Blade;
using Blade.Institution;

...

using PlaidClient client = new PlaidClient
{
	RequestFallbackData = new CommonEndpointRequestData { PublicKey = "0b6f326584eaa9c19319486f32136a" },
	Environment = Environment.Sandbox
};

SearchByIdResponse response = await client.FetchInstitutionByIdAsync(new SearchByIdRequest { Institution = "ins_14" });
```

Note that the provided `PublicKey` value is not an actual `public_key` value in this example.

## Remarks

A `PlaidClient` instance only needs to be disposed of with C# & `using` syntax or a `using(...) { ... }` block if `UseDefaultClient` is set to `false`, but it is good practice to do so anyways. If the SDK will no longer be used at any point but the application will continue execution, after disposing all instances that require being disposed, call the static `PlaidClient.DisposeRecycledResources()` method to dispose of recycled resources across all current and future instances.

## Contribution

This project welcomes the creation of pull requests, which are to be reviewed by a team member if the change is seen fit. Please create an issue to discuss a large change or feature request you would like to make before committing time and effort to develop it, in order for our team to give their opinion of the idea and whether or not it is likely to be adopted by the project.

## License

The source code provided in this repository is provided under [the MIT license](LICENSE.txt).

[Source Material](https://github.com/Ackara/Plaid.NET)
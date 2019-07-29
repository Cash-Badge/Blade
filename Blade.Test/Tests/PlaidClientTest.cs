#pragma warning disable IDE0060 // Remove unused parameter

using Blade;
using Blade.Authentication;
using Blade.Balance;
using Blade.Category;
using Blade.Identity;
using Blade.Income;
using Blade.Institution;
using Blade.Management;
using Blade.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.Threading.Tasks;

namespace Blade.Test
{
    [TestClass]
    public class PlaidClientTest
    {
        [AssemblyInitialize]
        public static async Task InitializeAsync(TestContext context)
        {
            await Helper.InitializeAsync();

            if (PlaidClient.DefaultRequestFallbackData.AccessToken is null || PlaidClient.DefaultRequestFallbackData.PublicToken is null)
            {
                using PlaidClient client = new PlaidClient { Environment = Environment.Sandbox };
                CreateSandboxedPublicTokenResponse response = await client.CreateSandboxedPublicToken(new CreateSandboxedPublicTokenRequest { Institution = "ins_14", InitialProducts = new[] { "assets", "auth", /*"balance",*/ "transactions", "income", "identity" } });
                PlaidClient.DefaultRequestFallbackData.PublicToken = response.PublicToken;
                PlaidClient.DefaultRequestFallbackData.AccessToken = (await client.ExchangeTokenAsync(new ExchangeTokenRequest { })).AccessToken;
                await Helper.PersistCommonEndpointRequestDataAsync();
            }
        }

        [TestMethod]
        public async Task GetItemAsync_should_retrieve_the_item_associated_with_the_specified_access_token()
        {
            // Arrange
            using PlaidClient client = new PlaidClient { Environment = Environment.Sandbox };
            GetItemRequest request = new GetItemRequest { };

            // Act
            GetItemResponse result = await client.FetchItemAsync(request);

            // Assert
            result.SuccessfulOutcome.ShouldBeTrue();
            result.Request.ShouldNotBeNullOrEmpty();
            result.Item.Identifier.ShouldNotBeNullOrEmpty();
            result.Item.Institution.ShouldNotBeNullOrEmpty();
            result.Item.BilledProducts.Length.ShouldBeGreaterThan(0);
            result.Item.AvailableProducts.Length.ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public async Task ExchangePublicTokenAsync_should_retrieve_a_response_from_the_api_server()
        {
            // Arrange
            using PlaidClient client = new PlaidClient { Environment = Environment.Sandbox };

            // Act
            ExchangeTokenRequest request = new ExchangeTokenRequest { };
            ExchangeTokenResponse result = await client.ExchangeTokenAsync(request);

            // Assert
            result.Exception.ShouldBeNull();
            result.SuccessfulOutcome.ShouldBeTrue();
        }

        [TestMethod]
        public async Task FetchCategoriesAsync_should_retrieve_the_api_category_list()
        {
            // Arrange
            using PlaidClient client = new PlaidClient { Environment = Environment.Sandbox };
            GetCategoriesRequest request = new GetCategoriesRequest { };

            // Act
            GetCategoriesResponse result = await client.FetchCategoriesAsync(request);

            // Assert
            result.SuccessfulOutcome.ShouldBeTrue();
            result.Request.ShouldNotBeNullOrEmpty();
            result.Categories.Length.ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public async Task FetchInstitutionsAsync_should_retrieve_a_list_of_banks_that_matches_a_specified_query()
        {
            // Arrange
            using PlaidClient sut = new PlaidClient { Environment = Environment.Sandbox };

            // Act
            SearchRequest request = new SearchRequest { Query = "tartan" };

            SearchResponse result = await sut.FetchInstitutionsAsync(request);

            // Assert
            result.SuccessfulOutcome.ShouldBeTrue();
            result.Request.ShouldNotBeNullOrEmpty();
            result.Institutions.Length.ShouldBeGreaterThanOrEqualTo(1);
            result.Institutions.ShouldAllBe(i => i.Name.ToLower().Contains(request.Query.ToLower()));
        }

        [TestMethod]
        public async Task FetchInstitutionByIdAsync_should_retrieve_a_bank_that_matches_a_specified_id()
        {
            // Arrange
            using PlaidClient client = new PlaidClient { Environment = Environment.Sandbox };

            // Act
            SearchByIdRequest request = new SearchByIdRequest { Institution = "ins_109511" };
            SearchByIdResponse response = await client.FetchInstitutionByIdAsync(request);

            // Assert
            response.SuccessfulOutcome.ShouldBeTrue();
            response.Request.ShouldNotBeNullOrEmpty();
            response.Institution.Identifier.ShouldBe(request.Institution);
        }

        [TestMethod]
        public async Task FetchTransactionsAsync_should_retrieve_a_list_of_transactions()
        {
            // Arrange
            using PlaidClient client = new PlaidClient { Environment = Environment.Sandbox };

            // Act
            GetTransactionsRequest request = new GetTransactionsRequest { };
            GetTransactionsResponse result = await client.FetchTransactionsAsync(request);

            // Assert
            result.SuccessfulOutcome.ShouldBeTrue();
            result.Request.ShouldNotBeNullOrEmpty();
            result.TransactionsReturned.ShouldBeGreaterThan(0);
            result.Transactions.Length.ShouldBeGreaterThan(0);
            result.Transactions[0].Amount.ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public async Task FetchAccountBalanceAsync_should_retrieve_the_account_balances_of_an_user()
        {
            // Arrange
            using PlaidClient client = new PlaidClient { Environment = Environment.Sandbox };

            // Act
            GetBalanceRequest request = new GetBalanceRequest { };
            GetBalanceResponse result = await client.FetchAccountBalanceAsync(request);

            // Assert
            result.Request.ShouldNotBeNullOrEmpty();
            result.Accounts.Length.ShouldBeGreaterThanOrEqualTo(1);
            result.Accounts[0].Balance.Current.ShouldBeGreaterThanOrEqualTo(1);
        }

        [TestMethod]
        public async Task FetchAccountInfoAsync_should_retrieve_the_routing_numbers_of_an_user_accounts()
        {
            // Arrange
            using PlaidClient client = new PlaidClient { Environment = Environment.Sandbox };
            GetAccountInfoRequest request = new GetAccountInfoRequest { };

            // Act
            GetAccountInfoResponse result = await client.FetchAccountInfoAsync(request);

            // Assert
            result.SuccessfulOutcome.ShouldBeTrue();
            result.Request.ShouldNotBeNullOrEmpty();
            result.Accounts.Length.ShouldBeGreaterThan(0);
            //result.Numbers.Length.ShouldBeGreaterThan(0);
            result.Item.ShouldNotBeNull();
        }

        [TestMethod]
        public async Task FetchUserIdentityAsync_should_retrieve_the_personal_info_of_an_userAsync()
        {
            // Arrange
            using PlaidClient client = new PlaidClient { Environment = Environment.Sandbox };
            GetUserIdentityRequest request = new GetUserIdentityRequest { };

            // Act
            GetUserIdentityResponse result = await client.FetchUserIdentityAsync(request);
            if (result.Exception?.ErrorCode == "INVALID_PRODUCT")
                Assert.Inconclusive(Properties.Resources.AuthorizationDenialMessage);

            // Assert
            result.SuccessfulOutcome.ShouldBeTrue();
            result.Request.ShouldNotBeNullOrEmpty();
            result.Accounts.Length.ShouldBeGreaterThan(0);
            result.Accounts[0].Owners.Length.ShouldBeGreaterThan(0);
            result.Item.ShouldNotBeNull();
        }

        [TestMethod]
        public async Task FetchIncomeAsync_should_retrieve_the_monthly_earnings_of_an_userAsync()
        {
            // Arrange
            using PlaidClient client = new PlaidClient { Environment = Environment.Sandbox };
            GetIncomeRequest request = new GetIncomeRequest { };

            // Act
            GetIncomeResponse result = await client.FetchUserIncomeAsync(request);
            if (result.Exception?.ErrorCode == "INVALID_PRODUCT")
                Assert.Inconclusive(Properties.Resources.AuthorizationDenialMessage);

            // Assert
            result.SuccessfulOutcome.ShouldBeTrue();
            result.Request.ShouldNotBeNullOrEmpty();
            result.Income.Streams.Length.ShouldBeGreaterThan(0);
            result.Income.LastYearIncome.ShouldBeGreaterThan(0);
            result.Item.ShouldBeNull();
        }
    }
}
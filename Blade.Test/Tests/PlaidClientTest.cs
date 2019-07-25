#pragma warning disable IDE0060 // Remove unused parameter

using Acklann.Plaid.MSTest;
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

namespace Blade.MSTest.Tests
{
    [TestClass]
    public class PlaidClientTest
    {
        [ClassInitialize]
        public static async Task InitializeAsync(TestContext context)
        {
            Helper.Initialize();

            if (Helper.CommonEndpointRequestData.Instance.AccessToken is null || Helper.CommonEndpointRequestData.Instance.PublicToken is null)
            {
                using PlaidClient client = new PlaidClient(Environment.Sandbox);
                CreateSandboxedPublicTokenResponse response = await client.CreateSandboxedPublicToken(new CreateSandboxedPublicTokenRequest { Institution = "ins_14", InitialProducts = new[] { "assets", "auth", /*"balance",*/ "transactions", "income", "identity" } }.UseDefaults());
                Helper.CommonEndpointRequestData.Instance.PublicToken = response.PublicToken;
                Helper.CommonEndpointRequestData.Instance.AccessToken = (await client.ExchangeTokenAsync(new ExchangeTokenRequest { }.UseDefaults())).AccessToken;
                await Helper.PersistCommonEndpointRequestDataAsync();
            }
        }

        [TestMethod]
        public async Task GetItemAsync_should_retrieve_the_item_associated_with_the_specified_access_token()
        {
            // Arrange
            using PlaidClient client = new PlaidClient(Environment.Sandbox);
            GetItemRequest request = new GetItemRequest { }.UseDefaults();

            // Act
            GetItemResponse result = await client.FetchItemAsync(request);

            // Assert
            result.SuccessfulOutcome.ShouldBeTrue();
            result.Request.ShouldNotBeNullOrEmpty();
            result.Item.Identifier.ShouldNotBeNullOrEmpty();
            result.Item.InstitutionId.ShouldNotBeNullOrEmpty();
            result.Item.BilledProducts.Length.ShouldBeGreaterThan(0);
            result.Item.AvailableProducts.Length.ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public async Task ExchangePublicTokenAsync_should_retrieve_a_response_from_the_api_server()
        {
            // Arrange
            using PlaidClient client = new PlaidClient(Environment.Sandbox) { };

            // Act
            ExchangeTokenRequest request = new ExchangeTokenRequest { }.UseDefaults();
            ExchangeTokenResponse result = await client.ExchangeTokenAsync(request);

            // Assert
            result.Exception.ShouldBeNull();
            result.SuccessfulOutcome.ShouldBeTrue();
        }

        [TestMethod]
        public async Task FetchCategoriesAsync_should_retrieve_the_api_category_list()
        {
            // Arrange
            using PlaidClient client = new PlaidClient(Environment.Sandbox);
            GetCategoriesRequest request = new GetCategoriesRequest { }.UseDefaults();

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
            using PlaidClient sut = new PlaidClient(Environment.Sandbox);

            // Act
            SearchRequest request = new SearchRequest { Query = "tartan" }.UseDefaults();

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
            using PlaidClient client = new PlaidClient(Environment.Sandbox);

            // Act
            SearchByIdRequest request = new SearchByIdRequest { InstitutionId = "ins_109511" }.UseDefaults();
            SearchByIdResponse response = await client.FetchInstitutionByIdAsync(request);

            // Assert
            response.SuccessfulOutcome.ShouldBeTrue();
            response.Request.ShouldNotBeNullOrEmpty();
            response.Institution.Identifier.ShouldBe(request.InstitutionId);
        }

        [TestMethod]
        public async Task FetchTransactionsAsync_should_retrieve_a_list_of_transactions()
        {
            // Arrange
            using PlaidClient client = new PlaidClient(Environment.Sandbox);

            // Act
            GetTransactionsRequest request = new GetTransactionsRequest { }.UseDefaults();
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
            using PlaidClient client = new PlaidClient(Environment.Sandbox);

            // Act
            GetBalanceRequest request = new GetBalanceRequest().UseDefaults();
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
            using PlaidClient client = new PlaidClient(Environment.Sandbox);
            GetAccountInfoRequest request = new GetAccountInfoRequest { }.UseDefaults();

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
            using PlaidClient client = new PlaidClient(Environment.Sandbox);
            GetUserIdentityRequest request = new GetUserIdentityRequest { }.UseDefaults();

            // Act
            GetUserIdentityResponse result = await client.FetchUserIdentityAsync(request);
            if (result.Exception?.ErrorCode == "INVALID_PRODUCT")
                Assert.Inconclusive(Test.Properties.Resources.AuthorizationDenialMessage);

            // Assert
            result.SuccessfulOutcome.ShouldBeTrue();
            result.Request.ShouldNotBeNullOrEmpty();
            result.Accounts.Length.ShouldBeGreaterThan(0);
            result.Identity.Names.Length.ShouldBeGreaterThan(0);
            result.Item.ShouldNotBeNull();
        }

        [TestMethod]
        public async Task FetchIncomeAsync_should_retrieve_the_monthly_earnings_of_an_userAsync()
        {
            // Arrange
            using PlaidClient client = new PlaidClient(Environment.Sandbox);
            GetIncomeRequest request = new GetIncomeRequest { }.UseDefaults();

            // Act
            GetIncomeResponse result = await client.FetchUserIncomeAsync(request);
            if (result.Exception?.ErrorCode == "INVALID_PRODUCT")
                Assert.Inconclusive(Test.Properties.Resources.AuthorizationDenialMessage);

            // Assert
            result.SuccessfulOutcome.ShouldBeTrue();
            result.Request.ShouldNotBeNullOrEmpty();
            result.Income.Streams.Length.ShouldBeGreaterThan(0);
            result.Income.LastYearIncome.ShouldBeGreaterThan(0);
            result.Item.ShouldNotBeNull();
        }
    }
}
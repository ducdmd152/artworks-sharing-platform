using System.Text;
using ArtHubRepository.Interface;
using ArtHubService.Interface;
using Microsoft.Extensions.Configuration;

namespace ArtHubService.Service;

public class PaypalService : IPaypalService
{
    private readonly IConfiguration _configuration;
    private readonly IAccountRepository accountRepository;
    private readonly IFeeRepository feeRepository;

    public PaypalService(IConfiguration configuration, IAccountRepository accountRepository, IFeeRepository feeRepository)
    {
        _configuration = configuration;
        this.accountRepository = accountRepository;
        this.feeRepository = feeRepository;
    }

    private double feeSub;

    public async Task<string> CreateSubscription(string audienceEmail, string creatorEmail)
    {
        feeSub = this.feeRepository.GetFeeByCreatorEmail(creatorEmail);

        var response = await CreateSubscriptionPlan("Arthub Subscribe", "Monthly subscription", "MONTH", "REGULAR", 1, 12, "USD", "TIERED", "false", "ACTIVE");
        string planId =  Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(response).id;
        return await  AddSubscription(planId, audienceEmail);
   

    }
    public async Task<string> CreateSubscriptionPlan(string name, string description, string billingCycleFrequency, string billingCycleTenureType, int billingCycleSequence, int billingCycleTotalCycles, string currencyCode, string pricingScheme, string quantitySupported, string status)
    {
        var productId = await CreateProduct("Test product", "Product for testing"); // Replace XXXXXXXXXXXX with your actual product ID

        var accessToken = await GetAccessToken();
        string result = await CreatePlan(name, description, billingCycleFrequency, billingCycleTenureType, billingCycleSequence, billingCycleTotalCycles, currencyCode, quantitySupported, status, productId, accessToken);
        return result;
    }

    private async Task<string> CreatePlan(string name, string description, string billingCycleFrequency, string billingCycleTenureType, int billingCycleSequence, int billingCycleTotalCycles, string currencyCode, string quantitySupported, string status, string productId, string accessToken)
    {
        var client = new System.Net.Http.HttpClient();
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

        var requestUrl = _configuration["PAYPAL_URL"] + "/v1/billing/plans";

        var requestData = new
        {
            product_id = productId,
            name,
            description,
            billing_cycles = new[]
            {
            new
            {
                frequency = new
                {
                    interval_unit = billingCycleFrequency,
                    interval_count = 1
                },
                tenure_type = billingCycleTenureType,
                sequence = billingCycleSequence,
                total_cycles = billingCycleTotalCycles,
                pricing_scheme = new
                {
                    fixed_price = new
                    {
                        value = this.feeSub,
                        currency_code = currencyCode
                    }
                }
            }
        },
            payment_preferences = new
            {
                auto_bill_outstanding = true,
                setup_fee = new
                {
                    currency_code = currencyCode,
                    value = this.feeSub,
                },
                setup_fee_failure_action = "CONTINUE",
                payment_failure_threshold = 3
            },
            taxes = new
            {
                percentage = "0",
                inclusive = false
            },
            quantity_supported = quantitySupported,
            status
        };

        var requestDataJson = Newtonsoft.Json.JsonConvert.SerializeObject(requestData);
        var requestContent = new StringContent(requestDataJson, Encoding.UTF8, "application/json");

        var response = await client.PostAsync(requestUrl, requestContent);
        var result = response.Content.ReadAsStringAsync().Result;
        return result;
    }

    public async Task<string> CreateProduct(string name, string description)
    {
        var accessToken = await GetAccessToken();

        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

        var requestUrl = _configuration["PAYPAL_URL"] + "/v1/catalogs/products";

        var requestData = new
        {
            name,
            description
        };

        var requestDataJson = Newtonsoft.Json.JsonConvert.SerializeObject(requestData);
        var requestContent = new StringContent(requestDataJson, Encoding.UTF8, "application/json");

        var response = await client.PostAsync(requestUrl, requestContent);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var productId = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(responseContent).id;
            return productId;
        }
        else
        {
            throw new Exception($"Failed to create product. {responseContent}");
        }
    }

    public async Task<string> AddSubscription(string planId, string audienceEmail)
    {
        var audienceAccount = this.accountRepository.GetAccountByEmail(audienceEmail);
        if (audienceEmail != default)
        {
            var _httpClient = new HttpClient();
            string url = _configuration["PAYPAL_URL"] + "/v1/billing/subscriptions";
            string accessToken = await GetAccessToken(); // Replace with your actual access token

            var requestData = new
            {
                plan_id = planId,
                start_time = DateTime.Now.AddDays(1),

                shipping_amount = new
                {
                    currency_code = "USD",
                    value = "0.00"
                },
                subscriber = new
                {
                    name = new
                    {
                        given_name = audienceAccount.LastName,
                        surname = audienceAccount.FirstName
                    },
                    email_address = audienceAccount.Email,
                    shipping_address = new
                    {
                        name = new
                        {
                            full_name = string.Format("{0} {1}", audienceAccount.FirstName, audienceAccount.LastName),
                        },
                        address = new
                        {
                            address_line_1 = string.Empty,
                            address_line_2 = string.Empty,
                            admin_area_2 = string.Empty,
                            admin_area_1 = string.Empty,
                            postal_code = string.Empty,
                            country_code = "US"
                        }
                    }
                },
                application_context = new
                {
                    brand_name = "Arthub",
                    locale = "en-US",
                    shipping_preference = "SET_PROVIDED_ADDRESS",
                    user_action = "SUBSCRIBE_NOW",
                    return_url = "https://localhost:5000/CreatorExploration/Details?handler=Success",
                    cancel_url = "https://localhost:5000/CreatorExploration/Details?handler=Cancel"
                }
            };

            var requestContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.PostAsync(url, requestContent);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                dynamic response1 = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(responseContent);

                // Extract the approve URL from the links in the response
                string approveUrl = response1.links[0].href;

                // Redirect the user to the approve URL for manual approval
                return approveUrl;
                //  return Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(responseContent).id;
            }

            throw new Exception($"Failed to create subscription: {responseContent}");
        }

        throw new Exception($"Not found account with email {audienceEmail}");

    }
        
    private async Task<string> GetAccessToken()
    {
        
        var ClientID = _configuration["PAYPAL_CLIENT"];
        var ClientSecret = _configuration["PAYPAL_SECRET_KEY"];
        var client = new HttpClient();
        var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{ClientID}:{ClientSecret}"));
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);

        var requestUrl = _configuration["PAYPAL_URL"] + "/v1/oauth2/token";
        var requestData = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");

        var response = await client.PostAsync(requestUrl, requestData);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var accessToken = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(responseContent).access_token;
            return accessToken;
        }
        else
        {
            throw new Exception($"Failed to get access token. {responseContent}");
        }
    }
}
using Flurl;
using Flurl.Http;
using ModuleCustomer.Interfaces;
using ModuleCustomer.Models.Response;

namespace ModuleCustomer.Models
{
    public class DataModel : IDataModel
    {
        private FlurlClient _client;

        public DataModel()
        {
        }

        public DataModel(FlurlClient client)
        {
            Console.WriteLine("DataModel constructor working");
            _client = client;
            _client.BaseUrl = "http://localhost:7089/Orchestration/";
        }

        private List<CustomerResponse> customers;

        public List<CustomerResponse> Customers
        {
            get { return customers; }
            private set { customers = value; }
        }

        public async Task GetAllCustomersAsync()
        {
            Console.WriteLine("GetAllCustomersAsync");

            bool tryAgain = true;

            while (tryAgain)
            {

                try
                {
                    customers = await _client.BaseUrl.AppendPathSegments("UC_300_002_GetAllCustomers").GetJsonAsync<List<CustomerResponse>>();

                }
                catch (Exception ex)
                {
                    if (ex.InnerException == )
                    {

                    }
                }

            }
        }
    }
}
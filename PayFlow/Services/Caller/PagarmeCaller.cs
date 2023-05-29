using Domain.Pagarme;
using Microsoft.Extensions.Options;
using PayFlow.Services.Caller.Interfaces;
using RestSharp;

namespace PayFlow.Services.Caller
{
    public class PagarmeCaller : IPagarmeCaller
    {
        private readonly PagarmeCredentials _pagarmeOptions;

        public PagarmeCaller(IOptions<PagarmeCredentials> pagarme)
        {
            _pagarmeOptions = pagarme.Value;
        }

        public async Task<PagarmeResponse> CreateOrder(PagarmeOrders orders)
        {
            try
            {
                var client = new RestClient(_pagarmeOptions.Endpoint ?? "");
                var request = new RestRequest("orders", Method.Post)
                    .AddHeader("accept", "application/json")
                    .AddHeader("content-type", "application/json")
                    .AddJsonBody(orders)
                    .AddHeader("authorization", _pagarmeOptions.BasicKey ?? "");

                RestResponse<PagarmeResponse>? response = null;

                try
                {
                    response = await client.ExecuteAsync<PagarmeResponse>(request);
                }
                catch (Exception ex)
                {
                    throw new Exception("Ocorreu um erro ao tentar gerar uma transação com o Pagarme.", ex);
                }

                if (response.IsSuccessful)
                {
                    var status = new PagarmeResponse { Status = response?.Data?.Status };
                    return status;
                }
                else
                {
                    throw new Exception("A solicitação para o Pagarme não foi bem-sucedida. Status de resposta: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu uma falha ao se comunicar com o servidor.", ex);
            }
        }

        public async Task<object?> GetOrder(string order)
        {
            var client = new RestClient(_pagarmeOptions.Endpoint ?? "");
            var request = new RestRequest("orders", Method.Get)
            .AddHeader("accept", "application/json")
            .AddHeader("content-type", "application/json")
            .AddParameter("application/json", order, ParameterType.RequestBody)
            .AddHeader("authorization", _pagarmeOptions.BasicKey ?? "");

            var response = await client.ExecuteAsync<object>(request);

            return response.Content;
        }
    }
}


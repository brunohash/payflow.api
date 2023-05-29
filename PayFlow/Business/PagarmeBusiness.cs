using Domain.Pagarme;
using Infrastructure.Repository.Interfaces;
using PayFlow.Services.Caller.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PayFlow.Business
{
    public class PagarmeBusiness : Controller
    {
        private readonly IPaymentRepository _paymentMethodsRepository;
        private readonly IPagarmeCaller _pagarmeCaller;

        public PagarmeBusiness(IPaymentRepository paymentMethods, IPagarmeCaller pagarme)
        {
            _paymentMethodsRepository = paymentMethods;
            _pagarmeCaller = pagarme;
        }

        public async Task<object> CreateOrder(PagarmeOrders orders)
        {
            try
            {
                return await _pagarmeCaller.CreateOrder(orders);
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }

        public async Task<object?> GetOrder(string code)
        {
            return await _pagarmeCaller.GetOrder(code);
        }
    }
}


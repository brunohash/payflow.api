using Domain.Pagarme;

namespace PayFlow.Services.Caller.Interfaces
{
    public interface IPagarmeCaller
    {
        Task<PagarmeResponse> CreateOrder(PagarmeOrders orders);
        Task<object?> GetOrder(string code);
    }
}


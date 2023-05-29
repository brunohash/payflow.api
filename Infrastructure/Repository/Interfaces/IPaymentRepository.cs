using Domain;

namespace Infrastructure.Repository.Interfaces
{
	public interface IPaymentRepository
	{
        Task<IEnumerable<PaymentMethodsBigImage>> GetPaymentMethodsBigImages();

        Task<IEnumerable<PaymentMethodsSmallImage>> GetPaymentMethodsSmallImages();

        Task<BankTransfer> GetBankTransfer();
    }
}


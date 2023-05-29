using Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Domain.Pix;
using pix_payload_generator.net.Models.CobrancaModels;
using pix_payload_generator.net.Models.PayloadModels;

namespace PayFlow.Business
{
    public class PaymentBusiness : Controller
    {
        private readonly IPaymentRepository _paymentMethodsRepository;

        public PaymentBusiness(IPaymentRepository paymentMethods)
        {
            _paymentMethodsRepository = paymentMethods;
        }

        public async Task<object> GetPaymentMethods(string size)
        {
            try
            {
                object result = "";

                if (size == "small")
                {
                    result = await _paymentMethodsRepository.GetPaymentMethodsSmallImages();
                }
                else if (size == "big")
                {
                    result = await _paymentMethodsRepository.GetPaymentMethodsBigImages();
                }

                return result;
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }

        public async Task<object> TransferBank()
        {
            try
            {
                return await _paymentMethodsRepository.GetBankTransfer();
            }
            catch (Exception ex)
            {
                return new { error = ex.Message };
            }
        }

        public Task<PixResponse> CreatePix(CreatePix pix)
        {
            var cobranca = new Cobranca(_chave: pix.Key)
            {
                SolicitacaoPagador = pix.Description,
                Valor = new Valor
                {
                    Original = pix.Value
                }
            };

            var payload = cobranca.ToPayload(pix.TxtId, new Merchant(pix.Name, pix.City));

            PixResponse qrCode = new PixResponse
            {
                StringQrCode = payload.GenerateStringToQrCode(),
                Created_at = DateTime.Now
            };

            return Task.FromResult(qrCode);
        }
    }
}


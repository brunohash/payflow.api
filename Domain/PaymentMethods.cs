
namespace Domain
{
	public class PaymentMethodsBigImage
	{
		public int InternalId { get; set; }

		public string? Nome { get; set; }

		public string? BigImage { get; set; }

		public int Status { get; set; }
	}

    public class PaymentMethodsSmallImage
    {
        public int InternalId { get; set; }

        public string? Nome { get; set; }

        public string? SmallImage { get; set; }

        public int Status { get; set; }
    }
}


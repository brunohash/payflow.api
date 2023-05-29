
namespace Domain.Pagarme
{
    public class PagarmeOrders
    {
        public Customer? Customer { get; set; }

        public string? Code { get; set; }

        public IEnumerable<Items>? Items { get; set; }

        public IEnumerable<Payments>? Payments { get; set; }
    }

    public class Customer
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Document { get; set; }

        public string? Type { get; set; }

        public Phones? Phones { get; set; }

        public string? Document_type { get; set; }

        public string? Code { get; set; }
    }

    public class Items
    {
        public int Amount { get; set; }

        public string? Description { get; set; }

        public int Quantity { get; set; }

        public string? Code { get; set; }
    }

    public class Payments
    {
        public CreditCard? Credit_card { get; set; }
        public string? Payment_method { get; set; }
    }

    public class CreditCard
    {
        public Card? card { get; set; }

        public int Installments { get; set; } // Quantidade de parcelas

        public string? Statement_descriptor { get; set; } // Texto exibido na fatura do cartão. Max: 22 caracteres para clientes Gateway; 13 para clientes PSP
    }

    public class Card
    {
        public Address? Address { get; set; }

        public string? Number { get; set; }

        public string? Holder_name { get; set; }

        public int Exp_month { get; set; }

        public int Exp_year { get; set; }

        public int Cvv { get; set; }
    }

    public class Address
    {
        public string? Line_1 { get; set; }

        public int Zip_code { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Country { get; set; }
    }

    public class Phones
    {
        public MobilePhone? mobile_phone { get; set; }
    }

    public class MobilePhone
    {
        public string? country_code { get; set; }
        public string? area_code { get; set; }
        public string? number { get; set; }
    }

    public class Resume
    {
        public string? Id { get; set; }
        public string? IdOr { get; set; }
        public string? IdTran { get; set; }
        public string? Gateway_id { get; set; }
        public string? Status { get; set; }
        public string? Acquirer_message { get; set; }
        public string? Acquirer_return_code { get; set; }
    }
}


// See https://aka.ms/new-console-template for more information

namespace Chapter1
{
    record Product(string Name, decimal Price, bool IsFood);

    record Order(Product Product, int Quantity)
    {
        public decimal NetPrice => Product.Price * Quantity;
    }

    record Address(string Country);

    public class VatStrategy
    {
        static decimal RateByCountry(string country)
        => country switch
        {
            "it" => 0.22m,
            "jp" => 0.08m,
            _ => throw new ArgumentException($"Missing rate for {country}")
        };

        static decimal Vat(Address address, Order order) => address switch
        {
            Address("de") => DeVat(order),
            Address(var country) => Vat(RateByCountry(address.Country), order)
        };

        static decimal DeVat(Order order) => order.NetPrice * (order.Product.isFood ? 0.08m : 0.2m);

        static decimal Vat(decimal rate, Order order) => order.NetPrice * rate;
    }
}



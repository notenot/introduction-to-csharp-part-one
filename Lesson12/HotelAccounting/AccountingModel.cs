using System;

namespace HotelAccounting
{
    public class AccountingModel : ModelBase
    {
        private double price;

        public double Price
        {
            get { return price; }
            set
            {
                if (value < 0) throw new ArgumentException();
                price = value;
                Notify(nameof(Price));
                Notify(nameof(Total));
            }
        }

        private int nightsCount;

        public int NightsCount
        {
            get { return nightsCount; }
            set
            {
                if (value <= 0) throw new ArgumentException();
                nightsCount = value;
                Notify(nameof(NightsCount));
                Notify(nameof(Total));
            }
        }

        private double discount;

        public double Discount
        {
            get { return discount; }
            set
            {
                if (value > 100) throw new ArgumentException();
                discount = value;
                Notify(nameof(Discount));
                Notify(nameof(Total));
            }
        }

        private double total;

        public double Total
        {
            get { return price * nightsCount * (1 - discount / 100); }
            set
            {
                if (value < 0) throw new ArgumentException();
                total = value;
                Notify(nameof(Total));
                discount = 100 * (1 - total / (price * nightsCount));
                Notify(nameof(Discount));
            }
        }
    }
}

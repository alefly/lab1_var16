using System;

namespace CarFactory
{
    /// <summary>
    /// Заказ клиента
    /// </summary>
    public class Booking
    {
        public int Id { get; set; }

        public int ConsumerId { get; set; }

        public int CommodityId { get; set; }

        public int? WorkerId { get; set; }

        public int Count { get; set; }

        public decimal SumPrice { get; set; }

        public BookingStatus Status { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime? DateWork { get; set; }
    }
}

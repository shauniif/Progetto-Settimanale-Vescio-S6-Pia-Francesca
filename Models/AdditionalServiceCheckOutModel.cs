namespace Progetto_Settimanale_Vescio_Pia_Francesca.Models
{
    public class AdditionalServiceCheckOutModel
    {
        public string TypeOfService { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal TotalCost { get; set; }
    }
}

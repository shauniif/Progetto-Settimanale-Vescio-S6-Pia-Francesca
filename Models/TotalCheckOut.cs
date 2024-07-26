namespace Progetto_Settimanale_Vescio_Pia_Francesca.Models
{
    public class TotalCheckOut
    {
       public string NameCustomer { get; set; }

       public InformationOfTheCheckOut RoomofCustomer { get; set; }

       public IEnumerable<AdditionalServiceCheckOutModel> TolalAdditionalService {  get; set; }
        
       public TotalToPayModel TotalToPay { get; set; }

    }
}

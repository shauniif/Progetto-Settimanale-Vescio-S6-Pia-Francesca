namespace Progetto_Settimanale_Vescio_Pia_Francesca.Models
{
    public class TotalCheckOut
    {
       public string NameCustomer { get; set; }

       public InformationOfTheCheckOut RoomofCustomer { get; set; }

       public Dictionary<string, int> TolalAdditionalService {  get; set; }
        
       public decimal TotalToPay { get; set; }

    }
}

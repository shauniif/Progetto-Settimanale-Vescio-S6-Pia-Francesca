namespace Progetto_Settimanale_Vescio_Pia_Francesca.Services.Data
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public List<string> Roles { get; set; } = [];
    }
}

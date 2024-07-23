namespace Progetto_Settimanale_Vescio_Pia_Francesca.Services.Password_Crypth_Implementations
{
    public interface IPasswordEncoder
    {
        string Encode(string password);
        bool IsSame(string plainText, string codedText);

    }
}

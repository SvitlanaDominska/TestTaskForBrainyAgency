namespace TestProjectSD.Models
{
    public class Auth
    {
        public int RowId { get; set; }
        public string Name { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
    }
}

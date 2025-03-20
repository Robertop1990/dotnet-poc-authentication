namespace WU.Poc.Authentication.Domain
{
    public class ApiKey
    {
        public int Id { get; set; }
        public string? Key { get; set; }
        public  bool IsActive { get; set; }
    }
}

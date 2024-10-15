namespace EntryApi.Models
{
    public class BaseRequest
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }

        public string TokenSecret { get; set; }

        public string TokenType { get; set; }

        public long RoleId { get; set; }
    }
}

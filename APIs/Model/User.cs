namespace APIs.Model
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string password { get; set; }
        public string PhoneNumber { get; set; }
        public string role { get; set; }
        public string ?RefreshToken { get; set; }
        public string ?RefreshTokenExpiryDate { get; set; }
        
    }
}

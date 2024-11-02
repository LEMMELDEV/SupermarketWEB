namespace SupermarketWEB.Models
{
    public class Provider
    {
        public int Id { get; set; } // Será la llave primaria
        public string Document { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}

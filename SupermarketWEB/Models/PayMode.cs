using System.ComponentModel.DataAnnotations.Schema;

namespace SupermarketWEB.Models
{
    public class PayMode
    {
        // [Key] -> Anotación si la propiedad no se llama Id, ejemplo ProductId
        public int Id { get; set; } // Será la llave primaria
        public string Name { get; set; }
        public string Observation { get; set; }

    }
}

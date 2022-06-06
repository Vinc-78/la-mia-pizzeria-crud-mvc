using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Models

{
    public class Pizza
    {
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        public string Id { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [StringLength(15, ErrorMessage = "Il nome non può avere più di 15 caratteri")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [MiaValidazione.CinqueParole]
        public string Descrizione { get; set; }

        public string? ImgPath { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [Range(1, 20, ErrorMessage = "Il prezzo può variare da 1 a 20 euro")]

        public string Prezzo { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]

        public IFormFile File { get; set; }
    }

    public class listaPizze
    {
        public List<Pizza> pizzas { get; set; }
        public listaPizze()
        {
            pizzas = new List<Pizza>();
        }
    }
}

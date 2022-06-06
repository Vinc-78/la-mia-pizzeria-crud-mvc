using Microsoft.AspNetCore.Mvc;
using la_mia_pizzeria_static.Models;


namespace la_mia_pizzeria_static.Controllers
{
    public class Post : Controller
    {
        public IActionResult FormNuovaPizza()
        {
            string nuovoId = Convert.ToString(PizzaController.pizze.pizzas.Count() + 1);

            Pizza nuovaPizza = new Pizza()
            {
                
                Id = nuovoId,
                Nome = " ",
                Descrizione = " ",
                ImgPath = " ",
                Prezzo = " "

            };
            return View(nuovaPizza);
        }

        [HttpPost] 
        [ValidateAntiForgeryToken]
        public IActionResult CreaSchedaPizza(Pizza nuovaPizza)
        {

            if (!ModelState.IsValid)
            {
                return View("FormNuovaPizza", nuovaPizza);
            }


            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\File");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            FileInfo fileInfo = new FileInfo(nuovaPizza.File.FileName);

            string fileName = nuovaPizza.Nome.Trim().ToLower() + fileInfo.Extension.Trim().ToLower();


            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                nuovaPizza.File.CopyTo(stream);
            }

            Pizza PizzaCreata = new Pizza()
            {
                Id = nuovaPizza.Id,
                Nome = nuovaPizza.Nome,
                Descrizione = nuovaPizza.Descrizione,
                ImgPath = "/File/" + fileName,
                Prezzo = nuovaPizza.Prezzo,
                
                

            };

            PizzaController.pizze.pizzas.Add(PizzaCreata);  //aggiunge nella lista delle pizze dell'index

            return View(PizzaCreata);


            
        }
    }
}

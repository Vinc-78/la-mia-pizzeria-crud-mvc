using Microsoft.AspNetCore.Mvc;
using la_mia_pizzeria_static.Models;
using System.Diagnostics;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        public static listaPizze pizze = null;

        
        public IActionResult Index()
        {
            if (pizze ==null)
            {
                pizze = new listaPizze();

                Pizza pizza1 = new Pizza()
                {
                    Id = "1",
                    Nome = "Margherita",
                    Descrizione = "Pizza con mozzarella e pomodoro",
                    ImgPath = "/img/pizza1.jpg",
                    Prezzo = "5.00"

                };

                pizze.pizzas.Add(pizza1);

                Pizza pizza2 = new Pizza()
                {
                    Id = "2",
                    Nome = "Filetto",
                    Descrizione = "Pizza con pomodorini tagliatti a fette",
                    ImgPath = "/img/pizza2.jpg",
                    Prezzo = "7.00"

                };

                pizze.pizzas.Add(pizza2);

                Pizza pizza3 = new Pizza()
                {
                    Id = "3",
                    Nome = "Al salame",
                    Descrizione = "Pizza con fette di salame",
                    ImgPath = "/img/pizza3.jpg",
                    Prezzo = "6.50"

                };

                pizze.pizzas.Add(pizza3);

            }

            return View(pizze);
        }


        public IActionResult Show(int id)
        {
            //versione con foreach ( crticità con indice quando rimane 1 elemento )
            //foreach (var ele in pizze.pizzas)
            //{
            //    if (ele.Id == Convert.ToString(id)) { return View("Show", pizze.pizzas[id-1]); }

            //}

            //return NotFound(" La pizza con l'id " + id + " non è stato trovato ");

            //versione con where ( testata anche con un solo elemento )
            Pizza? PizzaDaMostrare = pizze.pizzas.Where(p => p.Id == Convert.ToString(id)).FirstOrDefault();
            if (PizzaDaMostrare != null)
            {
                return View("Show", PizzaDaMostrare);
            }
            else { return NotFound(" La pizza con l'id " + id + " non è stato trovato "); }

        }


        public IActionResult Modifica(int id)
        {
            
            Pizza? PizzaDaModificare = pizze.pizzas.Where(p=>p.Id==Convert.ToString(id)).FirstOrDefault();
            if (PizzaDaModificare != null)
            {
                return View("Modifica", PizzaDaModificare);
            }
            else { return NotFound(" La pizza con l'id " + id + " non è stato trovato ");  }

        }

        public IActionResult ModificaSecondaVersione(int id)
        {
            Pizza? PizzaDaModificare = pizze.pizzas.Where(p => p.Id == Convert.ToString(id)).FirstOrDefault();
            if (PizzaDaModificare != null)
            {
                return View("ModificaSecondaVersione", PizzaDaModificare);
            }
            else { return NotFound(" La pizza con l'id " + id + " non è stato trovato "); }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Pizza Modificata)
        {
            if (!ModelState.IsValid)
            {
                return View("Modifica", Modificata);
            }

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\File");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            FileInfo fileInfo = new FileInfo(Modificata.File.FileName);

            string fileName = Modificata.Nome.Trim().ToLower() + fileInfo.Extension.Trim().ToLower();


            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                Modificata.File.CopyTo(stream);
            }


            Pizza? pizzaDaModificare = null;

            pizzaDaModificare = pizze.pizzas
                     .Where(pizza => pizza.Id == Modificata.Id)
                     .FirstOrDefault();

            if (pizzaDaModificare != null)
            {

                pizzaDaModificare.Id = Modificata.Id;
                pizzaDaModificare.Nome = Modificata.Nome;
                pizzaDaModificare.Descrizione = Modificata.Descrizione;
                pizzaDaModificare.ImgPath = "/File/" + fileName;
                pizzaDaModificare.Prezzo = Modificata.Prezzo.ToString();
                
                

                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult Elimina(int id)
        {
            Pizza? pizzaDaRimuovere = pizze.pizzas
                    .Where(pizza => pizza.Id == Convert.ToString(id))
                    .FirstOrDefault();

            if (pizzaDaRimuovere != null)
            {
                pizze.pizzas.Remove(pizzaDaRimuovere);
                

                return RedirectToAction("Index", "Pizza");
            }
            else
            {
                return NotFound(" La pizza con l'id " + id + " non è stato trovato ");
            }

           

        }


        public IActionResult EliminaSecondaVersione(int id)
        {
            int postIndexToRemove = -1;

            // cerchiamo l'elemento da modificare con un determinato id con un foreach 
            int currentIndex = 0;

            foreach (var pizza in pizze.pizzas)
            {
                if (pizza.Id ==Convert.ToString(id))
                {
                    postIndexToRemove = currentIndex;
                }

                currentIndex++;
            }

            if (postIndexToRemove != -1)
            {
                pizze.pizzas.RemoveAt(postIndexToRemove);

                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }



        }




    }
}

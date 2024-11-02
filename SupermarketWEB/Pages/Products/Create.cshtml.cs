using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.Products
{
    public class CreateModel : PageModel
    {
        // campo privado para almacenar el contexto de la base de datos.
        private readonly SupermarketContext _context;

        // constructor para inicializar el contexto.
        public CreateModel(SupermarketContext context)
        {
            _context = context;
        }

        // método para manejar solicitudes GET (mostrar el formulario).
        public IActionResult OnGet()
        {
            // retornar la vista para mostrar el formlario de creación de categorías.
            return Page();
        }

        // propiedad para enlazar los datos del formulario
        [BindProperty]
        public Product Product { get; set; } = default!;

        // metodo para manejar solicitudes POST (guardar la nueva categoría).
        public async Task<IActionResult> OnPostAsync()
        {
            //validación: si el modelo no es válido o el contexto o la categoría son nulos, se vuelve a mostrar la página.
            if (!ModelState.IsValid || _context.Products == null || Product == null)
            {
                //return Page();
            }

            // agregar la nueva categoría al contexto de la base de datos
            _context.Products.Add(Product);

            // guardar los cambios en la base de datos de forma asíncrona
            await _context.SaveChangesAsync();

            // redirige a la página de índice después de guardar la categoría
            return RedirectToPage("./Index");
        }
    }
}

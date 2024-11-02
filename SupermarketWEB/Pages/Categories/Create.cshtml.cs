using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.Categories
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

        // m�todo para manejar solicitudes GET (mostrar el formulario).
        public IActionResult OnGet()
        {
            // retornar la vista para mostrar el formlario de creaci�n de categor�as.
            return Page();
        }

        // propiedad para enlazar los datos del formulario
        [BindProperty]
        public Category Category { get; set; } = default!;

        // metodo para manejar solicitudes POST (guardar la nueva categor�a).
        public async Task<IActionResult> OnPostAsync()
        {
            //validaci�n: si el modelo no es v�lido o el contexto o la categor�a son nulos, se vuelve a mostrar la p�gina.
            if (!ModelState.IsValid || _context.Categories == null || Category == null)
            {
                return Page();
            }

            // agregar la nueva categor�a al contexto de la base de datos
            _context.Categories.Add(Category);

            // guardar los cambios en la base de datos de forma as�ncrona
            await _context.SaveChangesAsync();

            // redirige a la p�gina de �ndice despu�s de guardar la categor�a
            return RedirectToPage("./Index");
        }
    }
}

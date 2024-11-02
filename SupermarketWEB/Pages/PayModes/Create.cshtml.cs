using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.PayModes
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
            // retornar la vista para mostrar el formulario de creación de métodos de pago.
            return Page();
        }

        // propiedad para enlazar los datos del formulario
        [BindProperty]
        public PayMode PayMode { get; set; } = default!;

        // método para manejar solicitudes POST (guardar el nuevo método de pago).
        public async Task<IActionResult> OnPostAsync()
        {
            // validación: si el modelo no es válido o el contexto o el método de pago son nulos, se vuelve a mostrar la página.
            if (!ModelState.IsValid || _context.PayModes == null || PayMode == null)
            {
                return Page();
            }

            // agregar el nuevo método de pago al contexto de la base de datos
            _context.PayModes.Add(PayMode);

            // guardar los cambios en la base de datos de forma asíncrona
            await _context.SaveChangesAsync();

            // redirige a la página de índice después de guardar el método de pago
            return RedirectToPage("./Index");
        }
    }
}

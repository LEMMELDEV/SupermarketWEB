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

        // m�todo para manejar solicitudes GET (mostrar el formulario).
        public IActionResult OnGet()
        {
            // retornar la vista para mostrar el formulario de creaci�n de m�todos de pago.
            return Page();
        }

        // propiedad para enlazar los datos del formulario
        [BindProperty]
        public PayMode PayMode { get; set; } = default!;

        // m�todo para manejar solicitudes POST (guardar el nuevo m�todo de pago).
        public async Task<IActionResult> OnPostAsync()
        {
            // validaci�n: si el modelo no es v�lido o el contexto o el m�todo de pago son nulos, se vuelve a mostrar la p�gina.
            if (!ModelState.IsValid || _context.PayModes == null || PayMode == null)
            {
                return Page();
            }

            // agregar el nuevo m�todo de pago al contexto de la base de datos
            _context.PayModes.Add(PayMode);

            // guardar los cambios en la base de datos de forma as�ncrona
            await _context.SaveChangesAsync();

            // redirige a la p�gina de �ndice despu�s de guardar el m�todo de pago
            return RedirectToPage("./Index");
        }
    }
}

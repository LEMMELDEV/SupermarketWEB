using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.PayModes
{
    public class DeleteModel : PageModel
    {
        private readonly SupermarketContext _context;

        public DeleteModel(SupermarketContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PayMode PayMode { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.PayModes == null)
            {
                return NotFound();
            }

            var payMode = await _context.PayModes.FirstOrDefaultAsync(m => m.Id == id);

            if (payMode == null)
            {
                return NotFound();
            }
            else
            {
                PayMode = payMode;
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            // Verifica si el id es nulo o si el contexto de m�todos de pago es nulo.
            if (id == null || _context.PayModes == null)
            {
                // Si alguna de las condiciones anteriores es verdadera, se devuelve un error 404 (Not Found).
                return NotFound();
            }

            // Busca el m�todo de pago con el id proporcionado de forma as�ncrona.
            var payMode = await _context.PayModes.FindAsync(id);

            // Si el m�todo de pago no se encuentra, se devuelve un error 404.
            if (payMode == null)
            {
                return NotFound();
            }

            // Si se encuentra el m�todo de pago, se procede a eliminarlo.
            PayMode = payMode; // Asigna el m�todo de pago encontrado a la propiedad PayMode.
            _context.PayModes.Remove(PayMode); // Elimina el m�todo de pago del contexto.
            await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos de forma as�ncrona.

            // Redirige a la p�gina de �ndice despu�s de eliminar el m�todo de pago.
            return RedirectToPage("./Index");
        }
    }
}

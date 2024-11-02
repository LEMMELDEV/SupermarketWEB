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
            // Verifica si el id es nulo o si el contexto de métodos de pago es nulo.
            if (id == null || _context.PayModes == null)
            {
                // Si alguna de las condiciones anteriores es verdadera, se devuelve un error 404 (Not Found).
                return NotFound();
            }

            // Busca el método de pago con el id proporcionado de forma asíncrona.
            var payMode = await _context.PayModes.FindAsync(id);

            // Si el método de pago no se encuentra, se devuelve un error 404.
            if (payMode == null)
            {
                return NotFound();
            }

            // Si se encuentra el método de pago, se procede a eliminarlo.
            PayMode = payMode; // Asigna el método de pago encontrado a la propiedad PayMode.
            _context.PayModes.Remove(PayMode); // Elimina el método de pago del contexto.
            await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos de forma asíncrona.

            // Redirige a la página de índice después de eliminar el método de pago.
            return RedirectToPage("./Index");
        }
    }
}

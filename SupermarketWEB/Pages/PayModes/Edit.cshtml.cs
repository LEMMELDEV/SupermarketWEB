using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.PayModes
{
    public class EditModel : PageModel
    {
        private readonly SupermarketContext _context;

        public EditModel(SupermarketContext context)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page(); // Si el modelo no es válido, vuelve a la misma página
            }

            _context.Attach(PayMode).State = EntityState.Modified; // Marca la entidad como modificada

            try
            {
                await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos
            }
            catch (DbUpdateConcurrencyException) // Si ocurre un error de concurrencia
            {
                if (!PayModeExists(PayMode.Id)) // Si el método de pago ya no existe
                {
                    return NotFound(); // Devuelve un error 404
                }
                else
                {
                    throw; // Re lanza la excepción para que sea manejada por niveles superiores
                }
            }

            return RedirectToPage("./Index"); // Redirige a la página de índice
        }

        private bool PayModeExists(int id)
        {
            return (_context.PayModes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

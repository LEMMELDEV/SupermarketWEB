using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.Providers
{
    public class DeleteModel : PageModel
    {
        private readonly SupermarketContext _context;

        public DeleteModel(SupermarketContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Provider Provider { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Providers == null)
            {
                return NotFound();
            }

            var provider = await _context.Providers.FirstOrDefaultAsync(m => m.Id == id);

            if (provider == null)
            {
                return NotFound();
            }
            else
            {
                Provider = provider;
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            // Verifica si el id es nulo o si el contexto de proveedores es nulo.
            if (id == null || _context.Providers == null)
            {
                // Si alguna de las condiciones anteriores es verdadera, se devuelve un error 404 (Not Found).
                return NotFound();
            }

            // Busca el proveedor con el id proporcionado de forma asíncrona.
            var provider = await _context.Providers.FindAsync(id);

            // Si el proveedor no se encuentra, se devuelve un error 404.
            if (provider == null)
            {
                return NotFound();
            }

            // Si se encuentra el proveedor, se procede a eliminarlo.
            Provider = provider; // Asigna el proveedor encontrado a la propiedad Provider.
            _context.Providers.Remove(Provider); // Elimina el proveedor del contexto.
            await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos de forma asíncrona.

            // Redirige a la página de índice después de eliminar el proveedor.
            return RedirectToPage("./Index");
        }
    }
}

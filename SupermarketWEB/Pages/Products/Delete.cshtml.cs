using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.Products
{
    public class DeleteModel : PageModel
    {
        private readonly SupermarketContext _context;

        public DeleteModel(SupermarketContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            else
            {
                Product = product;
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            // Verifica si el id es nulo o si el contexto de productos es nulo.
            if (id == null || _context.Products == null)
            {
                // Si alguna de las condiciones anteriores es verdadera, se devuelve un error 404 (Not Found).
                return NotFound();
            }

            // Busca el producto con el id proporcionado de forma asíncrona.
            var product = await _context.Products.FindAsync(id);

            // Si el producto no se encuentra, se devuelve un error 404.
            if (product == null)
            {
                return NotFound();
            }

            // Si se encuentra el producto, se procede a eliminarlo.
            Product = product; // Asigna el producto encontrado a la propiedad Product.
            _context.Products.Remove(Product); // Elimina el producto del contexto.
            await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos de forma asíncrona.

            // Redirige a la página de índice después de eliminar el producto.
            return RedirectToPage("./Index");
        }
    }
}

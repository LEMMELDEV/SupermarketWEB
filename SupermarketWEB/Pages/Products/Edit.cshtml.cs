using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly SupermarketContext _context;

        public EditModel(SupermarketContext context)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                //return Page(); // Si el modelo no es válido, vuelve a la misma página
            }

            _context.Attach(Product).State = EntityState.Modified; // Marca la entidad como modificada

            try
            {
                await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos
            }
            catch (DbUpdateConcurrencyException) // Si ocurre un error de concurrencia
            {
                if (!ProductExists(Product.Id)) // Si la categoría ya no existe
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

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}

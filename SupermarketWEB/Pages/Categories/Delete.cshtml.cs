using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly SupermarketContext _context;

        public DeleteModel(SupermarketContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);

            if (category == null)
            {
                return NotFound();
            }
            else
            {
                Category = category;
                return Page();
            }
        }


            public async Task<IActionResult> OnPostAsync(int? id)
            {
                // Verifica si el id es nulo o si el contexto de categorías es nulo.
                if (id == null || _context.Categories == null)
                {
                    // Si alguna de las condiciones anteriores es verdadera, se devuelve un error 404 (Not Found).
                    return NotFound();
                }

                // Busca la categoría con el id proporcionado de forma asíncrona.
                var category = await _context.Categories.FindAsync(id);

                // Si la categoría no se encuentra, se devuelve un error 404.
                if (category == null)
                {
                    return NotFound();
                }

                // Si se encuentra la categoría, se procede a eliminarla.
                Category = category; // Asigna la categoría encontrada a la propiedad Category.
                _context.Categories.Remove(Category); // Elimina la categoría del contexto.
                await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos de forma asíncrona.

                // Redirige a la página de índice después de eliminar la categoría.
                return RedirectToPage("./Index");
            }
        }
  
}

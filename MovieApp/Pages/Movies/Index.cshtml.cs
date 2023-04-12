using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Models;

namespace MovieApp.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly MovieApp.Data.MovieAppContext _context;

        public IndexModel(MovieApp.Data.MovieAppContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public async Task OnGetAsync()
        {
            if (_context.Movie != null)
            {
                var movies = from m in _context.Movie select m;
                if(!string.IsNullOrEmpty(SearchString))
                {
                    movies = movies.Where(m => m.Title.Contains(SearchString));
                }
                Movie = await movies.ToListAsync();
            }
        }
    }
}

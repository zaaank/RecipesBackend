using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendRecipes.API.Data;
using BackendRecipes.Data.Dto.Recipe;
using AutoMapper;
using BackendRecipes.Data.Entities;

namespace BackendRecipes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly RecipesDbContext _context;
        private readonly IMapper _mapper;

        public RecipeController(RecipesDbContext context, IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }

        // GET: api/Recipe
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes()
        {
          if (_context.Recipes == null)
          {
              return NotFound();
          }
            return await _context.Recipes.ToListAsync();
        }

        // GET: api/Recipe/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipe(int id)
        {
          if (_context.Recipes == null)
          {
              return NotFound();
          }
            var recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            return recipe;
        }

        // PUT: api/Recipe/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipe(int id, Recipe recipe)
        {
            if (id != recipe.Id)
            {
                return BadRequest();
            }

            _context.Entry(recipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Recipe
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AddRecipe>> PostRecipe(AddRecipe recipe)
        {
            if (_context.Recipes == null)
            {
                return Problem("Entity set 'RecipesDbContext.Recipes'  is null.");
            }
            List<Ingredient> ingredient = new List<Ingredient>();
            recipe.IngredientIds.ForEach(ingreditentId =>
            {
                ingredient.Add(_context.Ingredients.Find(ingreditentId));
            });
            var origRecipe = new Recipe
            {
                Name = recipe.Name,
                Directions = recipe.Directions,
                Group = recipe.Group,
                Ingredients = ingredient,
            };
            await _context.Recipes.AddAsync(origRecipe);



            await _context.SaveChangesAsync();



            return Ok();
        }

        // DELETE: api/Recipe/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            if (_context.Recipes == null)
            {
                return NotFound();
            }
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecipeExists(int id)
        {
            return (_context.Recipes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

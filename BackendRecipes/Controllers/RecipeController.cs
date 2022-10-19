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

        // GET: api/Recipe/Dinner
        [HttpGet("{{group}}")]
        public async Task<ActionResult<IEnumerable<GetRecipeByGroupDto>>> GetRecipes(string group)
        {
            if (_context.Recipes == null)
            {
                return NotFound();
            }
            List<GetRecipeByGroupDto> customer1 = await (from recipes in _context.Recipes
                                                         join ingredientRecipes in _context.IngredientRecipes
                                                        on recipes.Id equals ingredientRecipes.RecipeId
                                                         where recipes.Group == @group
                                                         group ingredientRecipes.IngredientId
                                                         by
                                                         new {
                                                            recipes.Id,
                                                            recipes.Name,
                                                            recipes.Group, 
                                                            recipes.Directions
                                                        } into g
                                                         select new GetRecipeByGroupDto
                                                        {
                                                          Id = g.Key.Id,
                                                          Name = g.Key.Name,
                                                          Group = g.Key.Group,
                                                          Directions = g.Key.Directions,
                                                          ingredientIds = g.ToList()
                                                        }).ToListAsync();
            return Ok(customer1);
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

            var newIngredient = _mapper.Map<Recipe>(recipe);
            await _context.Recipes.AddAsync(newIngredient);
            await _context.SaveChangesAsync();
            recipe.IngredientIds.ForEach(ingreditentId =>
            {
                var newConnection = new IngredientRecipe
                {
                    IngredientId = ingreditentId,
                    RecipeId = newIngredient.Id
                };
                _context.IngredientRecipes.Add(newConnection);
            });


            try
            {
                await _context.SaveChangesAsync();
            }

            catch(Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("Something went wrong" + e.InnerException.Message);
            }
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

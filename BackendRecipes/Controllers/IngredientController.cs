using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendRecipes.API.Data;
using BackendRecipes.Data.Entities;
using BackendRecipes.Data.Dto.Ingredient;
using AutoMapper;

namespace BackendRecipes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly RecipesDbContext _context;
        private readonly IMapper _mapper;

        public IngredientController(RecipesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Ingredient
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAllIngredients>>> GetIngredients()
        {
          if (_context.Ingredients == null)
          {
              return NotFound();
          }
            var ingredients = await _context.Ingredients.ToListAsync();
            var mappedIngredients = _mapper.Map<List<GetAllIngredients>>(ingredients);

            return mappedIngredients;
        }

        // GET: api/Ingredient/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ingredient>> GetIngredient(int id)
        {
          if (_context.Ingredients == null)
          {
              return NotFound();
          }
            var ingredient = await _context.Ingredients.FindAsync(id);

            if (ingredient == null)
            {
                return NotFound();
            }

            return ingredient;
        }

        // PUT: api/Ingredient/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngredient(int id, Ingredient ingredient)
        {
            if (id != ingredient.Id)
            {
                return BadRequest();
            }

            _context.Entry(ingredient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredientExists(id))
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

        // POST: api/Ingredient
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PostIngredient>> PostIngredient(PostIngredient ingredient)
        {
          if (_context.Ingredients == null)
          {
              return Problem("Entity set 'RecipesDbContext.Ingredients'  is null.");
          }
            var newIngredient = _mapper.Map<Ingredient>(ingredient);
            await _context.Ingredients.AddAsync(newIngredient);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(PostIngredient), new { id = newIngredient.Id }, newIngredient);
        }

        // DELETE: api/Ingredient/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient(int id)
        {
            if (_context.Ingredients == null)
            {
                return NotFound();
            }
            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient == null)
            {
                return NotFound();
            }

            _context.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IngredientExists(int id)
        {
            return (_context.Ingredients?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

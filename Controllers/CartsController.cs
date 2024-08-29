using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using OrangeCoreApiTasks.Models;

namespace OrangeCoreApiTasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController(MyDbContext context) : ControllerBase
    {
        // GET: api/Carts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()
        {
            return await context.Carts.ToListAsync();
        }

        // GET: api/Carts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> GetCart(int id)
        {
            var cart = await context.Carts.FindAsync(id);

            if (cart == null)
            {
                return NotFound();
            }

            return cart;
        }

        [HttpGet("{id}/cartItems")]
        public IActionResult GetCartItems(int id)
        {
            var cart = context.Carts.Find(id);

            if (cart == null)
            {
                return NotFound();
            }

            return Ok(context.CartItems.Where(item => item.CartId == id).Select(item =>
            new
            {
                item.CartItemId,
                item.Product.ProductName,
                item.Product.ProductImage,
                item.Quantity,
                price = item.Product.Price * item.Quantity,
                item.ProductId
            }
            ).ToList());
        }

        // PUT: api/Carts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCart(int id, Cart cart)
        {
            if (id != cart.CartId)
            {
                return BadRequest();
            }

            context.Entry(cart).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartExists(id))
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

        // POST: api/Carts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cart>> PostCart(Cart cart)
        {
            context.Carts.Add(cart);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetCart", new { id = cart.CartId }, cart);
        }

        // DELETE: api/Carts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            var cart = await context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            context.Carts.Remove(cart);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool CartExists(int id)
        {
            return context.Carts.Any(e => e.CartId == id);
        }
    }
}

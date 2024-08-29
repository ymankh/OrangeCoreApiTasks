using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrangeCoreApiTasks.DTOs;
using OrangeCoreApiTasks.Models;
using Microsoft.Extensions.Logging;


namespace OrangeCoreApiTasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly ILogger<WeatherForecastController> _logger;

        public CartItemsController(MyDbContext context, ILogger<WeatherForecastController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/CartItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItem>>> GetCartItems()
        {
            _logger.LogInformation("CartItems.Get has benn called. and this is a test");
            return await _context.CartItems.ToListAsync();
        }

        // GET: api/CartItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CartItem>> GetCartItem(int id)
        {
            var cartItem = await _context.CartItems.FindAsync(id);

            if (cartItem == null)
            {
                return NotFound();
            }

            return cartItem;
        }

        // PUT: api/CartItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCartItem(int id, CartItem cartItem)
        {
            if (id != cartItem.CartItemId)
            {
                return BadRequest();
            }

            _context.Entry(cartItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartItemExists(id))
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

        // POST: api/CartItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostCartItem([FromBody] AddCartItemDto cartItem)
        {
            var cart = _context.Carts.Find(cartItem.CartId);
            if (cart == null)
                return BadRequest();
            var oldCartItem = _context.CartItems.FirstOrDefault(item => item.ProductId == cartItem.ProductId);
            if (oldCartItem == null)
            {
                var newCartItem = new CartItem
                {
                    CartId = cartItem.CartId,
                    ProductId = cartItem.ProductId,
                    Quantity = 1
                };
                _context.CartItems.Add(newCartItem);
                _context.SaveChanges();
                return Ok(cartItem);
            }

            oldCartItem.Quantity++;

            _context.CartItems.Update(oldCartItem);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut("decrement/{id}")]
        public IActionResult DecrementFromCart(int id)
        {
            var cartItem = _context.CartItems.Find(id);

            if (cartItem == null)
                return BadRequest();
            cartItem.Quantity--;
            if (cartItem.Quantity <= 0)
            {

                _context.CartItems.Remove(cartItem);
                _context.SaveChanges();
                return NoContent();
            }

            else
                _context.CartItems.Update(cartItem);
            _context.SaveChanges();
            return Ok(cartItem);
        }

        // DELETE: api/CartItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem == null)
            {
                return NotFound();
            }

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CartItemExists(int id)
        {
            return _context.CartItems.Any(e => e.CartItemId == id);
        }
    }
}

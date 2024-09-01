using Microsoft.Build.Framework;
using OrangeCoreApiTasks.Models;

namespace OrangeCoreApiTasks.DTOs
{
    public class AddCartItemDto
    {
        public int CartId { get; set; }

        public int ProductId { get; set; }
    }

    public class EditCartItemDto
    {
        public int Quantity { get; set; }
        public async Task<CartItem?> UpdateItem(int id, MyDbContext context)
        {
            var oldCartItem = await context.CartItems.FindAsync(id);
            if (oldCartItem == null) return null;
            oldCartItem.Quantity = Quantity;
            if (Quantity == 0)
                context.CartItems.Remove(oldCartItem);
            else
                context.CartItems.Update(oldCartItem);
            await context.SaveChangesAsync();
            return oldCartItem;
        }

    }

}

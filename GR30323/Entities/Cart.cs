using GR30323.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;


namespace GR30323.Domain.Entities
{
    public class Cart
    {
        public int Id { get; set; }

      
        [NotMapped] 
        public Dictionary<int, CartItem> CartItems { get; set; } = new();

       
        /// <param name="book">
        public virtual void AddToCart(Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book)); 

            if (CartItems.ContainsKey(book.Id))
            {
                CartItems[book.Id].Qty++;
            }
            else
            {
                CartItems.Add(book.Id, new CartItem
                {
                    Item = book,
                    Qty = 1
                });
            }
        }
        /// <param name="id">
        public virtual void RemoveItems(int id)
        {
            CartItems.Remove(id);
        }

        public virtual void ClearAll()
        {
            CartItems.Clear();
        }

        public void AddToCart(object data)
        {
            throw new NotImplementedException();
        }

        public int Count => CartItems.Sum(item => item.Value.Qty);

   
        public double TotalSum => CartItems.Sum(item => item.Value.Item.Price * item.Value.Qty);
    }
}


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

        /// <summary>
        /// Список объектов в корзине
        /// ключ - идентификатор объекта
        /// </summary>
        [NotMapped] // Убедитесь, что этот атрибут используется, если вы не хотите сохранять CartItems в БД
        public Dictionary<int, CartItem> CartItems { get; set; } = new();

        /// <summary>
        /// Добавить объект в корзину
        /// </summary>
        /// <param name="book">Добавляемый объект</param>
        public virtual void AddToCart(Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book)); // Проверка на null

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

        /// <summary>
        /// Удалить объект из корзины
        /// </summary>
        /// <param name="id">Идентификатор удаляемого объекта</param>
        public virtual void RemoveItems(int id)
        {
            CartItems.Remove(id);
        }

        /// <summary>
        /// Очистить корзину
        /// </summary>
        public virtual void ClearAll()
        {
            CartItems.Clear();
        }

        /// <summary>
        /// Количество объектов в корзине
        /// </summary>
        public int Count => CartItems.Sum(item => item.Value.Qty);

        /// <summary>
        /// Общая сумма объектов в корзине
        /// </summary>
        public double TotalSum => CartItems.Sum(item => item.Value.Item.Price * item.Value.Qty);
    }
}
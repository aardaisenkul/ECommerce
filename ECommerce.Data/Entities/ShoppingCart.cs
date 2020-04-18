using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Data.Entities
{
    public class ShoppingCart : Abstracts.Entity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}

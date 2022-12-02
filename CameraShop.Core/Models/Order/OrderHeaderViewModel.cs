using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraShop.Core.Models.Order
{
    public class OrderHeaderViewModel
    {
       public int Id { get; set; }
        
        public DateTime OrderDate { get; set; } 

        public double OrderTotal { get; set; }


    }
}

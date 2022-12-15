using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheWarehouse.Data;

namespace TheWarehouse.Web.Models
{
   
    public class CarritoItem
    {
        private Announcement _producto;

        public Announcement Producto
        {
            get { return _producto; }
            set { _producto = value; }
        }
        private int _cantidad;

        public int Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }

        public CarritoItem()
        {

        }

        public CarritoItem(Announcement producto, int cantidad)
        {
            this._producto = producto;
            this._cantidad = cantidad;
        }
    }
}

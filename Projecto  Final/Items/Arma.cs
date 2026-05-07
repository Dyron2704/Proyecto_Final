using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto__Final.Items
{
    internal class Arma : Item
    {
        int daño;

        public Arma(string nombre, string descripcion, int daño) : base(nombre, descripcion)
        {
            this.daño = daño;
            Tipo = "Arma";
        }

        public int Daño { get => daño; set => daño = value; }
    }
}

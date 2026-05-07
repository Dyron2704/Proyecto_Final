using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto__Final.Items
{
    internal class Pocion : Item
    {
        int cantidadCuracion;

        public Pocion(string nombre, string descripcion, int cantidadCuracion) : base(nombre, descripcion)
        {
            this.cantidadCuracion = cantidadCuracion;
            Tipo = "Pocion de Vida";
        }

        public int CantidadCuracion { get => cantidadCuracion; set => cantidadCuracion = value; }
    }
}

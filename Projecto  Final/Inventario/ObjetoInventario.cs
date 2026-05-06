using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto__Final.Inventarios
{
    internal class ObjetoInventario
    {
        string nombre;
        string tipo;
        int cantidad;

        public ObjetoInventario(string nombre, string tipo, int cantidad)
        {
            this.nombre = nombre;
            this.tipo = tipo;
            this.cantidad = cantidad;
        }

        public string Nombre { get => nombre; set => nombre = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
    }
}

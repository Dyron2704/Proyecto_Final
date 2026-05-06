using Projecto__Final.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto__Final.Inventarios
{
    internal class Inventario
    {
        private List<ObjetoInventario> objetos;

        public Inventario()
        {
            Objetos = new List<ObjetoInventario>();
        }

        public List<ObjetoInventario> Objetos { get => objetos; set => objetos = value; }

        public void AgregarObjeto(string nombre, string tipo, int cantidad = 1)
        {
            ObjetoInventario objetoExistente = objetos.Find(objeto => objeto.Nombre == nombre && objeto.Tipo == tipo);

            if (objetoExistente == null) {
                Objetos.Add(new ObjetoInventario(nombre, tipo, cantidad));
            } else {
                objetoExistente.Cantidad += cantidad;
            }
        }

        public bool UsarObjeto(string nombre)
        {
            bool resultado = false;

            ObjetoInventario item = Objetos.Find(item => item.Nombre == nombre);

            if (item != null && item.Cantidad > 0)
            {
                item.Cantidad--;

                if (item.Cantidad <= 0)
                {
                    Objetos.Remove(item);
                }

                resultado = true;
            }

            return resultado;
        }

        public void LimpiarInventario()
        {
            Objetos.Clear();
        }
    }
}

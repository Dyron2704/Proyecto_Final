using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto__Final.Entidades
{
    internal abstract class Entidad
    {
        string nombre;
        int vida;

        public Entidad(string nombre, int vida)
        {
            this.nombre = nombre;
            this.vida = vida;
        }

        public string Nombre { get => nombre; set => nombre = value; }
        public int Vida { get => vida; set => vida = value; }

        public abstract void Atacar();
    }
}

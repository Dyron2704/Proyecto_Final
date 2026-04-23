using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto__Final.Entidades
{
    internal abstract class Entidad
    {
        int vida;
        string nombre;

        public Entidad(int vida, string nombre)
        {
            this.vida = vida;
            this.nombre = nombre;
        }
        public abstract void Atacar(Entidad objetivo);

        public int Vida { get => vida; set => vida = value; }
        public string Nombre { get => nombre; set => nombre = value; }
    }
}

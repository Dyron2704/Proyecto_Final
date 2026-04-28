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
            this.vida = vida;
        }
        public abstract void Atacar(Entidad objetivo);

        public string Nombre { get => nombre; set => nombre = value; }
        public int Vida { get => vida; set => vida = value; }
    }
}

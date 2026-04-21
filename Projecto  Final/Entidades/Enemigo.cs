using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto__Final.Entidades
{
    internal class Enemigo:Entidad
    {
        int cantidadExperiencia;
        int cantidadOro; //cantidad de experiencia y oro que el enemigo da al ser derrotado

        public Enemigo(string nombre, int vida,int cantidadExperiencia, int cantidadOro) : base(nombre, vida)
        {
            this.cantidadExperiencia = cantidadExperiencia;
            this.cantidadOro = cantidadOro;
        }

        public override void Atacar()
        {
            //codigo provisional para atacar, el enemigo no hace nada
        }
        public int CantidadExperiencia { get => cantidadExperiencia; set => cantidadExperiencia = value; }
        public int CantidadOro { get => cantidadOro; set => cantidadOro = value; }

    }
}

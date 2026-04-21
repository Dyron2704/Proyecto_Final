using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto__Final.Entidades
{
    internal class Jugador : Entidad
    {
        int nivel;
        int experiencia;
        int energia;

        public Jugador(string nombre, int salud, int nivel, int experiencia, int energia) : base(nombre, salud)
        {
            this.nivel = nivel;
            this.experiencia = experiencia;
            this.energia = energia;
        }
        public void SubirNivel()
        {
            //codigo provisional para subir de nivel, la exp se queda a 0 y la energia se restaura a 100
            nivel++;
            experiencia = 0;
            energia = 100;
        }
        public override void Atacar()
        {
           
        }
        public int Nivel { get => nivel; set => nivel = value; }
        public int Experiencia { get => experiencia; set => experiencia = value; }
        public int Energia { get => energia; set => energia = value; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto__Final.Entidades
{
    internal class Jugador: Entidad
    {
        int nivel;
        int experiencia;
        int energia;
        public Jugador(int vida, string nombre, int nivel = 0, int experiencia = 0, int energia = 0) : base(vida, nombre)
        {
            this.nivel = nivel;
            this.experiencia = experiencia;
            this.energia = energia;
        }
       
        public override void Atacar(Entidad objetivo)
        {
            // Lógica de ataque del jugador
            
        }
        public void SubirNivel()
        {
            // Lógica para subir de nivel
        }
        public int Nivel { get => nivel; set => nivel = value; }
        public int Experiencia { get => experiencia; set => experiencia = value; }
        public int Energia { get => energia; set => energia = value; }

    }
}

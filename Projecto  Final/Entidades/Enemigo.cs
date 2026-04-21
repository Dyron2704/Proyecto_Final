using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto__Final.Entidades
{
    internal class Enemigo: Entidad
    {
        int nivelDificultad;
        int cantidadOro;
        int experienciaOtorgada;
        public Enemigo(int vida, string nombre, int nivelDificultad = 0, int cantidadOro = 0, int experienciaOtorgada = 0) : base(vida, nombre)
        {
            this.nivelDificultad = nivelDificultad;
            this.cantidadOro = cantidadOro;
            this.experienciaOtorgada = experienciaOtorgada;
        }
        public override void Atacar(Entidad objetivo)
        {
            // Lógica de ataque del enemigo
        }
        public int NivelDificultad { get => nivelDificultad; set => nivelDificultad = value; }
        public int CantidadOro { get => cantidadOro; set => cantidadOro = value; }
        public int ExperienciaOtorgada { get => experienciaOtorgada; set => experienciaOtorgada = value; }
    }
}

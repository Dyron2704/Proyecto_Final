using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Projecto__Final.Entidades
{
    internal class JefeFinal:Enemigo
    {
        bool AtaqueEspecialDisponible;
        bool bonusVidaMaxima;

        public JefeFinal(int vida, string nombre, Texture2D textura, Vector2 posicion, int nivelDificultad = 0, int cantidadOro = 0, int experienciaOtorgada = 0, bool ataqueEspecialDisponible = true, bool bonusVidaMaxima = false)
            : base(vida, nombre, textura, posicion, nivelDificultad, cantidadOro, experienciaOtorgada)  
        {
            this.AtaqueEspecialDisponible = ataqueEspecialDisponible;
            this.bonusVidaMaxima = bonusVidaMaxima;
        }

        

        public override void Atacar(Entidad objetivo)
        {
            if (AtaqueEspecialDisponible)
            {
                Console.WriteLine($" {nombre} Ha usado su ataque especial!");
                objetivo.Vida -= 40;
                AtaqueEspecialDisponible = false;
            }
            else
            {
                objetivo.Vida -= 20;
            }
            if (bonusVidaMaxima)
            {
                game1.AgregarAlerta("Bonus de vida máxima activado! Vida aumentada en 10 puntos.");
                Vida += 10;
                if (Vida > 200) Vida = 200;
            }
        }

        public bool AtaqueEspecialDisponible1 { get => AtaqueEspecialDisponible; set => AtaqueEspecialDisponible = value; }
        public bool BonusVidaMaxima { get => bonusVidaMaxima; set => bonusVidaMaxima = value; }
    }

}

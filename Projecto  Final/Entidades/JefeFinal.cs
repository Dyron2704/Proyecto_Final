using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Projecto__Final.Entidades
{
    internal class JefeFinal : Enemigo
    {
        bool AtaqueEspecialDisponible;
        bool bonusVidaMaxima;
        private Game1 _game;
        private int indexEspecial;
        private int indexVida;

        public JefeFinal(Game1 game, int vida, string nombre, Texture2D textura, Vector2 posicion, int nivelDificultad = 0, int cantidadOro = 0, int experienciaOtorgada = 0, bool ataqueEspecialDisponible = true, bool bonusVidaMaxima = false)
            : base(vida, nombre, textura, posicion, nivelDificultad, cantidadOro, experienciaOtorgada)
        {
            this._game = game;
            this.AtaqueEspecialDisponible = ataqueEspecialDisponible;
            this.bonusVidaMaxima = bonusVidaMaxima;
            this.indexEspecial = 0;
            this.indexVida = 0;
        }

        public override void Atacar(Entidad objetivo)
        {
            if (AtaqueEspecialDisponible)
            {
                _game.AgregarAlerta($" {nombre} Ha usado su ataque especial!");
                objetivo.Vida -= 40; 
                AtaqueEspecialDisponible = false;
                indexEspecial = 0;
            }
            else
            {
                objetivo.Vida -= 20;
                indexEspecial++;
                indexVida++;
                AtaqueEspecialDisponible = indexEspecial > 2;
                bonusVidaMaxima = indexVida > 3;
            }

            if (bonusVidaMaxima)
            {
                _game.AgregarAlerta("Bonus de vida máxima activado! Vida aumentada en 10 puntos.");
                Vida += 40;
                if (Vida > 400) Vida = 400;
                bonusVidaMaxima = false;
                indexVida = 0;
            }
        }

        public bool AtaqueEspecialDisponible1 { get => AtaqueEspecialDisponible; set => AtaqueEspecialDisponible = value; }
        public bool BonusVidaMaxima { get => bonusVidaMaxima; set => bonusVidaMaxima = value; }
    }

}

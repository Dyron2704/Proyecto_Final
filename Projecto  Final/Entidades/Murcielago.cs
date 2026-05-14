using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto__Final.Entidades
{
    internal class Murcielago : Enemigo
    {
        public Murcielago(int vida, string nombre, Texture2D textura, Vector2 posicion, int nivelDificultad, int cantidadOro, int experienciaOtorgada) : base(vida, nombre, textura, posicion, nivelDificultad, cantidadOro, experienciaOtorgada)
        {
        }

        public override void Draw(SpriteBatch spriteBatch, int columnas)
        {
            int ancho = textura.Width;
            int alto = textura.Height / 4;
            Rectangle origen = new Rectangle(columnaActual * ancho, filaActual * alto, ancho, alto);
            spriteBatch.Draw(textura, posicion, origen, Color.White);
        }
    }
}

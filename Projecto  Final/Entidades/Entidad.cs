using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto__Final.Entidades
{
    internal abstract class Entidad
    {
        protected int vida;
        protected string nombre;
        protected Texture2D textura;
        protected Vector2 posicion;
        protected int filaActual = 0;
        protected int columnaActual = 0;
        protected float timer = 0f;

        public Entidad(int vida, string nombre, Texture2D textura, Vector2 posicion)
        {
            this.vida = vida;
            this.nombre = nombre;
            this.textura = textura;
            this.posicion = posicion;
        }

        public abstract void Atacar(Entidad objetivo);

        public Vector2 Posicion { get => posicion; set => posicion = value; }

        public virtual void Draw(SpriteBatch spriteBatch, int columnas)
        {
            int ancho = textura.Width / 8;
            int alto = textura.Height / 4;
            Rectangle origen = new Rectangle(columnaActual * ancho, filaActual * alto, ancho, alto);
            spriteBatch.Draw(textura, posicion, origen, Color.White);
        }
    }
}

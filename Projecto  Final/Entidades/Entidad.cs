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
        private Vector2 posicion;
        protected int filaActual;
        protected int columnaActual;
        protected float timer = 0f;

        public Vector2 Posicion { get => posicion; set => posicion = value; }

        public Entidad(int vida, string nombre, Texture2D textura, Vector2 posicion)
        {
            this.vida = vida;
            this.nombre = nombre;
            this.textura = textura;
            this.posicion = posicion;
        }

        public abstract void Atacar(Entidad objetivo);

        protected void Animar(GameTime gameTime, int inicio, int fin)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer > 0.12f)
            {
                columnaActual++;
                if (columnaActual > fin) columnaActual = inicio;
                timer = 0f;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, int totalColumnas)
        {
            int anchoFrame = textura.Width / totalColumnas;
            int altoFrame = textura.Height / 4;

            Rectangle origen = new Rectangle(columnaActual * anchoFrame, filaActual * altoFrame, anchoFrame, altoFrame);
            spriteBatch.Draw(textura, posicion, origen, Color.White);
        }
    }
}

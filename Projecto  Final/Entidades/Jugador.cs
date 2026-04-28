using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Projecto__Final.Entidades
{
    internal class Jugador : Entidad
    {
        int nivel;
        float velocidad = 2.5f;
        int numColumnas;

        public Jugador(Texture2D textura, Vector2 posicionInicial, int vida, string nombre, int columnas)
            : base(vida, nombre, textura, posicionInicial)
        {
            numColumnas = 8;
            nivel = 1;
        }

        public void Update(GameTime gameTime, Texture2D mapaColisiones)
        {
            KeyboardState teclado = Keyboard.GetState();
            Vector2 direccion = Vector2.Zero;
            bool moviendose = false;

            if (teclado.IsKeyDown(Keys.W))
            {
                direccion.Y = -1;
                filaActual = 3; moviendose = true;
            }

            else if (teclado.IsKeyDown(Keys.S))
                direccion.Y = 1; filaActual = 0; moviendose = true;

            if (teclado.IsKeyDown(Keys.A))
            {
                direccion.X = -1;
                filaActual = 1; moviendose = true;
            }

            else if (teclado.IsKeyDown(Keys.D))
            {
                direccion.X = 1;
                filaActual = 2; moviendose = true;
            }

            Vector2 nuevaPosicion = Posicion + (direccion * velocidad);

            if (EsPosicionValida(nuevaPosicion, mapaColisiones))
                Posicion = nuevaPosicion;

            if (moviendose)
                Animar(gameTime, 0, 3);
            else
                columnaActual = 0;
        }

        private bool EsPosicionValida(Vector2 proximaPos, Texture2D mapaColisiones)
        {
            int anchoF = textura.Width / numColumnas;
            int altoF = textura.Height / 4;

            int posX = (int)proximaPos.X + (anchoF / 2);
            int posY = (int)proximaPos.Y + altoF;

            bool resultado = false;
            if (posX < 0 || posX >= mapaColisiones.Width || posY < 0 || posY >= mapaColisiones.Height)
                resultado = false;

            Color[] colorExtraido = new Color[1];
            mapaColisiones.GetData(0, new Rectangle(posX, posY, 1, 1), colorExtraido, 0, 1);

            resultado = colorExtraido[0] == Color.White;

            return resultado;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch, numColumnas);
        }

        public override void Atacar(Entidad objetivo)
        {
            throw new NotImplementedException();
        }
    }
}

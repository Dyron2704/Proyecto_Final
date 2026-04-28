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
        float velocidad = 2.5f;
        int columnas;
        public Jugador(Texture2D textura, Vector2 pos, int vida, string nombre, int columnas)
            : base(vida, nombre, textura, pos)
        {
            this.columnas = 8;
        }

        public void Update(GameTime gameTime, Texture2D mapaColisiones)
        {
            KeyboardState teclado = Keyboard.GetState();
            Vector2 direccion = Vector2.Zero;
            bool moviendose = false;

            if (teclado.IsKeyDown(Keys.W)) { direccion.Y = -1; filaActual = 3; moviendose = true; }
            if (teclado.IsKeyDown(Keys.S)) { direccion.Y = 1; filaActual = 0; moviendose = true; }
            if (teclado.IsKeyDown(Keys.A)) { direccion.X = -1; filaActual = 1; moviendose = true; }
            if (teclado.IsKeyDown(Keys.D)) { direccion.X = 1; filaActual = 2; moviendose = true; }

            if (moviendose)
            {
                Vector2 nuevaPos = posicion + (direccion * velocidad);
                if (EsPosicionValida(nuevaPos, mapaColisiones))
                {
                    posicion = nuevaPos;
                }

                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timer > 0.12f)
                {
                    columnaActual = (columnaActual + 1) % 4; // Ciclo de 4 frames
                    timer = 0;
                }
            }
            else { columnaActual = 0; }
        }

        private bool EsPosicionValida(Vector2 proximaPos, Texture2D col)
        {
            int x = (int)proximaPos.X + (textura.Width / columnas / 2);
            int y = (int)proximaPos.Y + (textura.Height / 4);

            if (x < 0 || x >= col.Width || y < 0 || y >= col.Height) return false;

            Color[] pixel = new Color[1];
            col.GetData(0, new Rectangle(x, y, 1, 1), pixel, 0, 1);
            return pixel[0] == Color.White;
        }

        public override void Atacar(Entidad objetivo) { }

        public void Draw(SpriteBatch sb) { base.Draw(sb, columnas); }
    }
}

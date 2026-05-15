using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto__Final.Menús
{
    internal class MenuGuardado
    {
        Texture2D fondo;
        Texture2D texturaBoton;
        Texture2D texturaBotonHover;
        SpriteFont fuente;

        List<Rectangle> botonesSlots;
        List<string> nombresPerfiles;
        string rutaPerfiles = "Saves/perfiles.txt";

        public MenuGuardado(Texture2D fondo, Texture2D texturaBoton, Texture2D texturaBotonHover, SpriteFont fuente)
        {
            this.fondo = fondo;
            this.texturaBoton = texturaBoton;
            this.texturaBotonHover = texturaBotonHover;
            this.fuente = fuente;
            this.nombresPerfiles = new List<string>();
            this.botonesSlots = new List<Rectangle>();

            CargarNombresDesdeFichero();
        }

        public void CargarNombresDesdeFichero()
        {
            nombresPerfiles.Clear();
            botonesSlots.Clear();

            if (File.Exists(rutaPerfiles))
            {
                string[] lineas = File.ReadAllLines(rutaPerfiles);

                foreach (string linea in lineas)
                {
                    if (!string.IsNullOrWhiteSpace(linea))
                        nombresPerfiles.Add(linea.Trim());
                }
            }

            for (int i = 0; i < nombresPerfiles.Count; i++)
            {
                botonesSlots.Add(new Rectangle(540, 150 + (i * 80), 200, 50));
            }
        }

        public void Update(MouseState mouse, MouseState mouseAnterior, Game1 game, ref Game1.GameState estadoActual)
        {
            for (int i = 0; i < botonesSlots.Count; i++)
            {
                if (botonesSlots[i].Contains(mouse.Position))
                {
                    if (mouse.LeftButton == ButtonState.Pressed && mouseAnterior.LeftButton == ButtonState.Released)
                    {
                        game.GuardarJSON(nombresPerfiles[i]);
                        estadoActual = Game1.GameState.Jugando;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, MouseState mouse)
        {
            spriteBatch.Draw(fondo, Vector2.Zero, Color.White);

            for (int i = 0; i < botonesSlots.Count; i++)
            {
                Texture2D textura = botonesSlots[i].Contains(mouse.Position) ? texturaBotonHover : texturaBoton;
                spriteBatch.Draw(textura, botonesSlots[i], Color.White);

                string texto = $"Guardar: {nombresPerfiles[i]}";
                Vector2 tam = fuente.MeasureString(texto);
                spriteBatch.DrawString(fuente, texto, new Vector2(botonesSlots[i].Center.X - tam.X / 2, botonesSlots[i].Center.Y - tam.Y / 2), Color.Black);
            }
        }
    }
}

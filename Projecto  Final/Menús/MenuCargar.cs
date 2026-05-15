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
    internal class MenuCargar
    {
        Texture2D fondo;
        Texture2D texturaBoton;
        Texture2D texturaBotonHover;
        SpriteFont fuente;

        List<Rectangle> botonesSlots;
        List<string> nombresPerfiles;
        string rutaPerfiles = "perfiles.txt";

        public MenuCargar(Texture2D fondo, Texture2D texturaBoton, Texture2D texturaBotonHover, SpriteFont fuente)
        {
            this.fondo = fondo;
            this.texturaBoton = texturaBoton;
            this.texturaBotonHover = texturaBotonHover;
            this.fuente = fuente;
            this.nombresPerfiles = new List<string>();
            this.botonesSlots = new List<Rectangle>();

            ActualizarListaPerfiles();
        }

        public void ActualizarListaPerfiles()
        {
            nombresPerfiles.Clear();
            botonesSlots.Clear();

            if (File.Exists(rutaPerfiles))
            {
                string[] lineas = File.ReadAllLines(rutaPerfiles);
                int indice = 0;
                foreach (string linea in lineas)
                {
                    string nombre = linea.Trim();
                    if (!string.IsNullOrWhiteSpace(nombre))
                    {
                        nombresPerfiles.Add(nombre);
                        botonesSlots.Add(new Rectangle(540, 150 + (indice * 80), 200, 50));
                        indice++;
                    }
                }
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
                        string rutaJson = nombresPerfiles[i] + ".json";
                        if (File.Exists(rutaJson))
                        {
                            game.CargarPartida(nombresPerfiles[i]);
                        }
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, MouseState mouse)
        {
            spriteBatch.Draw(fondo, Vector2.Zero, Color.White);

            if (nombresPerfiles.Count == 0)
            {
                spriteBatch.DrawString(fuente, "No hay perfiles creados en perfiles.txt", new Vector2(450, 300), Color.Red);
            }

            for (int i = 0; i < botonesSlots.Count; i++)
            {
                bool existeArchivo = File.Exists(nombresPerfiles[i] + ".json");
                Color colorBoton = existeArchivo ? Color.White : Color.Gray * 0.6f;

                Texture2D textura = botonesSlots[i].Contains(mouse.Position) ? texturaBotonHover : texturaBoton;
                spriteBatch.Draw(textura, botonesSlots[i], colorBoton);

                string texto = existeArchivo ? $"Cargar {nombresPerfiles[i]}" : $"{nombresPerfiles[i]} (Sin datos)";
                Vector2 tam = fuente.MeasureString(texto);
                spriteBatch.DrawString(fuente, texto, new Vector2(botonesSlots[i].Center.X - tam.X / 2, botonesSlots[i].Center.Y - tam.Y / 2), Color.Black);
            }
        }
    }
}

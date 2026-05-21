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
    public class MenuCargar
    {
        Texture2D fondo;
        Texture2D texturaBoton;
        Texture2D texturaBotonHover;
        SpriteFont fuente;

        List<Rectangle> botonesSlots;
        List<string> nombresPerfiles;
        Rectangle botonVolver;

        string rutaPerfiles = "Saves/perfiles.txt";

        public MenuCargar(Texture2D fondo, Texture2D texturaBoton, Texture2D texturaBotonHover, SpriteFont fuente)
        {
            this.fondo = fondo;
            this.texturaBoton = texturaBoton;
            this.texturaBotonHover = texturaBotonHover;
            this.fuente = fuente;
            this.nombresPerfiles = new List<string>();
            this.botonesSlots = new List<Rectangle>();

            this.botonVolver = new Rectangle(540, 580, 200, 50);

            ActualizarListaPerfiles();
        }

        public void ActualizarListaPerfiles()
        {
            nombresPerfiles.Clear();
            botonesSlots.Clear();

            string directorio = Path.GetDirectoryName(rutaPerfiles);
            if (!string.IsNullOrEmpty(directorio) && !Directory.Exists(directorio))
            {
                Directory.CreateDirectory(directorio);
            }

            if (!File.Exists(rutaPerfiles))
            {
                File.WriteAllLines(rutaPerfiles, new string[] { "Vacio", "Vacio", "Vacio" });
            }

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
                        string rutaJson = Path.Combine("Saves", nombresPerfiles[i] + ".json");
                        if (File.Exists(rutaJson))
                        {
                            game.CargarPartida(nombresPerfiles[i]);
                        }
                    }
                }
            }

            if (botonVolver.Contains(mouse.Position))
            {
                if (mouse.LeftButton == ButtonState.Pressed && mouseAnterior.LeftButton == ButtonState.Released)
                {
                    estadoActual = Game1.GameState.SeleccionPartida;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, MouseState mouse)
        {
            spriteBatch.Draw(fondo, new Rectangle(0, 0, 1280, 720), Color.White);

            string titulo = "CARGAR PARTIDA";
            spriteBatch.DrawString(fuente, titulo, new Vector2(640 - fuente.MeasureString(titulo).X / 2, 50), Color.Yellow);

            if (nombresPerfiles.Count == 0)
            {
                spriteBatch.DrawString(fuente, "No hay perfiles en perfiles.txt", new Vector2(450, 300), Color.Red);
            }

            for (int i = 0; i < botonesSlots.Count; i++)
            {
                bool existeArchivo = File.Exists(Path.Combine("Saves", nombresPerfiles[i] + ".json"));
                
                Color colorBoton = existeArchivo ? Color.White : Color.Gray * 0.6f;

                Texture2D texActual = botonesSlots[i].Contains(mouse.Position) ? texturaBotonHover : texturaBoton;
                spriteBatch.Draw(texActual, botonesSlots[i], colorBoton);

                string texto = existeArchivo ? $"Cargar {nombresPerfiles[i]}" : $"{nombresPerfiles[i]} (Vacio)";
                Vector2 tam = fuente.MeasureString(texto);
                Vector2 posTexto = new Vector2(
                    botonesSlots[i].X + (botonesSlots[i].Width / 2) - (tam.X / 2),
                    botonesSlots[i].Y + (botonesSlots[i].Height / 2) - (tam.Y / 2)
                );
                spriteBatch.DrawString(fuente, texto, posTexto, Color.Black);
            }

            Texture2D texVolver = botonVolver.Contains(mouse.Position) ? texturaBotonHover : texturaBoton;
            spriteBatch.Draw(texVolver, botonVolver, Color.White);

            string txtVolver = "VOLVER";
            Vector2 tamVolver = fuente.MeasureString(txtVolver);
            Vector2 posTxtVolver = new Vector2(
                botonVolver.X + (botonVolver.Width / 2) - (tamVolver.X / 2),
                botonVolver.Y + (botonVolver.Height / 2) - (tamVolver.Y / 2)
            );

            spriteBatch.DrawString(fuente, txtVolver, posTxtVolver, Color.Black);
        }
    }
}

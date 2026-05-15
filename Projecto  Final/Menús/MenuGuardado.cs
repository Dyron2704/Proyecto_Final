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
    public class MenuGuardado
    {
        Texture2D fondo;
        Texture2D texturaBoton;
        Texture2D texturaBotonHover;
        SpriteFont fuente;

        List<Rectangle> botonesSlots;
        List<string> nombresPerfiles;

        Rectangle botonGuardar;
        Rectangle botonAtras;
        int slotSeleccionado = -1;

        string rutaPerfiles = "Saves/perfiles.txt";
        string nombreEscrito = "";
        KeyboardState tecladoAnterior;

        public MenuGuardado(Texture2D fondo, Texture2D texturaBoton, Texture2D texturaBotonHover, SpriteFont fuente)
        {
            this.fondo = fondo;
            this.texturaBoton = texturaBoton;
            this.texturaBotonHover = texturaBotonHover;
            this.fuente = fuente;
            this.nombresPerfiles = new List<string>();
            this.botonesSlots = new List<Rectangle>();

            this.botonGuardar = new Rectangle(440, 550, 180, 50);
            this.botonAtras = new Rectangle(660, 550, 180, 50);

            CargarNombresDesdeFichero();
        }

        public void CargarNombresDesdeFichero()
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

            string[] lineas = File.ReadAllLines(rutaPerfiles);
            for (int i = 0; i < lineas.Length; i++)
            {
                nombresPerfiles.Add(lineas[i].Trim());
                botonesSlots.Add(new Rectangle(515, 200 + (i * 75), 250, 50));
            }
        }

        public void Update(MouseState mouse, MouseState mouseAnterior, Game1 game, ref Game1.GameState estadoActual)
        {
            KeyboardState teclado = Keyboard.GetState();
            bool shiftPresionado = teclado.IsKeyDown(Keys.LeftShift) || teclado.IsKeyDown(Keys.RightShift);

            foreach (Keys key in teclado.GetPressedKeys())
            {
                if (tecladoAnterior.IsKeyUp(key))
                {
                    if (key == Keys.Back && nombreEscrito.Length > 0)
                        nombreEscrito = nombreEscrito.Substring(0, nombreEscrito.Length - 1);
                    else if (key >= Keys.A && key <= Keys.Z)
                        nombreEscrito += shiftPresionado ? key.ToString().ToUpper() : key.ToString().ToLower();
                    else if (key == Keys.Space)
                        nombreEscrito += " ";
                }
            }

            for (int i = 0; i < botonesSlots.Count; i++)
            {
                if (botonesSlots[i].Contains(mouse.Position) && mouse.LeftButton == ButtonState.Pressed && mouseAnterior.LeftButton == ButtonState.Released)
                {
                    slotSeleccionado = i;
                }
            }

            if (botonGuardar.Contains(mouse.Position) && mouse.LeftButton == ButtonState.Pressed && mouseAnterior.LeftButton == ButtonState.Released)
            {
                if (slotSeleccionado != -1 && !string.IsNullOrWhiteSpace(nombreEscrito))
                {
                    game.GuardarJSON(nombreEscrito);

                    nombresPerfiles[slotSeleccionado] = nombreEscrito;

                    try
                    {
                        File.WriteAllLines(rutaPerfiles, nombresPerfiles);
                    }
                    catch (Exception ex)
                    {
                        game.AgregarAlerta("Error al actualizar perfiles.txt");
                    }

                    estadoActual = Game1.GameState.Jugando;
                }
            }

            if (botonAtras.Contains(mouse.Position) && mouse.LeftButton == ButtonState.Pressed && mouseAnterior.LeftButton == ButtonState.Released)
            {
                estadoActual = Game1.GameState.MenuEscape;
            }

            tecladoAnterior = teclado;
        }

        public void Draw(SpriteBatch spriteBatch, MouseState mouse)
        {
            spriteBatch.Draw(fondo, new Rectangle(0, 0, 1280, 720), Color.White);

            spriteBatch.DrawString(fuente, "1. ESCRIBE NOMBRE: " + nombreEscrito + "_", new Vector2(400, 50), Color.Yellow);
            spriteBatch.DrawString(fuente, "2. ELIGE UN HUECO (SLOT):", new Vector2(400, 150), Color.White);

            for (int i = 0; i < botonesSlots.Count; i++)
            {
                Color colorBoton = (slotSeleccionado == i) ? Color.Cyan : Color.White;
                Texture2D tex = botonesSlots[i].Contains(mouse.Position) ? texturaBotonHover : texturaBoton;

                spriteBatch.Draw(tex, botonesSlots[i], colorBoton);

                string txt = nombresPerfiles[i];
                Vector2 centroTexto = fuente.MeasureString(txt) / 2;
                spriteBatch.DrawString(fuente, txt, new Vector2(botonesSlots[i].Center.X - centroTexto.X, botonesSlots[i].Center.Y - centroTexto.Y), Color.Black);
            }

            spriteBatch.Draw(texturaBoton, botonGuardar, botonGuardar.Contains(mouse.Position) ? Color.Green : Color.White);
            spriteBatch.DrawString(fuente, "GUARDAR", new Vector2(botonGuardar.X + 40, botonGuardar.Y + 10), Color.Black);

            spriteBatch.Draw(texturaBoton, botonAtras, botonAtras.Contains(mouse.Position) ? Color.Red : Color.White);
            spriteBatch.DrawString(fuente, "VOLVER", new Vector2(botonAtras.X + 50, botonAtras.Y + 10), Color.Black);
        }
    }
}

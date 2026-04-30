using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto__Final.Menús
{
    internal class MenuEscape
    {
        List<Boton> _botones;
        Texture2D _texturaFondoOcupado;
        SpriteFont _fuente;

        public MenuEscape(GraphicsDevice graphics, Texture2D texBoton, Texture2D texBotonHover, SpriteFont fuente)
        {
            _fuente = fuente;
            _botones = new List<Boton>();

            _texturaFondoOcupado = new Texture2D(graphics, 1, 1);
            _texturaFondoOcupado.SetData(new[] { Color.White });

            int anchoBoton = 256;
            int posX = 640 - (anchoBoton / 2);

            _botones.Add(new Boton(texBoton, texBotonHover, fuente, new Vector2(posX, 200), "Continuar"));
            _botones.Add(new Boton(texBoton, texBotonHover, fuente, new Vector2(posX, 300), "Cambiar Personaje"));
            _botones.Add(new Boton(texBoton, texBotonHover, fuente, new Vector2(posX, 400), "Guardar Partida"));
            _botones.Add(new Boton(texBoton, texBotonHover, fuente, new Vector2(posX, 500), "Menu Principal"));
        }

        public void Update(MouseState mouse, MouseState mouseAnterior, ref Game1.GameState estadoGlobal)
        {
            foreach (Boton boton in _botones)
            {
                boton.Update(mouse);

                if (boton.Clicado(mouse, mouseAnterior))
                {
                    switch (boton.Texto)
                    {
                        case "Continuar":
                            estadoGlobal = Game1.GameState.Jugando;
                            break;
                        case "Cambiar Personaje":
                            estadoGlobal = Game1.GameState.MenuPersonajes;
                            break;
                        case "Guardar Partida":
                            // Aquí irá la lógica del guardado de la partida
                            break;
                        case "Menu Principal":
                            estadoGlobal = Game1.GameState.MenuPrincipal;
                            break;
                    }
                }
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texturaFondoOcupado, new Rectangle(0, 0, 1280, 720), new Color(0, 0, 0, 150));

            string titulo = "PAUSA";
            Vector2 tamTitulo = _fuente.MeasureString(titulo);
            spriteBatch.DrawString(_fuente, titulo, new Vector2(640 - tamTitulo.X / 2, 120), Color.White);

            foreach (Boton boton in _botones)
            {
                boton.Draw(spriteBatch);
            }
        }
    }
}

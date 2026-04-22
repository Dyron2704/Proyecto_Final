using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto__Final
{
    internal class MenuSeleccion
    {
        List<Boton> _botones;
        Texture2D _fondoNormal;

        public MenuSeleccion(Texture2D fondoNormal, Texture2D textureBoton, SpriteFont fuente, GraphicsDevice graphics)
        {
            _fondoNormal = fondoNormal;
            _botones = new List<Boton>();

            int xCentrada = (graphics.Viewport.Width / 2) - (200 / 2);

            _botones.Add(new Boton(textureBoton, fuente, new Vector2(xCentrada, 250), "Crear Partida"));
            _botones.Add(new Boton(textureBoton, fuente, new Vector2(xCentrada, 350), "Cargar Partida"));
            _botones.Add(new Boton(textureBoton, fuente, new Vector2(xCentrada, 450), "Volver"));
        }

        public void Upadate(MouseState mouse, ref Game1.GameState estadoGlobal)
        {
            foreach (Boton boton in _botones)
            {
                boton.Update(mouse);
                if (boton.Clicado(mouse))
                {
                    if (boton.Texto == "Crear Partida") estadoGlobal = Game1.GameState.Jugando;
                    if (boton.Texto == "Volver") estadoGlobal = Game1.GameState.MenuPrincipal;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_fondoNormal, new Rectangle(0, 0, 1280, 720), Color.CornflowerBlue);

            foreach (Boton boton in _botones)
            {
                boton.Draw(spriteBatch);
            }
        }
    }
}

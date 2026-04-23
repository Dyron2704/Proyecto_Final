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
    internal class MenuPersonaje
    {
        List<Boton> _botones;
        List<LineaDeTexto> _lineasDeTexto;
        Texture2D _fondoNormal;

        public MenuPersonaje(Texture2D fondoNormal, Texture2D textureBoton, SpriteFont fuente, GraphicsDevice graphics)
        {
            _fondoNormal = fondoNormal;
            _botones = new List<Boton>();
            _lineasDeTexto = new List<LineaDeTexto>();

            int xCentrada = (graphics.Viewport.Width / 2) - (200 / 2);

            _lineasDeTexto.Add(new LineaDeTexto(textureBoton, fuente, new Microsoft.Xna.Framework.Vector2(xCentrada, 150), Color.White, Color.Black, "Selecciona tu personaje"));
            _botones.Add(new Boton(textureBoton, fuente, new Microsoft.Xna.Framework.Vector2(xCentrada, 250), "Tipo 1"));
            _botones.Add(new Boton(textureBoton, fuente, new Microsoft.Xna.Framework.Vector2(xCentrada, 350), "Tipo 2"));
            _botones.Add(new Boton(textureBoton, fuente, new Microsoft.Xna.Framework.Vector2(xCentrada, 450), "Tipo 3"));
            _botones.Add(new Boton(textureBoton, fuente, new Microsoft.Xna.Framework.Vector2(xCentrada, 550), "Volver"));
        }

        public void Update(MouseState mouse, ref Game1.GameState estadoGlobal)
        {
            foreach (Boton boton in _botones)
            {
                boton.Update(mouse);
                if (boton.Clicado(mouse))
                {
                    if (boton.Texto == "Volver") estadoGlobal = Game1.GameState.SeleccionPartida;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_fondoNormal, new Rectangle(0, 0, 1280, 720), Color.DarkGray);

            foreach (Boton boton in _botones)
            {
                boton.Draw(spriteBatch);
            }
            foreach (LineaDeTexto linea in _lineasDeTexto)
            {
                linea.Draw(spriteBatch);
            }
        }
    }
}

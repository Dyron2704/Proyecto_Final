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
    internal class MenuPersonajes
    {
        List<Boton> _botones;
        List<LineaDeTexto> _lineasDeTexto;
        Texture2D _texturaNormal;
        Texture2D _texturaHover;

        public MenuPersonajes(Texture2D normal, Texture2D texBoton, Texture2D texBotonHover, SpriteFont fuente)
        {
            _texturaNormal = normal;
            _texturaHover = texBoton;
            _botones = new List<Boton>();
            _lineasDeTexto = new List<LineaDeTexto>();

            int ancho = 256;
            int posXCentrada = 640 - (ancho / 2);

            _lineasDeTexto.Add(new LineaDeTexto(texBoton, fuente, new Microsoft.Xna.Framework.Vector2(posXCentrada+8, 150), Color.White, Color.Black, "Selecciona tu personaje"));
            _botones.Add(new Boton(texBoton, texBotonHover, fuente, new Microsoft.Xna.Framework.Vector2(posXCentrada, 250), "Tipo 1"));
            _botones.Add(new Boton(texBoton, texBotonHover, fuente, new Microsoft.Xna.Framework.Vector2(posXCentrada, 350), "Tipo 2"));
            _botones.Add(new Boton(texBoton, texBotonHover, fuente, new Microsoft.Xna.Framework.Vector2(posXCentrada, 450), "Tipo 3"));
            _botones.Add(new Boton(texBoton, texBotonHover, fuente, new Microsoft.Xna.Framework.Vector2(posXCentrada, 550), "Volver"));
        }

        public void Update(MouseState mouse, MouseState mouseAnterior, ref Game1.GameState estadoGlobal)
        {
            foreach (Boton boton in _botones)
            {
                boton.Update(mouse);
                if (boton.Clicado(mouse, mouseAnterior))
                {
                    if (boton.Texto == "Volver") estadoGlobal = Game1.GameState.SeleccionPartida;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texturaNormal, new Rectangle(0, 0, 1280, 720), Color.DarkGray);

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

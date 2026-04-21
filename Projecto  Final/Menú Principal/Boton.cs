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
    internal class Boton
    {
        Texture2D _textura;
        Vector2 _posicion;
        Rectangle _rectangulo;
        Color _colorActual;

        public bool mouseEncima;
        public string texto;

        public bool MouseEncima { get => mouseEncima; set => mouseEncima = value; }
        public string Texto { get => texto; set => texto = value; }

        public Boton(Texture2D textura, Vector2 posicion, string texto)
        {
            _textura = textura;
            _posicion = posicion;
            Texto = texto;
            _rectangulo = new Rectangle((int)posicion.X, (int)posicion.Y, textura.Width, textura.Height);
        }

        public void Update(MouseState mouse)
        {
            if (_rectangulo.Contains(mouse.X, mouse.Y))
            {
                MouseEncima = true;
                _colorActual = Color.Gray;
            }
            else
            {
                MouseEncima = false;
                _colorActual = Color.White;
            }
        }

        public bool Clicado(MouseState mouse)
        {
            return MouseEncima && mouse.LeftButton == ButtonState.Pressed;
        }

        public void Draw(SpriteBatch spriteBach)
        {
            spriteBach.Draw(_textura, _posicion, _colorActual);
        }
    }
}

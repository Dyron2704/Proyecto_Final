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
        Texture2D _texturaNormal;
        Texture2D _texturaHover;
        Rectangle _rectangulo;
        Color _colorActual;

        SpriteFont _fuente;

        public bool mouseEncima;
        public string texto;

        public bool MouseEncima { get => mouseEncima; set => mouseEncima = value; }
        public string Texto { get => texto; set => texto = value; }

        public Boton(Texture2D normal, Texture2D hover, SpriteFont fuente, Vector2 posicion, string texto)
        {
            _texturaNormal = normal;
            _texturaHover = hover;
            _fuente = fuente;
            Texto = texto;

            _rectangulo = new Rectangle((int)posicion.X, (int)posicion.Y, 256, 64);
        }

        public void Update(MouseState mouse)
        {
            MouseEncima = _rectangulo.Contains(mouse.X, mouse.Y);
        }

        public bool Clicado(MouseState mouseActual, MouseState mouseAnterior)
        {
            return MouseEncima && mouseActual.LeftButton == ButtonState.Pressed &&
                mouseAnterior.LeftButton == ButtonState.Released;
        }

        public void Draw(SpriteBatch spriteBach)
        {
            Texture2D texturaADibujar = MouseEncima ? _texturaHover : _texturaNormal;

            spriteBach.Draw(texturaADibujar, _rectangulo, Color.White);

            if (_fuente != null)
            {
                Vector2 tamañoTexto = _fuente.MeasureString(Texto);
                Vector2 centroTexto = new Vector2(
                    _rectangulo.X + (_rectangulo.Width / 2) - (tamañoTexto.X / 2),
                    _rectangulo.Y + (_rectangulo.Height / 2) - (tamañoTexto.Y / 2)
                );
                spriteBach.DrawString(_fuente, Texto, centroTexto, Color.Black);
            }
        }
    }
}

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

        SpriteFont _fuente;

        public bool mouseEncima;
        public string texto;

        public bool MouseEncima { get => mouseEncima; set => mouseEncima = value; }
        public string Texto { get => texto; set => texto = value; }

        public Boton(Texture2D textura, SpriteFont fuente, Vector2 posicion, string texto)
        {
            _textura = textura;
            _posicion = posicion;
            Texto = texto;
            _fuente = fuente;
            //_rectangulo = new Rectangle((int)posicion.X, (int)posicion.Y, textura.Width, textura.Height); Para cuando tengamos las texturas definitivas, ahora lo dejo con un tamaño fijo para probar la lógica
            _rectangulo = new Rectangle((int)posicion.X, (int)posicion.Y, 200, 50);
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
            Color colorBoton = MouseEncima ? Color.Green : Color.Red;

            spriteBach.Draw(_textura, _rectangulo, colorBoton);

            Vector2 tamañoTexto = _fuente.MeasureString(Texto);
            Vector2 centroTexto = new Vector2(
                _rectangulo.X + (_rectangulo.Width / 2) - (tamañoTexto.X / 2),
                _rectangulo.Y + (_rectangulo.Height / 2) - (tamañoTexto.Y / 2)
            );

            spriteBach.DrawString(_fuente, Texto, centroTexto, Color.Black);
        }
    }
}

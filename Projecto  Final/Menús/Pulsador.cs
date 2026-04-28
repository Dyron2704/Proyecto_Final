using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Projecto__Final
{
    internal class Pulsador
    {
        //Tener cuidado con el texto que se pone ya que no lee todos los caracteres: no poner tildes.
        Texture2D _texturaNormal;
        Texture2D _texturaHover;
        Rectangle _rectangulo;
        Rectangle? _sourceRect;
        SpriteFont _fuente;

        public string textoNormal;
        public string textoPulsado;
        public bool MouseEncima;
        public bool pulsado;


        public Pulsador(Texture2D normal, Texture2D hover, SpriteFont fuente, Vector2 posicion, string textoNormal, string textoPulsado)
        {
            pulsado = false;
            _texturaNormal = normal;
            _texturaHover = hover;
            _fuente = fuente;
            this.textoNormal= textoNormal;
            this.textoPulsado= textoPulsado;
            _rectangulo = new Rectangle((int)posicion.X, (int)posicion.Y, 256, 64);
        }

        public void Update(MouseState mouse)
        {
            MouseEncima = _rectangulo.Contains(mouse.X, mouse.Y);
        }

        public void Clicado(MouseState mouseActual, MouseState mouseAnterior)
        {
            if (MouseEncima &&
                   mouseActual.LeftButton == ButtonState.Pressed &&
                   mouseAnterior.LeftButton == ButtonState.Released)
            {
                if (pulsado)
                {
                    pulsado = false;
                }
                else
                {
                    pulsado = true;
                }
            }
        }

        public void Draw(SpriteBatch sb)
        {
            Texture2D texturaADibujar = pulsado ? _texturaHover : _texturaNormal;
            if (texturaADibujar != null)
            {
                sb.Draw(texturaADibujar, _rectangulo, Color.White);
            }

            if (pulsado)
            {
                if (_fuente != null && !string.IsNullOrEmpty(textoPulsado))
                {
                    Vector2 tam = _fuente.MeasureString(textoPulsado);

                    Vector2 posTexto = new Vector2(
                        _rectangulo.X + (_rectangulo.Width / 2) - (tam.X / 2),
                        _rectangulo.Y + (_rectangulo.Height / 2) - (tam.Y / 2) - 4
                    );

                    sb.DrawString(_fuente, textoPulsado, posTexto, Color.Black);
                }
            }
            else
            {
                if (_fuente != null && !string.IsNullOrEmpty(textoNormal))
                {
                    Vector2 tam = _fuente.MeasureString(textoNormal);

                    Vector2 posTexto = new Vector2(
                        _rectangulo.X + (_rectangulo.Width / 2) - (tam.X / 2),
                        _rectangulo.Y + (_rectangulo.Height / 2) - (tam.Y / 2) - 4
                    );

                    sb.DrawString(_fuente, textoNormal, posTexto, Color.Black);
                }
            }
        }
    }
}

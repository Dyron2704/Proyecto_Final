using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Projecto__Final
{
    internal class Boton
    {
        Texture2D _texturaNormal;
        Texture2D _texturaHover;
        Texture2D _spriteReferencia;
        Rectangle _rectangulo;
        Rectangle? _sourceRect;
        SpriteFont _fuente;

        public string Texto;
        public bool MouseEncima;

        
        public Boton(Texture2D normal, Texture2D hover, SpriteFont fuente, Vector2 posicion, string texto)
        {
            _texturaNormal = normal;
            _texturaHover = hover;
            _fuente = fuente;
            Texto = texto;
            _rectangulo = new Rectangle((int)posicion.X, (int)posicion.Y, 256, 64);
        }

        public Boton(Texture2D normal, Texture2D hover, Texture2D spriteReferencia, Rectangle sourceRect, SpriteFont fuente, Vector2 posicion, string texto)
        {
            _texturaNormal = normal;
            _texturaHover = hover;
            _spriteReferencia = spriteReferencia;
            _sourceRect = sourceRect;
            _fuente = fuente;
            Texto = texto;
            
            _rectangulo = new Rectangle((int)posicion.X, (int)posicion.Y, 200, 120);
        }

        public Boton(Texture2D normal, SpriteFont fuente, Vector2 posicion, string texto, int ancho, int alto)
        {
            _texturaNormal = normal;
            _texturaHover = normal;
            _fuente = fuente;
            Texto = texto;
            _rectangulo = new Rectangle((int)posicion.X, (int)posicion.Y, ancho, alto);
        }

        public void Update(MouseState mouse)
        {
            MouseEncima = _rectangulo.Contains(mouse.X, mouse.Y);
        }

        public bool Clicado(MouseState mouseActual, MouseState mouseAnterior)
        {
            return MouseEncima &&
                   mouseActual.LeftButton == ButtonState.Pressed &&
                   mouseAnterior.LeftButton == ButtonState.Released;
        }

        public void Draw(SpriteBatch sb)
        {
            Texture2D texturaADibujar = MouseEncima ? _texturaHover : _texturaNormal;
            if (texturaADibujar != null)
            {
                sb.Draw(texturaADibujar, _rectangulo, Color.White);
            }

            if (_spriteReferencia != null)
            {
                Rectangle destRect = new Rectangle(_rectangulo.X + 10, _rectangulo.Y + 5, 60, 90);
                sb.Draw(_spriteReferencia, destRect, _sourceRect, Color.White);
            }

            if (_fuente != null && !string.IsNullOrEmpty(Texto))
            {
                Vector2 tam = _fuente.MeasureString(Texto);

                Vector2 posTexto = new Vector2(
                    _rectangulo.X + (_rectangulo.Width / 2) - (tam.X / 2),
                    _rectangulo.Y + (_rectangulo.Height / 2) - (tam.Y / 2) - 4
                );

                sb.DrawString(_fuente, Texto, posTexto, Color.Black);
            }
        }
    }
}
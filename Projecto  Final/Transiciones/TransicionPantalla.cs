using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Projecto__Final.Transiciones
{
    internal class TransicionPantalla
    {
        private float _progreso = 0f;
        public bool EstaActiva { get; private set; }
        public bool DireccionInversa { get; set; }
        private int _alturaPantalla;
        private float _velocidad = 1.5f;


        public TransicionPantalla(int altura)
        {
            _alturaPantalla = altura;
        }

        public void Iniciar(bool direccionInversa)
        {
            _progreso = 0f;
            EstaActiva = true;
            DireccionInversa = direccionInversa;
        }

        public void Update(GameTime gameTime)
        {
            if (!EstaActiva) return;

            _progreso += (float)gameTime.ElapsedGameTime.TotalSeconds * _velocidad;

            if (_progreso >= 1f)
            {
                _progreso = 1f;
                EstaActiva = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texturaVieja, Texture2D texturaNueva)
        {
            float desplazamientoSuave = MathHelper.SmoothStep(0, 1, _progreso);
            int pixelesY = (int)(desplazamientoSuave * _alturaPantalla);

            if(!DireccionInversa)
            {
                spriteBatch.Draw(texturaVieja, new Vector2(0, pixelesY), Color.White);
                spriteBatch.Draw(texturaNueva, new Vector2(0, pixelesY - _alturaPantalla), Color.White);
            }
            else
            {
                spriteBatch.Draw(texturaVieja, new Vector2(0, -pixelesY), Color.White);
                spriteBatch.Draw(texturaNueva, new Vector2(0, _alturaPantalla - pixelesY), Color.White);
            }
        }
    }
}


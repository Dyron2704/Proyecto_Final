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
    internal class MenuOpciones
    {
        List<Pulsador> _pulsadores;
        List<Boton> _botones;
        Texture2D _fondoNormal;
        bool pantallaCompleta;

        public MenuOpciones(Texture2D fondoNormal, Texture2D texBoton, Texture2D texBotonHover, SpriteFont fuente)
        {
            pantallaCompleta = false;
            _fondoNormal = fondoNormal;
            _botones = new List<Boton>();
            _pulsadores = new List<Pulsador>();

            int ancho = 256;
            int posXCentrada = 640 - (ancho / 2);

            _pulsadores.Add(new Pulsador(texBoton, texBotonHover, fuente, new Vector2(posXCentrada, 150), "Musica: SI", "Musica: NO"));
            _botones.Add(new Boton(texBoton, texBotonHover, fuente, new Vector2(posXCentrada, 300), "Ajuste 2"));
            _botones.Add(new Boton(texBoton, texBotonHover, fuente, new Vector2(posXCentrada, 450), "Volver"));
        }

        public void Update(MouseState mouse, MouseState mouseAnterior, ref Game1.GameState estadoGlobal)
        {
            foreach (Boton boton in _botones)
            {
                boton.Update(mouse);

                if (boton.Clicado(mouse, mouseAnterior))
                {
                    if (boton.Texto == "Volver") estadoGlobal = Game1.GameState.MenuPrincipal;
                }
            }

            foreach (Pulsador pulsador in _pulsadores)
            {
                pulsador.Update(mouse);
                pulsador.Clicado(mouse, mouseAnterior);
                if (pulsador.pulsado)
                {

                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_fondoNormal, new Rectangle(0, 0, 1280, 720), Color.White);

            foreach (Boton boton in _botones)
            {
                boton.Draw(spriteBatch);
            }

            foreach (Pulsador pulsador in _pulsadores)
            {
                pulsador.Draw(spriteBatch);
            }
        }
    }
}

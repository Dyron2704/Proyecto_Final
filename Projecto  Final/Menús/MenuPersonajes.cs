using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Projecto__Final.Menús
{
    internal class MenuPersonajes
    {
        List<Boton> _botones;
        Texture2D _fondo;

        public MenuPersonajes(Texture2D fondo, List<Texture2D> fotosPersonajes, List<string> nombres, SpriteFont fuente, Texture2D texBotonVacio)
        {
            _fondo = fondo;
            _botones = new List<Boton>();

            int personajesPorFila = 5;
            int espaciadoX = 220;
            int espaciadoY = 250;
            int inicioX = 100; 
            int inicioY = 100;  

            for (int i = 0; i < fotosPersonajes.Count; i++)
            {
                int fila = i / personajesPorFila;
                int columna = i % personajesPorFila;

                Vector2 pos = new Vector2(inicioX + (columna * espaciadoX), inicioY + (fila * espaciadoY));
                _botones.Add(new Boton(texBotonVacio, texBotonVacio, fotosPersonajes[i], new Rectangle(0, 0, fotosPersonajes[i].Width, fotosPersonajes[i].Height), fuente, pos, nombres[i]));
            }

            _botones.Add(new Boton(texBotonVacio, fuente, new Vector2(50, 620), "Volver", 150, 50));
        }

        public void Update(MouseState mouse, MouseState mouseAnterior, ref Game1.GameState estadoGlobal)
        {
            foreach (var boton in _botones)
            {
                boton.Update(mouse);
                if (boton.Clicado(mouse, mouseAnterior))
                {
                    if (boton.Texto == "Volver")
                        estadoGlobal = Game1.GameState.SeleccionPartida;
                    else
                    {
                        estadoGlobal = Game1.GameState.Jugando;
                    }
                }
            }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(_fondo, new Rectangle(0, 0, 1280, 720), Color.White);
            foreach (var boton in _botones) boton.Draw(sb);
        }
    }
}
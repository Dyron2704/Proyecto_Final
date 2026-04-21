using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Projecto__Final.Game1;

namespace Projecto__Final
{
    internal class MenuPrincipal
    {
        List<Boton> _botones;
        Texture2D _fondoNormal;
        Texture2D _fondoHover;
        bool _mostrarFondoEspecial = false;

        public MenuPrincipal(Texture2D fondoNormal, Texture2D fondoHover, Texture2D textureBoton)
        {
            _fondoNormal = fondoNormal;
            _fondoHover = fondoHover;
            _botones = new List<Boton>();

            _botones.Add(new Boton(textureBoton, new Microsoft.Xna.Framework.Vector2(300, 200), "Jugar"));
            _botones.Add(new Boton(textureBoton, new Microsoft.Xna.Framework.Vector2(300, 300), "Opciones"));
            _botones.Add(new Boton(textureBoton, new Microsoft.Xna.Framework.Vector2(300, 400), "Salir"));
        }

        public void Update(MouseState mouse, ref GameState estadoGlobal)
        {
            _mostrarFondoEspecial = false;

            foreach (Boton boton in _botones)
            {
                boton.Update(mouse);

                if (boton.MouseEncima && boton.Texto == "Jugar")
                    _mostrarFondoEspecial = true;

                if (boton.Clicado(mouse))
                {
                    if (boton.Texto == "Jugar") estadoGlobal = GameState.Jugando;
                    if (boton.Texto == "Salir") /* Lógica para salir */;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Texture2D fondoADibujar = _mostrarFondoEspecial ? _fondoHover : _fondoNormal;
            spriteBatch.Draw(fondoADibujar, new Rectangle(0, 0, 800, 480), Color.White);

            foreach (Boton boton in _botones)
            {
                boton.Draw(spriteBatch);
            }
        }
    }
}

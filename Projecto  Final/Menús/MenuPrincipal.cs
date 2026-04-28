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

        public Texture2D FondoNormal { get => _fondoNormal; set => _fondoNormal = value; }

        public MenuPrincipal(Texture2D fondoNormal, Texture2D fondoHover, Texture2D texBoton, Texture2D texBotonHover, SpriteFont fuente)
        {
            _fondoNormal = fondoNormal;
            _fondoHover = fondoHover;
            _botones = new List<Boton>();

            int ancho = 256;
            int posXCentrada = 640 - (ancho / 2);

            _botones.Add(new Boton(texBoton, texBotonHover, fuente, new Vector2(posXCentrada, 150), "Jugar"));
            _botones.Add(new Boton(texBoton, texBotonHover, fuente, new Vector2(posXCentrada, 300), "Opciones"));
            _botones.Add(new Boton(texBoton, texBotonHover, fuente, new Vector2(posXCentrada, 450), "Salir"));
        }

        public void Update(MouseState mouse, MouseState mouseAnterior, ref GameState estadoGlobal)
        {
            _mostrarFondoEspecial = false;
            foreach (Boton boton in _botones)
            {
                boton.Update(mouse);
                if (boton.MouseEncima && boton.Texto == "Jugar") _mostrarFondoEspecial = true;

                if (boton.Clicado(mouse, mouseAnterior))
                {
                    if (boton.Texto == "Jugar") estadoGlobal = GameState.SeleccionPartida;
                    if (boton.Texto == "Salir") System.Environment.Exit(0);
                    if (boton.Texto == "Opciones") estadoGlobal = GameState.Opciones;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Texture2D fondoADibujar = _mostrarFondoEspecial ? _fondoHover : _fondoNormal;

            Color colorFondo = _mostrarFondoEspecial ? Color.LightBlue : Color.White;

            spriteBatch.Draw(fondoADibujar, new Rectangle(0, 0, 1280, 720), colorFondo);

            foreach (Boton boton in _botones)
            {
                boton.Draw(spriteBatch);
            }
        }
    }
}

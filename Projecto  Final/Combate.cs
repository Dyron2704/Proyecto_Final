using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Projecto__Final.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto__Final
{
    internal class Combate
    {
        Jugador jugador;
        Enemigo enemigo;
        List<Boton> botones;
        SpriteFont fuente;

        Vector2 posicionJugador = new Vector2(250, 350);
        Vector2 posicionEnemigo = new Vector2(850, 350);

        bool esTurnoJugador = true;
        string mensajeAccion = "¡Elige una acción para comenzar!";

        Texture2D fondoCombate;

        public Combate(Texture2D fondoCombate, Jugador jugador, Enemigo enemigo, Texture2D texBoton, Texture2D texBotonHover, SpriteFont fuente)
        {
            this.fondoCombate = fondoCombate;
            this.jugador = jugador;
            this.enemigo = enemigo;
            this.fuente = fuente;

            botones = new List<Boton>();
            int posX = 150;

            botones.Add(new Boton(texBoton, texBotonHover, fuente, new Vector2(posX, 550), "Atacar"));
            botones.Add(new Boton(texBoton, texBotonHover, fuente, new Vector2(posX + 280, 550), "Usar Pocion"));
            botones.Add(new Boton(texBoton, texBotonHover, fuente, new Vector2(posX + 560, 550), "Huir"));
        }

        public void Update(GameTime gameTime, MouseState mouse, MouseState mouseAnterior)
        {
            if (esTurnoJugador)
            {
                foreach (Boton boton in botones)
                {
                    boton.Update(mouse);

                    if (boton.Clicado(mouse, mouseAnterior))
                    {
                        ProcesarAccion(boton.Texto);
                    }
                }
            }

            else
                TurnoEnemigo();
        }

        private void ProcesarAccion(string accion)
        {
            switch (accion)
            {
                case "Atacar":
                    enemigo.Vida -= 20;

                    if (enemigo.Vida < 0) enemigo.Vida = 0;

                    mensajeAccion = "¡Has atacado al enemigo!";

                    esTurnoJugador = false;
                    break;

                case "Usar Pocion":
                    bool exito = jugador.Inventario.UsarObjeto("Pocion de vida");

                    if (exito)
                    {
                        jugador.Vida += 20;

                        if (jugador.Vida > 100) jugador.Vida = 100;

                        mensajeAccion = "¡Has usado una poción de vida!";
                    }

                    else
                    {
                        mensajeAccion = "¡No tienes pociones de vida!";
                    }

                    esTurnoJugador = false;
                    break;

                case "Huir":
                    mensajeAccion = "¡Has huido del combate!";

                    break;
            }
        }

        private void TurnoEnemigo()
        {
            jugador.Vida -= 10;
            if (jugador.Vida < 0) jugador.Vida = 0;
            mensajeAccion = "¡El enemigo te ha atacado!";

            esTurnoJugador = true;
        }

        public void Draw(SpriteBatch sb)
        {
            Vector2 posOriginalJugador = jugador.Posicion;
            Vector2 posOriginalEnemigo = enemigo.Posicion;

            jugador.Posicion = posicionJugador;
            enemigo.Posicion = posicionEnemigo;

            jugador.Draw(sb, fuente);

            if (enemigo.Nombre == "Murcielago" || enemigo.Nombre == "murcielago")
            {
                enemigo.Draw(sb, 1, 4); // Podemos ajustar el tamaño del enemigo para que se vea más grande
            } else if (enemigo.Nombre == "Slime" || enemigo.Nombre == "slime")
            {
                enemigo.Draw(sb, 1, 9); // Ajustamos el tamaño del slime para que se vea más grande
            }
            else if (enemigo.Nombre == "Caballero" || enemigo.Nombre == "caballero")
            {
                enemigo.Draw(sb, 1, 8);
            }

            jugador.Posicion = posOriginalJugador;
            enemigo.Posicion = posOriginalEnemigo;

            sb.DrawString(fuente, $"Jugador HP: {jugador.Vida} / 100", new Vector2(posicionJugador.X - 50, posicionJugador.Y + 120), Color.White);
            sb.DrawString(fuente, $"Enemigo HP: {enemigo.Vida} / 100", new Vector2(posicionEnemigo.X - 50, posicionEnemigo.Y + 120), Color.White);

            sb.DrawString(fuente, mensajeAccion, new Vector2(100, 30), Color.Yellow);

            foreach (Boton b in botones)
            {
                b.Draw(sb);
            }
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Projecto__Final.Entidades;
using Projecto__Final.Menús;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Projecto__Final.Game1;

namespace Projecto__Final
{
    internal class Combate
    {
        Jugador jugador;
        Enemigo enemigo;
        List<Boton> botones;
        SpriteFont fuente;
        int vidaMaximaEnemigo;

        Vector2 posicionJugador = new Vector2(250, 350);
        Vector2 posicionEnemigo = new Vector2(850, 350);

        bool esTurnoJugador = true;
        string mensajeAccion = "¡Elige una acción para comenzar!";

        Texture2D fondoCombate;

        public Combate(Texture2D fondoCombate, Jugador jugador, Enemigo[] enemigos, Texture2D texBoton, Texture2D texBotonHover, SpriteFont fuente)
        {
            this.fondoCombate = fondoCombate;
            this.jugador = jugador;
            this.enemigo = enemigos[new Random().Next(enemigos.Length)];
            this.vidaMaximaEnemigo = this.enemigo.Vida;
            this.fuente = fuente;

            botones = new List<Boton>();
            int posX = 150;

            botones.Add(new Boton(texBoton, texBotonHover, fuente, new Vector2(posX, 550), "Atacar"));
            botones.Add(new Boton(texBoton, texBotonHover, fuente, new Vector2(posX + 280, 550), "Usar Pocion"));
            botones.Add(new Boton(texBoton, texBotonHover, fuente, new Vector2(posX + 560, 550), "Huir"));
        }

        public void Update(GameTime gameTime, MouseState mouse, MouseState mouseAnterior, ref GameState estadoActual)
        {
            if (enemigo.Vida <= 0 && jugador.Vida > 0)
            {
                if (enemigo is JefeFinal)
                {
                    mensajeAccion = "¡Has derrotado al jefe final!";
                    jugador.Puntuacion += 100;
                    estadoActual = GameState.PantallaVictoria;

                }
                else 
                {
                    mensajeAccion = "¡Has derrotado al enemigo!";
                    jugador.Puntuacion += 50;
                    jugador.DanoExtra += 1;
                    estadoActual = GameState.Jugando;
                }
                return;
            }

            if (esTurnoJugador)
            {
                foreach (Boton boton in botones)
                {
                    boton.Update(mouse);

                    if (boton.Clicado(mouse, mouseAnterior))
                    {
                        ProcesarAccion(boton.Texto, ref estadoActual);
                    }
                }
            }

            else
                TurnoEnemigo(ref estadoActual);
        }

        private void ProcesarAccion(string accion, ref GameState estadoActual)
        {
            switch (accion)
            {
                case "Atacar":
                    enemigo.Vida -= 10 + 10*jugador.DanoExtra;
                    mensajeAccion = $"¡Has atacado al enemigo e infligido {20} de daño!";
                    esTurnoJugador = false;
                    break;

                case "Usar Pocion":
                    bool exito = jugador.Inventario.UsarObjeto("Pocion de Vida");

                    if (exito)
                    {
                        jugador.Vida += 20;

                        if (jugador.Vida > 100) jugador.Vida = 100;

                        mensajeAccion = "¡Has usado una poción de vida!";
                        esTurnoJugador = true;
                    }

                    else
                    {
                        mensajeAccion = "¡No tienes pociones de vida!";
                        esTurnoJugador = false;
                    }
                    break;

                case "Huir":
                    mensajeAccion = "¡Has huido del combate!";
                    Microsoft.Xna.Framework.Media.MediaPlayer.Stop(); 
                    estadoActual = GameState.Jugando;
                    
                    break;
            }
        }

        private void TurnoEnemigo(ref GameState estadoActual)
        {
            if (enemigo is JefeFinal)
            {
                JefeFinal jefe = (JefeFinal)enemigo;
                jefe.Atacar(jugador);
            }
            else
            {
                jugador.Vida -= 10;
            }
            if (jugador.Vida < 0)
            { 
                jugador.Vida = 0; 
                mensajeAccion = "¡Has sido derrotado por el enemigo!";
                esTurnoJugador = false;
                estadoActual = GameState.PantallaMuerte;
            }
            else
            {
                mensajeAccion = "¡El enemigo te ha atacado!";
                esTurnoJugador = true;
            }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(fondoCombate, Vector2.Zero, Color.White);

            Vector2 posOriginalJugador = jugador.Posicion;
            Vector2 posOriginalEnemigo = enemigo.Posicion;

            jugador.Posicion = posicionJugador;
            if (enemigo is JefeFinal)
            { 
                enemigo.Posicion = new Vector2(posicionEnemigo.X, 120);
            }
            else
            {
                enemigo.Posicion = posicionEnemigo;
            }

            jugador.Draw(sb, fuente);
            if (enemigo is JefeFinal)
            {
                enemigo.Draw(sb, 1, 1, 4.5f);
            }
            if (enemigo.Nombre == "Murcielago" || enemigo.Nombre == "murcielago")
            {
                enemigo.Draw(sb, 1, 4, 1.5f);
            }
            else if (enemigo.Nombre == "Slime" || enemigo.Nombre == "slime")
            {
                enemigo.Draw(sb, 1, 9, 4f);
            }
            else if (enemigo.Nombre == "Caballero" || enemigo.Nombre == "caballero")
            {
                enemigo.Draw(sb, 1, 8, 4f);
            }
            

            jugador.Posicion = posOriginalJugador;
            enemigo.Posicion = posOriginalEnemigo;

            sb.DrawString(fuente, $"Jugador HP: {jugador.Vida} / 100", new Vector2(posicionJugador.X - 50, posicionJugador.Y + 120), Color.White);
            sb.DrawString(fuente, $"Enemigo HP: {enemigo.Vida} / {vidaMaximaEnemigo}", new Vector2(posicionEnemigo.X - 50, posicionEnemigo.Y + 120), Color.White);

            sb.DrawString(fuente, mensajeAccion, new Vector2(100, 30), Color.Yellow);

            foreach (Boton b in botones)
            {
                b.Draw(sb);
            }
        }
    }
}
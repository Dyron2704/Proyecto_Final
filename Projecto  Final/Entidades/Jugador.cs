using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Projecto__Final.Inventarios;
using Projecto__Final.Items;
using Projecto__Final.Menús;
using Projecto__Final.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Projecto__Final.Entidades
{
    internal class Jugador : Entidad
    {
        float velocidad = 2.5f;
        int columnas;
        Inventario inventario;
        int puntuacion;
        int danoExtra;

        KeyboardState tecladoAnterior;
        
        public Inventario Inventario { get => inventario; set => inventario = value; }
        public int Puntuacion { get => puntuacion; set => puntuacion = value; }
        public int DanoExtra { get => danoExtra; set => danoExtra = value; }

        public Jugador(Texture2D textura, Vector2 pos, int vida, string nombre, int columnas, int danoExtra)
            : base(vida, nombre, textura, pos)
        {
            this.columnas = 8;
            inventario = new Inventario();
            tecladoAnterior = Keyboard.GetState();
            puntuacion = 0;
            danoExtra = 0;
        }

        public void Update(GameTime gameTime, Texture2D mapaColisiones, List<Cofre> cofres, Game1 juego)
        {
            KeyboardState teclado = Keyboard.GetState();
            Vector2 direccion = Vector2.Zero;
            bool moviendose = false;

            if (teclado.IsKeyDown(Keys.W)) { direccion.Y = -1; filaActual = 3; moviendose = true; }
            if (teclado.IsKeyDown(Keys.S)) { direccion.Y = 1; filaActual = 0; moviendose = true; }
            if (teclado.IsKeyDown(Keys.A)) { direccion.X = -1; filaActual = 1; moviendose = true; }
            if (teclado.IsKeyDown(Keys.D)) { direccion.X = 1; filaActual = 2; moviendose = true; }

            if (teclado.IsKeyDown(Keys.LeftShift)) { velocidad = 5f; }
            else { velocidad = 2.5f; }

            bool interactuado = false;

            if (teclado.IsKeyDown(Keys.F) && tecladoAnterior.IsKeyUp(Keys.F))
            {
                Rectangle rectJugador = new Rectangle((int)posicion.X, (int)posicion.Y, 32, 32);
                rectJugador.Inflate(15, 15); // Aumenta el área de interacción

                for (int i = 0; i < cofres.Count && !interactuado; i++)
                {
                    if (!cofres[i].abierto && cofres[i].area.Intersects(rectJugador))
                    {
                        if (cofres[i].Abrir())
                        {
                            if (cofres[i].esTrampa)
                            {
                                /* Esto es más o menos lo que hay que hacer
                                this.Vida -= 10;
                                Item armaEnemigo = new Item(cofres[i].contenido, "Arma soltada por enemigo");
                                armaEnemigo.Tipo = "Arma";

                                this.Inventario.AgregarObjeto(armaEnemigo);
                                juego.AgregarAlerta("¡Trampa! Has recibido 10 de daño pero obtuviste el arma.");
                                */
                            }
                            else
                            {
                                Item nuevoItem;
                                if (cofres[i].contenido == "Pocion de Vida")
                                {
                                    nuevoItem = new Pocion("Pocion de Vida", "Cura 20 HP", 20);
                                }
                                else
                                {
                                    nuevoItem = new Item(cofres[i].contenido, "Objeto común");
                                }

                                this.Inventario.AgregarObjeto(nuevoItem);
                                juego.AgregarAlerta($"Guardado: {cofres[i].contenido}");
                            }
                        }
                        
                        interactuado = true;
                    }
                }
            }

            if (teclado.IsKeyDown(Keys.E) && tecladoAnterior.IsKeyUp(Keys.E))
            {
                if (this.Inventario.UsarObjeto("Pocion de Vida"))
                {
                    this.Vida += 20;
                    if (this.Vida > 100) this.Vida = 100;
                }
            }

            tecladoAnterior = teclado;

            if (moviendose)
            {
                Vector2 nuevaPos = posicion + (direccion * velocidad);
                if (EsPosicionValida(nuevaPos, mapaColisiones))
                {
                    posicion = nuevaPos;
                }

                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (timer > 0.12f)
                {
                    columnaActual = (columnaActual + 1) % 4; // Ciclo de 4 frames
                    timer = 0;
                }
            }
            else { columnaActual = 0; }
        }

        private bool EsPosicionValida(Vector2 proximaPos, Texture2D col)
        {
            bool resultado = false;

            int x = (int)proximaPos.X + (textura.Width / columnas / 2);
            int y = (int)proximaPos.Y + (textura.Height / 4);

            if (x < 0 || x >= col.Width || y < 0 || y >= col.Height) resultado = false;

            Color[] pixel = new Color[1];
            col.GetData(0, new Rectangle(x, y, 1, 1), pixel, 0, 1);

            resultado = pixel[0] == Color.White;

            return resultado;
        }

        public override void Atacar(Entidad objetivo) { }

        public void Draw(SpriteBatch sb, SpriteFont fuente) 
        { 
            base.Draw(sb, columnas); 

            Vector2 posicionTexto = new Vector2(posicion.X, posicion.Y - 20);

            sb.DrawString(fuente, $"HP: {this.Vida}", posicionTexto, Color.White);
        }
    }
}

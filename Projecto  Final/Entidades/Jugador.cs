using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Projecto__Final.Entidades
{
    internal class Jugador: Entidad
    {
        int nivel;
        int experiencia;
        int energia;

        Texture2D textura;
        Vector2 posicion;
        int filaActual;
        int columnaActual;

        float timer = 0f;
        float intervalo = 0.12f;
        float velocidad = 2.5f;

        public Jugador(Texture2D textura, Vector2 posicionInicial, int vida, string nombre, int nivel = 0, int experiencia = 0, int energia = 0) : base(vida, nombre)
        {
            this.nivel = nivel;
            this.experiencia = experiencia;
            this.energia = energia;

            this.textura = textura;
            posicion = posicionInicial;
        }
       
        public override void Atacar(Entidad objetivo)
        {
            // Lógica de ataque del jugador
            
        }
        public void SubirNivel()
        {
            // Lógica para subir de nivel
        }
        public int Nivel { get => nivel; set => nivel = value; }
        public int Experiencia { get => experiencia; set => experiencia = value; }
        public int Energia { get => energia; set => energia = value; }

        public void Animar(GameTime gameTime, int inicio, int final)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer > intervalo)
            {
                columnaActual++;

                if (columnaActual > final) columnaActual = inicio;
                
                timer = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int ancho = textura.Width / 8;
            int alto = textura.Height / 4;

            Rectangle rectRecorte = new Rectangle(
                columnaActual * ancho,
                filaActual * alto,
                ancho,
                alto
            );

            spriteBatch.Draw(textura, posicion, rectRecorte, Color.White);
        }

        public void Update(GameTime gameTime, Texture2D mapaColisiones)
        {
            KeyboardState teclado = Keyboard.GetState();
            Vector2 direccion = Vector2.Zero;
            bool moviendose = false;

            if (teclado.IsKeyDown(Keys.W))
            {
                direccion.Y = -1;
                filaActual = 3;
                moviendose = true;
            }
            else if (teclado.IsKeyDown(Keys.S))
            {
                direccion.Y = 1;
                filaActual = 0;
                moviendose = true;
            }

            if (teclado.IsKeyDown(Keys.A))
            {
                direccion.X = -1;
                filaActual = 1;
                moviendose = true;
            }
            else if (teclado.IsKeyDown(Keys.D))
            {
                direccion.X = 1;
                filaActual = 2;
                moviendose = true;
            }

            Vector2 nuevaPosicion = posicion + (direccion * velocidad);
            if (EsPosicionValida(nuevaPosicion, mapaColisiones))
                posicion = nuevaPosicion;

            if (moviendose)
                Animar(gameTime, 0, 3);
            else
            {
                columnaActual = 0;
            }
        }

        private bool EsPosicionValida(Vector2 proximaPos, Texture2D mapaColisiones)
        {
            int anchoF = textura.Width / 8;
            int altoF = textura.Height / 4;

            int posX = (int)proximaPos.X + (anchoF / 2);
            int posY = (int)proximaPos.Y + altoF;

            bool resultado = false;
            
            if (posX < 0 || posX >= mapaColisiones.Width || posY < 0 || posY >= mapaColisiones.Height)
                resultado = false;

            Color[] colorExtraido = new Color[1];
            mapaColisiones.GetData(0, new Rectangle(posX, posY, 1, 1), colorExtraido, 0, 1);

            resultado = colorExtraido[0] != Color.Black;

            return resultado;
        }
    }
}

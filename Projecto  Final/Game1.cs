using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Projecto__Final.Entidades;
using Projecto__Final.Menús;
using System;
using System.Collections.Generic;

namespace Projecto__Final
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Atributos menú

        public enum GameState
        {
            MenuPrincipal,
            SeleccionPartida,
            MenuPersonajes,
            Jugando,
            Opciones
        }

        MenuPrincipal menuPrincipal;
        MenuSeleccion menuSeleccion;
        MenuOpciones menuOpciones;
        MenuPersonajes menuPersonajes;
        GameState estadoActual = GameState.MenuPrincipal;

        MouseState mouseAnterior;

        Nivel nivelActual;
        int numeroNivelActual = 1;

        Jugador jugador;
        Texture2D texturaPersonaje;
        Texture2D mapaColisiones;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        public void CargarMapa(string nombreMapa)
        {
            nivelActual = new Nivel();
            nivelActual.Fondo = Content.Load<Texture2D>(nombreMapa);
            string[] partesNombreMapa = nombreMapa.Split(' ');
            int nivel = Convert.ToInt32(partesNombreMapa[1]);
            numeroNivelActual = nivel;

            nivelActual.Colisiones = Content.Load<Texture2D>(nombreMapa + " Colisiones");

            if (nivel == 1)
            {
                nivelActual.Puerta1 = new Rectangle(250, 20, 64, 32);
            }
            else if (nivel == 2)
            {
                nivelActual.Puerta1 = new Rectangle(200, 600, 64, 32);
                nivelActual.Puerta2 = new Rectangle(210, 10, 64, 32);
            }
            else if (nivel == 3)
            {
                nivelActual.Puerta1 = new Rectangle(120, 600, 64, 32);
                nivelActual.Puerta2 = new Rectangle(1100, 15, 64, 32);
            }
            else if (nivel == 4)
            {
                nivelActual.Puerta1 = new Rectangle(60, 600, 64, 32);
            }
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            SpriteFont fuenteCargada = Content.Load<SpriteFont>("FuenteMenu");

            Texture2D botonNoPresionado = Content.Load<Texture2D>("Boton");
            Texture2D botonPresionado = Content.Load<Texture2D>("Boton Presionado");

            Texture2D fondoNormal = Content.Load<Texture2D>("FondoMenu");
            Texture2D fondoEspecial = Content.Load<Texture2D>("FondoMenuEspecial");

            List<Texture2D> listaPersonajesRecortados = new List<Texture2D>();
            listaPersonajesRecortados.Add(Content.Load<Texture2D>("Astrid - Menu"));
            listaPersonajesRecortados.Add(Content.Load<Texture2D>("Bellty - Menu"));
            listaPersonajesRecortados.Add(Content.Load<Texture2D>("Dormund - Menu"));
            listaPersonajesRecortados.Add(Content.Load<Texture2D>("Elyssa - Menu"));
            listaPersonajesRecortados.Add(Content.Load<Texture2D>("Flora - Menu"));
            listaPersonajesRecortados.Add(Content.Load<Texture2D>("Froyd - Menu"));
            listaPersonajesRecortados.Add(Content.Load<Texture2D>("Jade - Menu"));
            listaPersonajesRecortados.Add(Content.Load<Texture2D>("Joseph - Menu"));
            listaPersonajesRecortados.Add(Content.Load<Texture2D>("Martha - Menu"));
            listaPersonajesRecortados.Add(Content.Load<Texture2D>("Pesta - Menu"));

            List<string> nombres = new List<string> { "Astrid", "Bellty", "Dormund", "Elyssa", "Flora", "Froyd", "Jade", "Joseph", "Martha", "Pesta" };

            menuPrincipal = new MenuPrincipal(fondoNormal, fondoEspecial, botonNoPresionado, botonPresionado, fuenteCargada);
            menuSeleccion = new MenuSeleccion(fondoNormal, botonNoPresionado, botonPresionado, fuenteCargada);
            menuOpciones = new MenuOpciones(fondoNormal, botonNoPresionado, botonPresionado, fuenteCargada);
            menuPersonajes = new MenuPersonajes(fondoNormal, listaPersonajesRecortados, nombres, fuenteCargada, botonPresionado);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.LeftAlt))
                Exit();

            // TODO: Add your update logic here

            MouseState mouse = Mouse.GetState();

            switch (estadoActual)
            {
                case GameState.MenuPrincipal:
                    menuPrincipal.Update(mouse, mouseAnterior, ref estadoActual);
                    break;

                case GameState.Jugando:
                    if (jugador == null)
                    {
                        texturaPersonaje = Content.Load<Texture2D>(DatosPartida.PersonajeSeleccionado);
                        CargarMapa($"Pantalla {numeroNivelActual}");
                        jugador = new Jugador(texturaPersonaje, new Vector2(400, 300), 100, DatosPartida.PersonajeSeleccionado, DatosPartida.ColumnasPersonaje);
                    }

                    jugador.Update(gameTime, nivelActual.Colisiones);

                    Rectangle rectJugador = new Rectangle((int)jugador.Posicion.X, (int)jugador.Posicion.Y, 32, 32);

                    if (numeroNivelActual == 1)
                    {
                        if (rectJugador.Intersects(nivelActual.Puerta1))
                        {
                            CargarMapa($"Pantalla 2");
                            jugador.Posicion = new Vector2(200, 570);
                        }
                    }
                    else if (numeroNivelActual == 2)
                    {
                        if (rectJugador.Intersects(nivelActual.Puerta2))
                        {
                            CargarMapa($"Pantalla 3");
                            jugador.Posicion = new Vector2(100, 560);
                        }
                        else if (rectJugador.Intersects(nivelActual.Puerta1))
                        {
                            CargarMapa($"Pantalla 1");
                            jugador.Posicion = new Vector2(280, 100);
                        }
                    }
                    else if (numeroNivelActual == 3)
                    {
                        if (rectJugador.Intersects(nivelActual.Puerta2))
                        {
                            CargarMapa($"Pantalla 4");
                            jugador.Posicion = new Vector2(60, 560);
                        }
                        else if (rectJugador.Intersects(nivelActual.Puerta1))
                        {
                            CargarMapa($"Pantalla 2");
                            jugador.Posicion = new Vector2(200, 50); 
                        }
                    }
                    else if (numeroNivelActual == 4)
                    {
                        if (rectJugador.Intersects(nivelActual.Puerta1))
                        {
                            CargarMapa($"Pantalla 3");
                            jugador.Posicion = new Vector2(1100, 60);
                        }
                    }
                    break;

                case GameState.SeleccionPartida:
                    menuSeleccion.Update(mouse, mouseAnterior, ref estadoActual);
                    break;

                case GameState.Opciones:
                    menuOpciones.Update(mouse, mouseAnterior, ref estadoActual);
                    break;

                case GameState.MenuPersonajes:
                    menuPersonajes.Update(mouse, mouseAnterior, ref estadoActual);
                    break;
            }

            mouseAnterior = mouse;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            switch (estadoActual)
            {
                case GameState.MenuPrincipal:
                    menuPrincipal.Draw(_spriteBatch);
                    break;

                case GameState.Jugando:
                    if (nivelActual != null && nivelActual.Fondo != null)
                        _spriteBatch.Draw(nivelActual.Fondo, Vector2.Zero, Color.White);

                    if (jugador != null)    
                        jugador.Draw(_spriteBatch);
                    break;

                case GameState.SeleccionPartida:
                    menuSeleccion.Draw(_spriteBatch);
                    break;

                case GameState.Opciones:
                    menuOpciones.Draw(_spriteBatch);
                    break;

                case GameState.MenuPersonajes:
                    menuPersonajes.Draw(_spriteBatch);
                    break;
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

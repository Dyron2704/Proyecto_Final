using System;
using System.Collections.Generic;
using System.Transactions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Projecto__Final.Entidades;
using Projecto__Final.Menús;
using Projecto__Final.Transiciones;
using static Projecto__Final.Transiciones.TransicionPantalla;


namespace Projecto__Final
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameState estadoAnterior;

        RenderTarget2D pantallaA;
        RenderTarget2D pantallaB;
        TransicionPantalla transicion;

        // Atributos menú

        public enum GameState
        {
            MenuPrincipal,
            SeleccionPartida,
            MenuPersonajes,
            Jugando,
            Opciones,
            MenuEscape
        }

        MenuPrincipal menuPrincipal;
        MenuSeleccion menuSeleccion;
        MenuOpciones menuOpciones;
        MenuPersonajes menuPersonajes;
        MenuEscape menuEscape;
        GameState estadoActual = GameState.MenuPrincipal;

        MouseState mouseAnterior;
        KeyboardState tecladoAnterior;

        Nivel nivelActual;
        int numeroNivelActual = 1;

        Jugador jugador;
        Texture2D texturaPersonaje;
        Texture2D mapaColisiones;

        //variables prueba alertas
        List<Alertas> listaDeAlertas = new List<Alertas>();
        Texture2D texturaFondoAlerta;
        SpriteFont fuenteGlobal;
        string personajeSeleccionadoEnUso = "";

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

            //transición de la pantalla (de arriba a abajo)
            pantallaA = new RenderTarget2D(GraphicsDevice, 800, 600);
            pantallaB = new RenderTarget2D(GraphicsDevice, 800, 600);
            transicion = new TransicionPantalla(600);

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

            fuenteGlobal = Content.Load<SpriteFont>("FuenteMenu");
            menuEscape = new MenuEscape(GraphicsDevice, botonNoPresionado, botonPresionado, fuenteCargada);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.LeftAlt))
                Exit();

            // TODO: Add your update logic here

            MouseState mouse = Mouse.GetState();
            KeyboardState teclado = Keyboard.GetState();

            estadoAnterior = estadoActual;

            if (estadoActual == GameState.Jugando && teclado.IsKeyDown(Keys.Escape) && tecladoAnterior.IsKeyDown(Keys.Escape))
                estadoActual = GameState.MenuEscape;
            else if (estadoActual == GameState.MenuEscape && teclado.IsKeyDown(Keys.Escape) && !tecladoAnterior.IsKeyDown(Keys.Escape))
                estadoActual = GameState.Jugando;

            switch (estadoActual)
            {
                case GameState.MenuPrincipal:
                    menuPrincipal.Update(mouse, mouseAnterior, ref estadoActual);
                    break;

                case GameState.Jugando:
                    if (jugador == null || personajeSeleccionadoEnUso != DatosPartida.PersonajeSeleccionado)
                    {
                        texturaPersonaje = Content.Load<Texture2D>(DatosPartida.PersonajeSeleccionado);
                        personajeSeleccionadoEnUso = DatosPartida.PersonajeSeleccionado;

                        Vector2 posicionAnterior = new Vector2(400, 300);
                        if (jugador != null)
                        {
                            posicionAnterior = jugador.Posicion;
                        }

                        CargarMapa($"Pantalla {numeroNivelActual}");

                        jugador = new Jugador(texturaPersonaje, posicionAnterior, 100, DatosPartida.PersonajeSeleccionado, DatosPartida.ColumnasPersonaje);
                    }

                    jugador.Update(gameTime, nivelActual.Colisiones);

                    Rectangle rectJugador = new Rectangle((int)jugador.Posicion.X, (int)jugador.Posicion.Y, 32, 32);

                    if (numeroNivelActual == 1)
                    {
                        if (rectJugador.Intersects(nivelActual.Puerta1))
                        {
                            CargarMapa($"Pantalla 2");
                            jugador.Posicion = new Vector2(200, 570);
                            listaDeAlertas.Add(new Alertas("Has entrado en la Pantalla 2", new Vector2(500, 100), 3.0f));
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

                case GameState.MenuEscape:
                    menuEscape.Update(mouse, mouseAnterior, ref estadoActual);
                    break;
            }

            mouseAnterior = mouse;
            tecladoAnterior = teclado;

            //codigo prueba alertas
            foreach (var alerta in listaDeAlertas)
            {
                alerta.Update(gameTime);
            }

            listaDeAlertas.RemoveAll(a => !a.Activa);

            if (estadoActual == GameState.MenuPrincipal && estadoAnterior != GameState.MenuPrincipal)
            {
                {
                    Reset();
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            if (estadoActual == GameState.MenuEscape || estadoActual == GameState.Jugando)
            {
                if (nivelActual != null && nivelActual.Fondo != null)
                    _spriteBatch.Draw(nivelActual.Fondo, Vector2.Zero, Color.White);

                if (jugador != null)
                    jugador.Draw(_spriteBatch);

                if (estadoActual == GameState.MenuEscape)
                    menuEscape.Draw(_spriteBatch);
            }

            else
            {
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
            }

            //prueba alertas
            foreach (var alerta in listaDeAlertas)
            {
                _spriteBatch.DrawString(fuenteGlobal, alerta.Mensaje, alerta.Posicion, Color.Yellow * alerta.Opacidad);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void Reset()
        {
            estadoActual = GameState.MenuPrincipal;
            jugador = null;
            numeroNivelActual = 1;
            personajeSeleccionadoEnUso = "";
        }
    }
}

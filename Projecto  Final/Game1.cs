using System;
using System.Collections.Generic;
using System.Transactions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Projecto__Final.Entidades;
using Projecto__Final.Menús;
using Projecto__Final.Objetos;
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
            MenuEscape,
            Transiciones
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

            Texture2D texturaCofre=Content.Load<Texture2D>("cofre");

            if (nivel == 1)
            {
                nivelActual.Puerta1 = new Rectangle(250, 20, 64, 32);

                nivelActual.Cofres.Add(new Cofre(new Rectangle(285, 380, 40, 40), 
                    "Pocion de Vida", false, texturaCofre, null));
                nivelActual.Cofres.Add(new Cofre(new Rectangle(575, 220, 40, 40), "Trampa", 
                    true, texturaCofre, null));
                nivelActual.Cofres.Add(new Cofre(new Rectangle(670, 220, 40, 40), 
                    "Llave Antigua", false, texturaCofre, null));
            }
            else if (nivel == 2)
            {
                nivelActual.Puerta1 = new Rectangle(200, 600, 64, 32);
                nivelActual.Puerta2 = new Rectangle(210, 10, 64, 32);

                nivelActual.Cofres.Add(new Cofre(new Rectangle(730, 60, 40, 40),
                    "Pocion de Vida", false, texturaCofre, null));
                nivelActual.Cofres.Add(new Cofre(new Rectangle(1210, 130, 40, 40),
                    "Trampa tramposilla", false, texturaCofre, null));

                nivelActual.Cofres.Add(new Cofre(new Rectangle(480, 570, 40, 40),
                    "Pocion de Vida", false, texturaCofre, null));
                nivelActual.Cofres.Add(new Cofre(new Rectangle(800, 380, 40, 40),
                    "Trampa tramposa", false, texturaCofre, null));



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
        public void IniciarTransicion(string nombreNuevoMapa, Vector2 nuevaPosicionJugador,bool inversa)
        {
            GraphicsDevice.SetRenderTarget(pantallaA);
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            _spriteBatch.Draw(nivelActual.Fondo, Vector2.Zero, Color.White);
            jugador.Draw(_spriteBatch);
            _spriteBatch.End();

            CargarMapa(nombreNuevoMapa);
            jugador.Posicion = nuevaPosicionJugador;

            GraphicsDevice.SetRenderTarget(pantallaB);
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            _spriteBatch.Draw(nivelActual.Fondo, Vector2.Zero, Color.White);
            jugador.Draw(_spriteBatch);
            _spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);

            transicion.Iniciar(inversa);
            estadoActual = GameState.Transiciones;
        }
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            //transición de la pantalla (de arriba a abajo)
            int ancho = _graphics.PreferredBackBufferWidth;
            int alto = _graphics.PreferredBackBufferHeight;

            pantallaA = new RenderTarget2D(GraphicsDevice, ancho, alto);
            pantallaB = new RenderTarget2D(GraphicsDevice, ancho, alto);

            transicion = new TransicionPantalla(alto);

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

            if (estadoActual == GameState.Jugando && teclado.IsKeyDown(Keys.Escape) 
                && !tecladoAnterior.IsKeyDown(Keys.Escape))
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

                    Rectangle rectJugador = new Rectangle((int)jugador.Posicion.X, 
                        (int)jugador.Posicion.Y, 32, 32);

                    if (nivelActual?.Cofres != null)
                    {
                        foreach (var cofre in nivelActual.Cofres)
                        {
                            if (cofre.CheckInteraccion(rectJugador, teclado, tecladoAnterior))
                            {
                                if (cofre.esTrampa)
                                {
                                    listaDeAlertas.Add(new Alertas("Bicho fuera! Es una trampa"
                                        , jugador.Posicion, 2.0f));
                                }
                                else
                                {
                                    listaDeAlertas.Add(new Alertas($"Has encontrado: " +
                                        $"{cofre.contenido}", jugador.Posicion, 3.0f));
                                }
                            }
                            else if(cofre.abierto && rectJugador.Intersects(cofre.area))
                            {
                                if (teclado.IsKeyDown(Keys.F) && !tecladoAnterior.IsKeyDown(Keys.F))
                                {
                                    listaDeAlertas.Add(new Alertas("Este cofre ya esta vacio", 
                                        jugador.Posicion, 1.5f));
                                }
                            }
                        }
                    }
                    if (numeroNivelActual == 1)
                    {
                        if (rectJugador.Intersects(nivelActual.Puerta1))
                        {
                            IniciarTransicion($"Pantalla 2", new Vector2(200, 570),false);
                            //jugador.Posicion = new Vector2(200, 570);
                            listaDeAlertas.Add(new Alertas("Has entrado en la Pantalla 2", 
                                new Vector2(500, 100), 3.0f));
                        }
                    }
                    else if (numeroNivelActual == 2)
                    {
                        if (rectJugador.Intersects(nivelActual.Puerta2))
                        {
                            IniciarTransicion($"Pantalla 3", new Vector2(100, 560),false);
                            //jugador.Posicion = new Vector2(100, 560);
                        }
                        else if (rectJugador.Intersects(nivelActual.Puerta1))
                        {
                            IniciarTransicion($"Pantalla 1", new Vector2(280, 100),true);
                            //jugador.Posicion = new Vector2(280, 100);
                        }
                    }
                    else if (numeroNivelActual == 3)
                    {
                        if (rectJugador.Intersects(nivelActual.Puerta2))
                        {
                            IniciarTransicion($"Pantalla 4", new Vector2(60, 560), false);
                            //jugador.Posicion = new Vector2(60, 560);
                        }
                        else if (rectJugador.Intersects(nivelActual.Puerta1))
                        {
                            IniciarTransicion($"Pantalla 2", new Vector2(200, 50), true);
                            //jugador.Posicion = new Vector2(200, 50); 
                        }
                    }
                    else if (numeroNivelActual == 4)
                    {
                        if (rectJugador.Intersects(nivelActual.Puerta1))
                        {
                            IniciarTransicion($"Pantalla 3", new Vector2(1100, 60), true);
                            //jugador.Posicion = new Vector2(1100, 60);
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
                case GameState.Transiciones:
                    transicion.Update(gameTime);
                    if(!transicion.EstaActiva)
                    {
                        estadoActual= GameState.Jugando;
                    }
                    break;
            }

            //codigo prueba alertas
            foreach (var alerta in listaDeAlertas)
            {
                alerta.Update(gameTime);
            }

            listaDeAlertas.RemoveAll(a => !a.Activa);

            mouseAnterior = mouse;
            tecladoAnterior = teclado;
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
            _spriteBatch.Begin();
           
            // TODO: Add your drawing code here
            Console.WriteLine($"Estado actual: {estadoActual}");
            if (estadoActual == GameState.MenuEscape || estadoActual == GameState.Jugando)
            {
                if (nivelActual != null && nivelActual.Fondo != null)
                    _spriteBatch.Draw(nivelActual.Fondo, Vector2.Zero, Color.White);

                if (jugador != null)
                {
                    Texture2D pixel = new Texture2D(GraphicsDevice, 1, 1);
                    pixel.SetData(new[] { Color.White });

                    Rectangle rectJugador = new Rectangle((int)jugador.Posicion.X, 
                        (int)jugador.Posicion.Y, 32, 32);

                    _spriteBatch.Draw(pixel, rectJugador, Color.Red * 0.5f);

                    foreach (var cofre in nivelActual.Cofres)
                    {
                        _spriteBatch.Draw(pixel, cofre.area, Color.Blue * 0.5f);
                    }
                }

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
                        {
                            jugador.Draw(_spriteBatch);
                            
                        }
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
                    case GameState.Transiciones:
                        //GraphicsDevice.Clear(Color.Black);
                        transicion.Draw(_spriteBatch, pantallaA, pantallaB);
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

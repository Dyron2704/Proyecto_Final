using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Projecto__Final.Entidades;
using Projecto__Final.Menús;
using Projecto__Final.Objetos;
using Projecto__Final.Transiciones;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Transactions;
using static Projecto__Final.Transiciones.TransicionPantalla;


namespace Projecto__Final
{
    public class Game1 : Game
    {
        Random r = new Random();

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
            Transiciones,
            Combate,
            MenuGuardar,
            MenuCargar,
            PantallaMuerte,
            PantallaVictoria
        }

        MenuPrincipal menuPrincipal;
        MenuSeleccion menuSeleccion;
        MenuOpciones menuOpciones;
        MenuPersonajes menuPersonajes;
        MenuEscape menuEscape;
        MenuGuardado menuGuardado;
        MenuCargar menuCargar;
        

        Combate combate;
        GameState estadoActual = GameState.MenuPrincipal;

        MouseState mouseAnterior;
        KeyboardState tecladoAnterior;

        Nivel nivelActual;
        int numeroNivelActual = 1;

        Jugador jugador;
        Texture2D texturaPersonaje;
        Texture2D mapaColisiones;

        List<Alertas> listaDeAlertas = new List<Alertas>();
        Texture2D texturaFondoAlerta;
        SpriteFont fuenteGlobal;
        string personajeSeleccionadoEnUso = "";

        Enemigo[] enemigos;
        bool combateJefeIniciado = false;

        Texture2D pantallaMuerte;
        Texture2D pantallaVictoria;

        Song musicaExploracion;
        Song musicaCombate;
        Song musicaMenu;
        Song musicaJefe;

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

            Texture2D texturaCofre = Content.Load<Texture2D>("cofre");

            if (nivel == 1)
            {
                nivelActual.Puerta1 = new Rectangle(250, 20, 64, 32);

                nivelActual.Cofres.Add(new Cofre(new Rectangle(285, 380, 40, 40),
                    "Pocion de Vida", false, texturaCofre, null, 11));
                nivelActual.Cofres.Add(new Cofre(new Rectangle(575, 220, 40, 40), "Trampa trampera",
                    true, texturaCofre, null, 12));
                nivelActual.Cofres.Add(new Cofre(new Rectangle(670, 220, 40, 40),
                    "Poción de Vida", false, texturaCofre, null, 13));
            }
            else if (nivel == 2)
            {
                nivelActual.Puerta1 = new Rectangle(200, 600, 64, 32);
                nivelActual.Puerta2 = new Rectangle(210, 10, 64, 32);

                nivelActual.Cofres.Add(new Cofre(new Rectangle(730, 60, 40, 40),
                    "Pocion de Vida", false, texturaCofre, null,21));
                nivelActual.Cofres.Add(new Cofre(new Rectangle(1210, 130, 40, 40),
                    "Trampa tramposilla", true, texturaCofre, null,22));

                nivelActual.Cofres.Add(new Cofre(new Rectangle(480, 570, 40, 40),
                    "Pocion de Vida", false, texturaCofre, null,23));
                nivelActual.Cofres.Add(new Cofre(new Rectangle(800, 380, 40, 40),
                    "Trampa tramposa", true, texturaCofre, null,24));



            }
            else if (nivel == 3)
            {
                nivelActual.Puerta1 = new Rectangle(120, 600, 64, 32);
                nivelActual.Puerta2 = new Rectangle(1100, 15, 64, 32);

                nivelActual.Cofres.Add(new Cofre(new Rectangle(190, 250, 40, 40),
                    "Pocion de Vida", false, texturaCofre, null,31));
                nivelActual.Cofres.Add(new Cofre(new Rectangle(960, 310, 40, 40),
                    "Trampa tramposilla", true, texturaCofre, null,32));

                nivelActual.Cofres.Add(new Cofre(new Rectangle(1050, 310, 40, 40),
                    "Pocion de Vida", false, texturaCofre, null,33));
            }
            else if (nivel == 4)
            {
                nivelActual.Puerta1 = new Rectangle(60, 600, 64, 32);
            }
        }
        public void IniciarTransicion(string nombreNuevoMapa, Vector2 nuevaPosicionJugador, bool inversa)
        {
            GraphicsDevice.SetRenderTarget(pantallaA);
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            _spriteBatch.Draw(nivelActual.Fondo, Vector2.Zero, Color.White);
            jugador.Draw(_spriteBatch, fuenteGlobal);
            _spriteBatch.End();

            CargarMapa(nombreNuevoMapa);
            jugador.Posicion = nuevaPosicionJugador;

            GraphicsDevice.SetRenderTarget(pantallaB);
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            _spriteBatch.Draw(nivelActual.Fondo, Vector2.Zero, Color.White);
            jugador.Draw(_spriteBatch, fuenteGlobal);
            _spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);

            transicion.Iniciar(inversa);
            estadoActual = GameState.Transiciones;
        }

        protected override void Initialize()
        {
            Window.Title = "Proyecto Final: LA MAZMORRA";
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

            Texture2D fondoCombate = Content.Load<Texture2D>("Pantalla Combate");
            pantallaMuerte=Content.Load<Texture2D>("mapaMuerte");
            pantallaVictoria=Content.Load<Texture2D>("mapaVictoria");


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
            //menuPersonajes = new MenuPersonajes(fondoNormal, listaPersonajesRecortados, nombres, fuenteCargada, botonPresionado);
            //combate = new Combate(fondoCombate, jugador, enemigos, Content.Load<Texture2D>("Boton"), Content.Load<Texture2D>("Boton Presionado"), fuenteGlobal);
            menuPersonajes = new MenuPersonajes(fondoNormal, listaPersonajesRecortados, nombres, fuenteCargada, botonPresionado);


            fuenteGlobal = Content.Load<SpriteFont>("FuenteMenu");
            menuEscape = new MenuEscape(GraphicsDevice, botonNoPresionado, botonPresionado, fuenteCargada);

            menuGuardado = new MenuGuardado(fondoNormal, botonNoPresionado, botonPresionado, fuenteGlobal);
            menuCargar = new MenuCargar(fondoNormal, botonNoPresionado, botonPresionado, fuenteGlobal);

            texturaFondoAlerta = new Texture2D(GraphicsDevice, 1, 1);
            texturaFondoAlerta.SetData(new[] { Color.White });

            musicaCombate= Content.Load<Song>("MusicaCombate");
            musicaExploracion= Content.Load<Song>("MusicaExploracion");
            musicaMenu= Content.Load<Song>("MusicaMenu");
            musicaJefe= Content.Load<Song>("MusicaJefe");

            enemigos = new Enemigo[]
            {
                new Enemigo(40, "Slime", Content.Load<Texture2D>("Slime"), new Vector2(800, 300), 1, 10, 20),
                new Enemigo(70, "Murcielago", Content.Load<Texture2D>("enemy-bird"), new Vector2(800,300), 2, 20, 40),
                new Enemigo(120, "Caballero", Content.Load<Texture2D>("Caballero"), new Vector2(800, 300), 3, 40, 80)
            };
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

                    if (MediaPlayer.Queue.ActiveSong != musicaMenu)
                    {
                        MediaPlayer.IsRepeating = true;
                        MediaPlayer.Volume = 0.4f;
                        MediaPlayer.Play(musicaMenu);
                    }

                    menuPrincipal.Update(mouse, mouseAnterior, ref estadoActual);
                    break;

                case GameState.Jugando:
                    if (jugador == null || personajeSeleccionadoEnUso != DatosPartida.PersonajeSeleccionado)
                    {
                        texturaPersonaje = Content.Load<Texture2D>(DatosPartida.PersonajeSeleccionado);
                        personajeSeleccionadoEnUso = DatosPartida.PersonajeSeleccionado;

                        if (MediaPlayer.Queue.ActiveSong != musicaExploracion)
                        {
                            MediaPlayer.IsRepeating = true;
                            MediaPlayer.Volume = 0.5f;
                            MediaPlayer.Play(musicaExploracion);
                        }

                        Vector2 posicionAnterior = new Vector2(400, 300);
                        if (jugador != null)
                        {
                            posicionAnterior = jugador.Posicion;
                        }

                        CargarMapa($"Pantalla {numeroNivelActual}");

                        jugador = new Jugador(texturaPersonaje, posicionAnterior, 100, DatosPartida.PersonajeSeleccionado, DatosPartida.ColumnasPersonaje, DatosPartida.PuntuacionUsuario, this);
                    }

                    jugador.Update(gameTime, nivelActual.Colisiones, nivelActual.Cofres, this, ref estadoActual);

                    Rectangle rectJugador = new Rectangle((int)jugador.Posicion.X, (int)jugador.Posicion.Y, 32, 32);

                    if (numeroNivelActual == 1)
                    {
                        if (rectJugador.Intersects(nivelActual.Puerta1))
                        {
                            IniciarTransicion($"Pantalla 2", new Vector2(200, 570), false);
                            AgregarAlerta("Has entrado en la Pantalla 2");
                        }
                    }
                    else if (numeroNivelActual == 2)
                    {
                        if (rectJugador.Intersects(nivelActual.Puerta2))
                        {
                            IniciarTransicion($"Pantalla 3", new Vector2(100, 560), false);
                            //jugador.Posicion = new Vector2(100, 560);
                            AgregarAlerta("Has entrado en la Pantalla 3");
                        }
                        else if (rectJugador.Intersects(nivelActual.Puerta1))
                        {
                            IniciarTransicion($"Pantalla 1", new Vector2(280, 100), true);
                            //jugador.Posicion = new Vector2(280, 100);
                            AgregarAlerta("Has entrado en la Pantalla 1");
                        }
                    }
                    else if (numeroNivelActual == 3)
                    {
                        if (rectJugador.Intersects(nivelActual.Puerta2))
                        {
                            IniciarTransicion($"Pantalla 4", new Vector2(60, 560), false);
                            //jugador.Posicion = new Vector2(60, 560);
                            AgregarAlerta("Has entrado en la Pantalla 4");
                        }
                        else if (rectJugador.Intersects(nivelActual.Puerta1))
                        {
                            IniciarTransicion($"Pantalla 2", new Vector2(200, 50), true);
                            //jugador.Posicion = new Vector2(200, 50); 
                            AgregarAlerta("Has entrado en la Pantalla 2");
                        }
                    }
                    else if (numeroNivelActual == 4)
                    {
                        if (rectJugador.Intersects(nivelActual.Puerta1))
                        {
                            IniciarTransicion($"Pantalla 3", new Vector2(1100, 60), true);
                            //jugador.Posicion = new Vector2(1100, 60);
                            AgregarAlerta("Has entrado en la Pantalla 3");
                        }

                        if (jugador.Posicion.X > 550 && !combateJefeIniciado)
                        {
                            combateJefeIniciado = true;

                            Texture2D texBoton = Content.Load<Texture2D>("Boton");
                            Texture2D texBotonHover = Content.Load<Texture2D>("Boton Presionado");
                            Texture2D fondoCombateJefe = Content.Load<Texture2D>("Pantalla Combate");

                            Texture2D texturaJefe = Content.Load<Texture2D>("Jefe");

                            JefeFinal jefe = new JefeFinal(
                                this,                 
                                200,                  
                                "Jefe", 
                                texturaJefe,          
                                new Vector2(850, 350),
                                4, 500, 1000,        
                                true,                 
                                true                  
                            );

                            Enemigo[] grupoJefe = new Enemigo[] { jefe };

                            MediaPlayer.IsRepeating = true;
                            MediaPlayer.Volume = 0.6f;
                            MediaPlayer.Play(musicaJefe);

                            combate = new Combate(fondoCombateJefe, jugador, grupoJefe, texBoton, texBotonHover, fuenteGlobal);
                            estadoActual = GameState.Combate;

                            AgregarAlerta("¡El Guardián de la Lava bloquea tu camino!");
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

                case GameState.MenuGuardar:
                    if (estadoAnterior != GameState.MenuGuardar)
                    {
                        menuGuardado.CargarNombresDesdeFichero();
                        estadoAnterior = GameState.MenuGuardar;
                    }
                    menuGuardado.Update(mouse, mouseAnterior, this, ref estadoActual);
                    break;

                case GameState.MenuCargar:
                    if (estadoAnterior != GameState.MenuCargar)
                    {
                        menuCargar.ActualizarListaPerfiles();
                        estadoAnterior = GameState.MenuCargar;
                    }
                    menuCargar.Update(mouse, mouseAnterior, this, ref estadoActual);
                    break;

                case GameState.Transiciones:
                    transicion.Update(gameTime);
                    if (!transicion.EstaActiva)
                    {
                        estadoActual = GameState.Jugando;
                    }
                    break;

                case GameState.Combate:
                    combate.Update(gameTime, mouse, mouseAnterior, ref estadoActual);
                    break;
                case GameState.PantallaMuerte:
                    Console.WriteLine("Presione ENTER para continuar");
                    if (teclado.IsKeyDown(Keys.Enter) && !tecladoAnterior.IsKeyDown(Keys.Enter))
                    {
                        Reset(); 
                        estadoActual = GameState.MenuPrincipal;
                        AgregarAlerta("Volviendo al menú principal...");
                        MediaPlayer.Stop();
                    }
                    break;
                case GameState.PantallaVictoria:
                    Console.WriteLine("¡Has ganado! Presiona ENTER para volver al menú principal.");
                    if (teclado.IsKeyDown(Keys.Enter) && !tecladoAnterior.IsKeyDown(Keys.Enter))
                    {
                        Reset();
                        estadoActual = GameState.MenuPrincipal;
                        AgregarAlerta("Volviendo al menú principal...");
                        MediaPlayer.Stop();
                    }
                    break;
            }

            foreach (Alertas alerta in listaDeAlertas)
            {
                alerta.Update(gameTime);
            }

            listaDeAlertas.RemoveAll(a => !a.Activa);

            mouseAnterior = mouse;
            tecladoAnterior = teclado;

            if (estadoActual != GameState.MenuCargar && estadoActual != GameState.MenuGuardar)
            {
                estadoAnterior = estadoActual;
            }

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

                    jugador.Draw(_spriteBatch, fuenteGlobal); 
                    
                    /*_spriteBatch.Draw(pixel, rectJugador, Color.Red * 0.5f);

                    foreach (var cofre in nivelActual.Cofres)
                    {
                        _spriteBatch.Draw(pixel, cofre.area, Color.Blue * 0.5f);
                    }*/
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

                    case GameState.MenuGuardar:
                        menuGuardado.Draw(_spriteBatch, mouseAnterior);
                        break;

                    case GameState.MenuCargar:
                        menuCargar.Draw(_spriteBatch, mouseAnterior);
                        break;

                    case GameState.Jugando:
                        if (nivelActual != null && nivelActual.Fondo != null)
                            _spriteBatch.Draw(nivelActual.Fondo, Vector2.Zero, Color.White);

                        if (jugador != null)
                        {
                            jugador.Draw(_spriteBatch, fuenteGlobal);

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

                    case GameState.Combate:
                        combate.Draw(_spriteBatch);
                        break;
                    case GameState.PantallaMuerte:
                        if (pantallaMuerte != null)
                        {
                            _spriteBatch.Draw(pantallaMuerte, new Rectangle(0, 0, 1280, 720), Color.White);
                        }
                        break;
                    case GameState.PantallaVictoria:
                        if(pantallaVictoria !=null)
                        {
                            _spriteBatch.Draw(pantallaVictoria, new Rectangle(0, 0, 1280, 720), Color.White);
                        }
                        break;
                }
            }


            //prueba alertas
            foreach (Alertas alerta in listaDeAlertas)
            {
                Vector2 tamañoTexto = fuenteGlobal.MeasureString(alerta.Mensaje);

                int margen = 8;
                Rectangle areaFondo = new Rectangle(
                    (int)alerta.Posicion.X - margen,
                    (int)alerta.Posicion.Y - margen,
                    (int)tamañoTexto.X + (margen * 2),
                    (int)tamañoTexto.Y + (margen * 2)
                );

                _spriteBatch.Draw(texturaFondoAlerta, areaFondo, Color.Black * 0.5f * alerta.Opacidad);

                _spriteBatch.DrawString(fuenteGlobal, alerta.Mensaje, alerta.Posicion, Color.White * alerta.Opacidad);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
        public void AgregarAlerta(string texto)
        {
            int anchoVentana = GraphicsDevice.Viewport.Width;
            int altoVentana = GraphicsDevice.Viewport.Height;

            float posX = 30f;
            float posY = altoVentana / 2f;

            float espacioEntreAlertas = 37f;
            foreach (var alerta in listaDeAlertas)
            {
                alerta.Posicion = new Vector2(alerta.Posicion.X, alerta.Posicion.Y - espacioEntreAlertas);
            }

            listaDeAlertas.Add(new Alertas(texto, new Vector2(posX, posY), 3f));
        }

        public void IniciarCombate()
        {
            foreach (Enemigo e in enemigos)
            {
                if (e.Nombre == "Slime" || e.Nombre == "slime") e.Vida = 40;
                else if (e.Nombre == "Murcielago" || e.Nombre == "murcielago") e.Vida = 70;
                else if (e.Nombre == "Caballero" || e.Nombre == "caballero") e.Vida = 100;
            }

            combate = new Combate(
                Content.Load<Texture2D>("Pantalla Combate"),
                jugador,
                enemigos,
                Content.Load<Texture2D>("Boton"),
                Content.Load<Texture2D>("Boton Presionado"),
                fuenteGlobal
            );

            estadoActual = GameState.Combate;

            AgregarAlerta("¡Un cofre trampa te ha emboscado!");
        }

        public void Reset()
        {
            estadoActual = GameState.MenuPrincipal;
            jugador = null;
            numeroNivelActual = 1;
            personajeSeleccionadoEnUso = "";
        }

        public void GuardarJSON(string nombrePerfil)
        {
            DatosJugador datos = new DatosJugador();

            datos.NombrePrefil = nombrePerfil;
            datos.PersonajeTextura = DatosPartida.PersonajeSeleccionado;
            datos.PosX = jugador.Posicion.X;
            datos.PosY = jugador.Posicion.Y;
            datos.Vida = jugador.Vida;
            datos.NivelActual = numeroNivelActual;
            datos.PuntuacionUsuario = jugador.Puntuacion;

            datos.ObjetosGuardados = jugador.Inventario.Objetos;

            datos.CofresAbiertosIds = new List<int>();
            List<Cofre> cofresActuales = nivelActual.Cofres;

            for (int i = 0; i < cofresActuales.Count; i++)
            {
                if (cofresActuales[i].abierto)
                {
                    datos.CofresAbiertosIds.Add(i);
                }
            }

            string jsonString = JsonSerializer.Serialize(datos);

            string carpetaSaves = "Saves";
            if (!Directory.Exists(carpetaSaves))
            {
                Directory.CreateDirectory(carpetaSaves);
            }

            string rutaCompleta = Path.Combine(carpetaSaves, nombrePerfil + ".json");
            File.WriteAllText(rutaCompleta, jsonString);

            AgregarAlerta($"¡Partida '{nombrePerfil}' guardada!");
        }

        public void CargarPartida(string nombrePerfil)
        {
            string ruta = Path.Combine("Saves", nombrePerfil + ".json");

            if (File.Exists(ruta))
            {
                string contenido = File.ReadAllText(ruta);
                DatosJugador datos = JsonSerializer.Deserialize<DatosJugador>(contenido);

                DatosPartida.PersonajeSeleccionado = datos.PersonajeTextura;
                numeroNivelActual = datos.NivelActual;

                personajeSeleccionadoEnUso = datos.PersonajeTextura;

                CargarMapa("Pantalla " + numeroNivelActual);

                texturaPersonaje = Content.Load<Texture2D>(DatosPartida.PersonajeSeleccionado);
                jugador = new Jugador(texturaPersonaje, new Vector2(datos.PosX, datos.PosY),
                                     datos.Vida, datos.PersonajeTextura, DatosPartida.ColumnasPersonaje, datos.PuntuacionUsuario, this);

                jugador.Inventario.Objetos = datos.ObjetosGuardados;

                for (int i = 0; i < datos.CofresAbiertosIds.Count; i++)
                {
                    int indice = datos.CofresAbiertosIds[i];

                    if (indice < nivelActual.Cofres.Count)
                    {
                        nivelActual.Cofres[indice].abierto = true;
                    }
                }

                estadoActual = GameState.Jugando;
                AgregarAlerta("Partida cargada con éxito");
            }
        }
    }
}
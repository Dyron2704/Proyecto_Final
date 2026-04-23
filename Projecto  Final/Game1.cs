using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Projecto__Final.Menús;
using System;

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
            Jugando,
            Opciones
        }

        MenuPrincipal menuPrincipal;
        MenuSeleccion menuSeleccion;
        MenuOpciones menuOpciones;
        GameState estadoActual = GameState.MenuPrincipal;

        MouseState mouseAnterior;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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

            Texture2D texProvisional = new Texture2D(GraphicsDevice, 1, 1);
            texProvisional.SetData(new[] { Color.White });

            Texture2D botonNoPresionado = Content.Load<Texture2D>("Boton");
            Texture2D botonPresionado = Content.Load<Texture2D>("Boton Presionado");

            Texture2D fondoNormal = Content.Load<Texture2D>("FondoMenu");
            //Texture2D fondoEspecial = Content.Load<Texture2D>("FondoMenuEspecial");

            menuPrincipal = new MenuPrincipal(fondoNormal, texProvisional, botonNoPresionado, botonPresionado, fuenteCargada);
            menuSeleccion = new MenuSeleccion(fondoNormal, botonNoPresionado, botonPresionado, fuenteCargada);
            menuOpciones = new MenuOpciones(fondoNormal, botonNoPresionado, botonPresionado, fuenteCargada);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            MouseState mouse = Mouse.GetState();

            switch (estadoActual)
            {
                case GameState.MenuPrincipal:
                    menuPrincipal.Update(mouse, mouseAnterior, ref estadoActual);
                    break;

                case GameState.Jugando:
                    // Como aún no tenemos la lógica del juego, por ahora no haremos nada aquí
                    break;

                case GameState.SeleccionPartida:
                    menuSeleccion.Update(mouse, mouseAnterior, ref estadoActual);
                    break;

                case GameState.Opciones:
                    menuOpciones.Update(mouse, mouseAnterior, ref estadoActual);
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
                    // De momento, solo pondremos un fondo diferente para distinguirlo del menú
                    GraphicsDevice.Clear(Color.CornflowerBlue);
                    break;

                case GameState.SeleccionPartida:
                    menuSeleccion.Draw(_spriteBatch);
                    break;

                case GameState.Opciones:
                    menuOpciones.Draw(_spriteBatch);
                    break;
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

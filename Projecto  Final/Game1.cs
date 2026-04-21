using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
            Jugando,
            Opciones
        }

        MenuPrincipal menuPrincipal;
        GameState estadoActual = GameState.MenuPrincipal;


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

            // TODO: use this.Content to load your game content here

            // Textura provisional para el fondo y los botones, hasta que Lucía tenga las imágenes definitivas
            Texture2D texProvisional = new Texture2D(GraphicsDevice, 1, 1);
            texProvisional.SetData(new[] { Color.White });

            // Cuando Lucía tenga los fondos pondre: Content.Load<Texture2D>("nombre_imagen")
            menuPrincipal = new MenuPrincipal(
                fondoNormal: texProvisional, 
                fondoHover: texProvisional, 
                textureBoton: texProvisional
            );
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
                    menuPrincipal.Update(mouse, ref estadoActual);
                    break;

                case GameState.Jugando:
                    // Como aún no tenemos la lógica del juego, por ahora no haremos nada aquí
                    break;
            }

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
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

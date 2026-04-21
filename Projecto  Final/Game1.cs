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
            Opciones,
            Jugando,
            Pausa
        }

        private GameState estadoActual = GameState.MenuPrincipal;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            // Cambio del estado del juego

            switch (estadoActual)
            {
                case GameState.MenuPrincipal:
                    UpdateMenu();
                    break;
                case GameState.Opciones:
                    UpdateOpciones();
                    break;
                case GameState.Jugando:
                    UpdateJuego();
                    break;
                case GameState.Pausa:
                    UpdatePausa();
                    break;
            }

            base.Update(gameTime);
        }

        private void UpdatePausa()
        {
            throw new NotImplementedException();
        }

        private void UpdateJuego()
        {
            throw new NotImplementedException();
        }

        private void UpdateOpciones()
        {
            throw new NotImplementedException();
        }

        private void UpdateMenu()
        {
            throw new NotImplementedException();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            // Dependiendo del estado se dibuja una cosa u otra

            switch (estadoActual)
            {
                case GameState.MenuPrincipal:
                    DrawMenu();
                    break;
                case GameState.Opciones:
                    DrawOpciones();
                    break;
                case GameState.Jugando:
                    DrawJuego();
                    break;
                case GameState.Pausa:
                    DrawPausa();
                    break;
            }

            base.Draw(gameTime);
        }

        private void DrawMenu()
        {
            throw new NotImplementedException();
        }

        private void DrawOpciones()
        {
            throw new NotImplementedException();
        }

        private void DrawJuego()
        {
            throw new NotImplementedException();
        }

        private void DrawPausa()
        {
            throw new NotImplementedException();
        }
    }
}

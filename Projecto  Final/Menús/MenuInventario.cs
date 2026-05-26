using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Projecto__Final.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Projecto__Final.Menús
{
    internal class MenuInventario
    {
        private Texture2D _backgroundTexture;
        private Texture2D _slotTexture;
        private SpriteFont _font;

        private int _slotsX = 5;
        private int _slotsY = 3;
        private int _slotSize = 64;
        private int _spacing = 10;

        private int _selectedSlotX = 0;
        private int _selectedSlotY = 0;

        public MenuInventario()
        {
        }

        public void LoadContent(Texture2D background, Texture2D slot, SpriteFont font)
        {
            _backgroundTexture = background;
            _slotTexture = slot;
            _font = font;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keys = Keyboard.GetState();


        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            if (_backgroundTexture != null)
            {
                spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height), Color.White);
            }
            else
            {
            }

            int gridWidth = (_slotsX * _slotSize) + ((_slotsX - 1) * _spacing);
            int gridHeight = (_slotsY * _slotSize) + ((_slotsY - 1) * _spacing);
            int startX = (graphicsDevice.Viewport.Width - gridWidth) / 2;
            int startY = (graphicsDevice.Viewport.Height - gridHeight) / 2;

            for (int x = 0; x < _slotsX; x++)
            {
                for (int y = 0; y < _slotsY; y++)
                {
                    int posX = startX + (x * (_slotSize + _spacing));
                    int posY = startY + (y * (_slotSize + _spacing));

                    Rectangle slotRect = new Rectangle(posX, posY, _slotSize, _slotSize);

                    Color slotColor = (x == _selectedSlotX && y == _selectedSlotY) ? Color.Yellow : Color.White;

                    if (_slotTexture != null)
                    {
                        spriteBatch.Draw(_slotTexture, slotRect, slotColor);
                    }
                }
            }

            if (_font != null)
            {
                spriteBatch.DrawString(_font, "INVENTARIO (Estructura)", new Vector2(startX, startY - 40), Color.White);
                spriteBatch.DrawString(_font, "Presiona la tecla configurada para salir", new Vector2(startX, startY + gridHeight + 20), Color.Gray);
            }
        }
    }
}

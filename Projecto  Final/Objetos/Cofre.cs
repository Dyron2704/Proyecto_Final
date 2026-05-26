using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Projecto__Final.Objetos
{
    internal class Cofre
    {
        public Rectangle area;
        public string contenido;
        public bool esTrampa;
        public bool abierto = false;
        private Texture2D _texturaCerrado;
        private Texture2D _texturaAbierto;
        public int id;

        public Cofre(Rectangle area,string contenido,bool esTrampa,
            Texture2D texturaCerrado, Texture2D texturaAbierto,int id)
        {
            this.area = area;
            this.contenido = contenido;
            this.esTrampa = esTrampa;
            this.abierto= false;
            this._texturaCerrado = texturaCerrado;
            this._texturaAbierto = texturaAbierto;
            this.id = id;
        }

        public int GetId()
        {
            return id;
        }

        public void SetId(int id)
        {
            this.id = id;
        }
        

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle zonaInteraccion = area;
            zonaInteraccion.Inflate(20, 20);
            if (!abierto && _texturaCerrado != null)
            {
                spriteBatch.Draw(_texturaCerrado, area, Color.White);
            }
            
        }

        public bool Abrir()
        {
            bool resultado = false;

            if (!abierto)
            {
                abierto = true;
                resultado = true;
            }

            return resultado;
        }
    }
}


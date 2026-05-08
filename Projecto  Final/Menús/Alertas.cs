using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto__Final.Menús
{
    public class Alertas
    {
        public string Mensaje { get; set; }
        public Vector2 Posicion { get; set; }
        public float Cronometro { get; set; } 
        public float Duracion { get; set; }   
        public bool Activa { get; set; }
        public float Opacidad { get; set; }  

        public Alertas(string mensaje, Vector2 pos, float duracion)
        {
            Mensaje = mensaje;
            Posicion = pos;
            Duracion = duracion;
            Cronometro = 0f;
            Activa = true;
            Opacidad = 1f;
        }

        public void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Cronometro += delta;

            if (Cronometro >= Duracion)
            {
                Opacidad -= delta * 2f; 
                if (Opacidad <= 0) Activa = false;
            }
        }
    }
}

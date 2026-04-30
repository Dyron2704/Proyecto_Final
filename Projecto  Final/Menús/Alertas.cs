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
        public float Cronometro { get; set; } // Cuánto tiempo lleva activa
        public float Duracion { get; set; }   // Cuánto tiempo debe durar
        public bool Activa { get; set; }
        public float Opacidad { get; set; }   // Para el efecto de desvanecimiento (Fade out)

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

            // Si llega al final, empezamos a bajar la opacidad
            if (Cronometro >= Duracion)
            {
                Opacidad -= delta * 2f; // Se desvanece en medio segundo
                if (Opacidad <= 0) Activa = false;
            }
        }
    }
}

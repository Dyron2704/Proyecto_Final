using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Projecto__Final.Objetos
{
    internal class Cofre
    {
        public Rectangle area;
        public string contenido;
        public bool esTrampa;
        public bool abierto = false;

        public void CheckInteraccion(Rectangle jugadorHitbox)
        {
            if (!abierto && area.Intersects(jugadorHitbox))
            {
                abierto = true;
                if (esTrampa)
                {

                    Console.WriteLine("¡Bicho fuera!");
                }
                else
                {
                    Console.WriteLine("Has encontrado: " + contenido);
                }
            }
        }
    }
}


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Projecto__Final.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto__Final
{
    internal class Nivel
    {
        Texture2D fondo;
        Texture2D colisiones;
        Rectangle puerta1;
        Rectangle puerta2;
        string siguienteFondo;
        List<Cofre> cofres;

        public Nivel()
        {
            cofres=new List<Cofre>();
        }

        public Texture2D Fondo { get => fondo; set => fondo = value; }
        public Texture2D Colisiones { get => colisiones; set => colisiones = value; }
        public Rectangle Puerta1 { get => puerta1; set => puerta1 = value; }
        public string SiguienteFondo { get => siguienteFondo; set => siguienteFondo = value; }
        public Rectangle Puerta2 { get => puerta2; set => puerta2 = value; }
        public List<Cofre> Cofres { get => cofres; set => cofres = value; }
    }
}

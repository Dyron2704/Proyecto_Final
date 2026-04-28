using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto__Final
{
    public static class DatosPartida
    {
        static string personajeSeleccionado;
        static int columnasPersonaje;

        static string mapaActual;
        static string mapaColisiones;

        public static string PersonajeSeleccionado { get => personajeSeleccionado; set => personajeSeleccionado = value; }
        public static int ColumnasPersonaje { get => columnasPersonaje; set => columnasPersonaje = value; }
        public static string MapaActual { get => mapaActual; set => mapaActual = value; }
        public static string MapaColisiones { get => mapaColisiones; set => mapaColisiones = value; }


    }
}

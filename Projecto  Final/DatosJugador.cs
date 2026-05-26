using Projecto__Final.Inventarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto__Final
{
    internal class DatosJugador
    {
        string nombrePrefil;
        string personajeTextura;
        float posX, posY;
        int vida;
        int puntuacionUsuario;

        int nivelActual;
        List<ObjetoInventario> objetosGuardados;
        List<int> cofresAbiertosIds;

        public string NombrePrefil { get => nombrePrefil; set => nombrePrefil = value; }
        public string PersonajeTextura { get => personajeTextura; set => personajeTextura = value; }
        public float PosX { get => posX; set => posX = value; }
        public float PosY { get => posY; set => posY = value; }
        public int Vida { get => vida; set => vida = value; }
        public int PuntuacionUsuario { get => puntuacionUsuario; set => puntuacionUsuario = value; }
        public int NivelActual { get => nivelActual; set => nivelActual = value; }
        public List<int> CofresAbiertosIds { get => cofresAbiertosIds; set => cofresAbiertosIds = value; }
        public List<ObjetoInventario> ObjetosGuardados { get => objetosGuardados; set => objetosGuardados = value; }
    }
}

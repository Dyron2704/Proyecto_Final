using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto__Final.Guardado
{
    internal class Serializacion
    {
        string nombrePrefil;
        string personajeTextura;
        float posX, posY;
        int vida;
        int danyoExtra;

        int nivelActual;
        List<string> inventarioNombres;
        List<int> cofresAbiertosIds;

        public string NombrePrefil { get => nombrePrefil; set => nombrePrefil = value; }
        public string PersonajeTextura { get => personajeTextura; set => personajeTextura = value; }
        public float PosX { get => posX; set => posX = value; }
        public float PosY { get => posY; set => posY = value; }
        public int Vida { get => vida; set => vida = value; }
        public int DanyoExtra { get => danyoExtra; set => danyoExtra = value; }
        public int NivelActual { get => nivelActual; set => nivelActual = value; }
        public List<string> InventarioNombres { get => inventarioNombres; set => inventarioNombres = value; }
        public List<int> CofresAbiertosIds { get => cofresAbiertosIds; set => cofresAbiertosIds = value; }

        
    }
}

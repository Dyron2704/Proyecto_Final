using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto__Final.Guardado
{
    internal class GuardadoEngine
    {
        static string perfilesUsuario = "Saves/PerfilesUsuario.txt";


        public static void RegistrarPerfil(string nombre)
        {
            List<string> perfiles = File.Exists(perfilesUsuario) ? File.ReadAllLines(perfilesUsuario).ToList() : new List<string>();
            
            if (!perfiles.Contains(nombre))
                File.AppendAllLines(perfilesUsuario, new[] { nombre });
        }
    }
}

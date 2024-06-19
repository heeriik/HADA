using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
        // Clase para los argumentos del evento cuando una casilla es tocada
        public class TocadoArgs : EventArgs
        {
            // Propiedades
            public string Nombre { get; }
            public Coordenada CoordenadaImpacto { get; }
            public string Etiqueta { get; }

            // Constructor
            public TocadoArgs(string nombre, Coordenada coordenadaImpacto, string etiqueta)
            {
                this.Nombre = nombre;
                this.CoordenadaImpacto = coordenadaImpacto;
                this.Etiqueta = etiqueta;
            }
        }

        // Clase para los argumentos del evento cuando el barco está hundido
        public class HundidoArgs : EventArgs
        {
            // Propiedades
            public string Nombre { get; }

            // Constructor
            public HundidoArgs(string nombre)
            {
                this.Nombre = nombre;
            }
        }
}
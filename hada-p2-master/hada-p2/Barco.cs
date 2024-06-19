using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class Barco
    {
        public Dictionary<Coordenada, String> CoordenadasBarco { get; private set; }
        public string Nombre { get; }
        public int NumDanyos { get; private set; }

        public Barco(string nombre, int longuitud, char orientacion, Coordenada coordenadaInicio)
        {
            this.Nombre = nombre;
            this.NumDanyos = 0;
            this.CoordenadasBarco = new Dictionary<Coordenada, String>();

            //Inicializar las coordenadas
            int cFila = coordenadaInicio.Fila;
            int cColum = coordenadaInicio.Columna;

            for (int i = 0; i < longuitud; i++)
            {
                Coordenada newCoordenada;
                if (orientacion == 'h')
                {
                    newCoordenada = new Coordenada(cFila, cColum + i);
                    CoordenadasBarco.Add(newCoordenada, Nombre);

                }
                else if (orientacion == 'v')
                {
                    newCoordenada = new Coordenada(cFila + i, cColum);
                    CoordenadasBarco.Add(newCoordenada, Nombre);
                }
                else
                {
                    throw new ArgumentException("Error orientacion");
                }
            }

            
        }

        public bool hundido()
        {
            foreach (string etiqueta in CoordenadasBarco.Values)
            {
                if (!etiqueta.EndsWith("_T"))
                {
                    return false;
                } 
            }
            return true;
        }

        public void Disparo(Coordenada c)
        {
            //si el usuario acierta la coordenada del barco
            if (CoordenadasBarco.ContainsKey(c))
            {
                CoordenadasBarco[c] += "_T";
                NumDanyos++;

                
                if(eventoTocado != null)
                {
                    eventoTocado(this, new TocadoArgs(this.Nombre,c,CoordenadasBarco[c]));
                }
                if (hundido() && eventoHundido != null)
                {
                    //eventoTocado(this, new TocadoArgs(this.Nombre, c, CoordenadasBarco[c]));
                    eventoHundido(this, new HundidoArgs(this.Nombre));
                }
            }
        }

        public override string ToString()
        {
            string coordenadas = "";
            foreach (var kvp in CoordenadasBarco)
            {
                coordenadas += $"[({kvp.Key.Fila},{kvp.Key.Columna}): {kvp.Value}] ";
            }
            //coordenadas = coordenadas.TrimEnd(',', ' ');

            return $"[{Nombre}] - DAÑOS: [{NumDanyos}] - HUNDIDO: [{hundido()}] - Coordenadas: {coordenadas}";
        }
        public event EventHandler<TocadoArgs> eventoTocado;
        public event EventHandler<HundidoArgs> eventoHundido;
    }
}
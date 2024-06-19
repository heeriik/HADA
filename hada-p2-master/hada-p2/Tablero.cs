using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class Tablero
    {
        private int _tamTablero;
        public int TamTablero
        {
            get
            {
                return _tamTablero;
            }
            set
            {
                if (value < 4 && value > 9)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "The valid range is between 4 and 9.");
                }
                _tamTablero = value;
            }
        }

        private List<Coordenada> coordenadasDisparadas;
        private List<Coordenada> coordenadasTocadas;
        private List<Barco> barcos;
        private List<Barco> barcosEliminados;
        private Dictionary<Coordenada, string> casillasTablero;


        public Tablero(int tamTablero, List<Barco> barcos)
        {
            if(tamTablero < 4 || tamTablero > 9)
            {
                throw new ArgumentException("uwu");
            }
            this.TamTablero = tamTablero;
            this.barcos = new List<Barco>();
            coordenadasDisparadas = new List<Coordenada>();
            coordenadasTocadas = new List<Coordenada>();
            barcosEliminados = new List<Barco>();
            casillasTablero = new Dictionary<Coordenada, string>();

            for (int i = 0; i < barcos.Count(); i++)
            {
                Barco barco = barcos[i];
                barco.eventoTocado += cuandoEventoTocado;
                barco.eventoHundido += cuandoEventoHundido;
                this.barcos.Add(barco);
            }
            inicializaCasillasTablero();
        }
        private void inicializaCasillasTablero()
        {
            for (int i = 0; i < TamTablero; i++)
            {
                for (int j = 0; j < TamTablero; j++)
                {
                    Coordenada cord = new Coordenada(i, j);
                    string valor = "AGUA";
                    casillasTablero.Add(cord, valor);
                }
            }
            foreach (Barco barco in this.barcos)
            {
                foreach (KeyValuePair<Coordenada, string> kvp in barco.CoordenadasBarco)
                {
                    Coordenada coordenada = new Coordenada(kvp.Key.Fila, kvp.Key.Columna);
                    casillasTablero[coordenada] = kvp.Value;
                }
            }
        }


        public void Disparar(Coordenada c)
        {
            if(c.Fila > TamTablero-1 || c.Fila < 0 || c.Columna < 0 || c.Columna > TamTablero - 1)
            {
                Console.WriteLine("La cordenada (" + c.Fila + "," + c.Columna + ") esta fuera de las dimensiones del tablero.");
            }
            for (int i = 0; i < barcos.Count; i++)
            {
                barcos[i].Disparo(c);    
            }
            coordenadasDisparadas.Add(c);
        }

        private string DibujarTablero()
        {
            string dibujo = string.Empty;
            for(int i=0; i<TamTablero; i++)
            {
                for(int j=0; j<TamTablero; j++)
                {
                    Coordenada cord = new Coordenada(i, j);
                    dibujo = dibujo + "[" + casillasTablero[cord] + "]";
                }
                dibujo += "\n";
            }
            return dibujo;
        }
        
        public override string ToString()
        {
            string info = "";
            for (int i = 0; i < barcos.Count(); i++)
            {
                info += barcos[i].ToString() + "\n";
            }

            info += "\nCoordenadas disparadas: ";
            
            for (int i = 0; i < coordenadasDisparadas.Count(); i++)
            {
                Coordenada cd = coordenadasDisparadas[i];
                info += $"({cd.Fila}, {cd.Columna}) ";
            }

            info += "\nCoordenadas tocadas: ";
            for (int i = 0; i < coordenadasTocadas.Count(); i++)
            {
                Coordenada ct = coordenadasTocadas[i];
                info += $"({ct.Fila},{ct.Columna}) ";
            }

            info += "\n\n\n\n";
            info += "CASILLAS TABLERO\n-------\n" ;
            info += DibujarTablero() + "\n";   
            return info;
        }

        private void cuandoEventoTocado(object sender, TocadoArgs e)
        {
            Coordenada coordenada = e.CoordenadaImpacto;
            string nombreBarco = e.Nombre;
            coordenadasTocadas.Add(coordenada);
            casillasTablero[coordenada] = $"{nombreBarco}_T";
            Console.WriteLine($"TABLERO: Barco [{nombreBarco}] tocado en Coordenada: [({coordenada.Fila},{coordenada.Columna}) ]");
            
            /*Barco barco = (Barco)sender;
            this.casillasTablero[e.CoordenadaImpacto] = $"{barco.Nombre}_T";
            Console.WriteLine($"TABLERO: Barco [{barco.Nombre}] tocado en Coordenada: [{e.CoordenadaImpacto}]");
            */    
        }

        protected virtual void cuandoEventoFinPartida()
        {
            eventoFinPartida?.Invoke(this, EventArgs.Empty);
        }

        private void cuandoEventoFinPartida(object sender, EventArgs e)
        {
            // Lógica para finalizar el juego...
            Console.WriteLine("Fin del juego.");
            Environment.Exit(0);
        }

        private void cuandoEventoHundido(object sender, HundidoArgs e)
        {
            string nombreBarco = e.Nombre;
            Console.WriteLine($"TABLERO: Barco [{nombreBarco}] hundido!!");
            Barco barco = (Barco)sender;
            barcosEliminados.Add(barco);

            /* NO TOCAR, solo tocar en disparo de Barco.cs
            //actualizar la etiqueta cuando está hundod (Arreglo del bug)
            foreach (KeyValuePair<Coordenada, string> kvp in barco.CoordenadasBarco)
            {
                Coordenada coordenada = kvp.Key;
                casillasTablero[coordenada] += "_T";
            }
            */

            //comprobar si todos los barcos están hundidos
            if (barcosEliminados.Count == barcos.Count)
            {
                eventoFinPartida += cuandoEventoFinPartida;
                this.eventoFinPartida(this, new EventArgs());
            }
        }
        public event EventHandler<EventArgs> eventoFinPartida;

    }
}
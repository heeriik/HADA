using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Hada
{
    public class Game
    {
        private bool finPartida;
        public Game()
        {
            finPartida = false;
            gameLoop();
        }

        
        private void gameLoop()
        {
            //finPartida = true;
            Barco thor = new Barco("THOR", 1, 'h', new Coordenada(0, 0));
            Barco loki = new Barco("LOKI", 2, 'v', new Coordenada(1, 2));
            Barco maya = new Barco("MAYA", 3, 'h', new Coordenada(3, 1));
            List<Barco> barcos = new List<Barco>
            {
                thor,
                loki,
                maya
            };
            Tablero tablero = new Tablero(4, barcos);
            

            do
            {
                Console.WriteLine("Introduce la coordenada a la que dispara FILA,COLUMNA ('S' para Salir):");
                //simplificar la condición con tolower
                string cord = Console.ReadLine().ToLower();
                Regex regex = new Regex(@"^\d,\d$");
                if (cord != "s")
                {
                    if (regex.IsMatch(cord))
                    {
                        string[] partes = cord.Split(',');
                        string fila = partes[0];
                        string columna = partes[1];
                        Coordenada c = new Coordenada(fila, columna);
                        tablero.Disparar(c);
                        Console.WriteLine(tablero);
                    }
                    
                }
                else
                {
                    finPartida = true;
                }
            } while (finPartida == false);
        }
    }
}
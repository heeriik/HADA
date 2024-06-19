using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class Coordenada
    {
        private int _fila;
        public int Fila
        {
            get
            {
                return _fila;
            }
            set
            {
                if(value < 0 || value > 9)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),"The valid range is between 0 and 3.");
                }
                _fila = value;
            }
        }
        private int _columna;
        public int Columna
        {
            get
            {
                return _columna;
            }
            set
            {
                if (value < 0 || value > 9)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "The valid range is between 0 and 3.");
                }
                _columna = value;
            }
        }
        public Coordenada()
        {
            
        }
        public Coordenada(int fila, int columna)
        {
            Columna = columna;
            Fila = fila;
        }
        public Coordenada(string fila, string columna)
        {
            Fila = int.Parse(fila);
            Columna = int.Parse(columna);
        }
        public Coordenada(Coordenada cor)
        {
            Fila = cor.Fila;
            Columna = cor.Columna;
        }
        override
        public string ToString()
        {
            return ("(" + Fila + "," + Columna + ")");
        }
        override
        public int GetHashCode()
        {
            return this.Fila.GetHashCode() ^ this.Columna.GetHashCode();
        }
        override
        public bool Equals(Object obj)
        {
            Coordenada cordItem = obj as Coordenada;
            return (cordItem.Columna == this.Columna && cordItem.Fila == this.Fila);
        }
        public bool Equals(Coordenada cord)
        {
            return (cord.Columna == this.Columna && cord.Fila == this.Fila);
        }

    }
}

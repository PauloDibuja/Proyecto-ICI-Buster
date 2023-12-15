using System;
using Rating;

namespace BlockBuster{
    abstract class Productos{

        private string? codigo;
        private string? nombre;
        private int anio;
        private string? genero;
        private int copias;

        public Productos(string codigo, string nombre, int anio, string genero, int copias){
            this.codigo = codigo;
            this.nombre = nombre;
            this.anio = anio;
            this.genero = genero;
            this.copias = copias;
        }

        public Productos(){
            this.codigo = "";
            this.nombre = "";
            this.anio = -1;
            this.genero = "";
            this.copias = -1;
        }

        public string? Codigo{
            get { return codigo; }
            set { codigo = value; }
        }
        public string? Nombre{
            get { return nombre; }
            set { nombre = value; }
        }
        public int Anio{
            get { return anio; }
            set { anio = value; }
        }
        public string? Genero{
            get { return genero; }
            set { genero = value; }
        
        }
        public int Copias{
            get{return copias;}
            set{copias = value;}
        }

        public abstract void ImprimirDetalles();
    }
}
using System;

namespace BlockBuster{
    class Cliente{
        private string? nombre;
        private int edad;
        private string? direccion;
        private string? telefono;
        
        public string? Nombre{
            get {return this.nombre;}
            set {this.nombre = value;}
        }


        public int Edad{
            get { return this.edad; }
            set { this.edad = value; }
        }

        public string? Direccion{
            get {return this.direccion;}
            set {this.direccion = value;}
        }

        public string? Telefono{
            get { return this.telefono; }
            set { this.telefono = value; }
        }        
    }
}
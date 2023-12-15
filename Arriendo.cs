using System;


namespace BlockBuster{
    class Arriendo{

        private string? nombre;
        List<string> arriendos = new List<string>();
        private string? fechaArriendo = DateTime.Now.ToString("dd-MM-yyyy");

        public Arriendo(string nombre, string codigos, DateTime fechaArriendo){
            this.nombre = nombre;
            this.fechaArriendo = fechaArriendo.ToString("dd-MMM-yyyy");
            
            string[] datos;
            datos = codigos.Split(",");
            foreach(string codigo in datos){
                this.arriendos.Add(codigo.Trim());
            }
        }

        public string? Nombre{
            get {return this.nombre;}
            set {this.nombre = value;}
        }

        public List<string> Arriendos{
            get {return this.arriendos;}
        }

        public string? FechaArriendo{
            get {return this.fechaArriendo;}
            set {this.fechaArriendo = value;}
        }
    }
}
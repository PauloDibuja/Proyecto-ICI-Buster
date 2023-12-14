using System;
using Rating;


namespace BlockBuster{
    class Juego : Productos{

        private string? plataforma;
        private ESRB esrb_rating;

        public Juego(string codigo, string nombre, int anio, string genero, ESRB esrb_rating, string plataforma, int copias) : base(codigo, nombre, anio, genero, copias){
            this.plataforma = plataforma;
            this.esrb_rating = esrb_rating;
        }


        public string? Plataforma{
            get { return plataforma; }
            set { plataforma = value; }
        }

        public ESRB ESRB_Rating{
            get { return esrb_rating; }
            set { esrb_rating = value; }
        }
    }
}
using System;
using Rating;


namespace BlockBuster{
    class Pelicula : Productos{

        private string? duracion;
        private MPA mpa_rating;
        public Pelicula(string codigo, string nombre, int anio, string genero, MPA mpa_rating, string duracion, int copias) : base(codigo, nombre, anio, genero, copias){
            this.duracion = duracion;
            this.mpa_rating = mpa_rating;
        }

        public string? Duracion{
            get { return duracion; }
            set { duracion = value; }
        }

        public MPA MPA_Rating{
            get { return mpa_rating; }
            set { mpa_rating = value; }
        }

    }
}
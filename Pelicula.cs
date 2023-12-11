using System;
using Rating;


namespace BlockBuster{
    class Pelicula : Productos{

        private string? duracion;
        public Pelicula(string nombre, int anio, string genero, string clasificacion, string duracion) : base(nombre, anio, genero){
            this.duracion = duracion;
        }
    }
}
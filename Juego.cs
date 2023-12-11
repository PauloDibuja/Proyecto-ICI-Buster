using System;
using Rating;


namespace BlockBuster{
    class Juego : Productos{

        private string? plataforma;

        public Juego(string nombre, int anio, string genero, string clasificacion, string plataforma) : base(nombre, anio, genero){
            this.plataforma = plataforma;
        }
    }
}
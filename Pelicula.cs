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

        public bool ExistePorNombre(List<Pelicula> peli_List, string nombre)
        {
            return peli_List.Where(x => x.Nombre == nombre).ToList().Count > 0;
        }

        public override void ImprimirDetalles(){
            Console.WriteLine("----------------------------");
            Console.WriteLine($"Código: {this.Codigo}");
            Console.WriteLine($"Título: {this.Nombre}");
            Console.WriteLine($"        Año: {this.Anio}");
            Console.WriteLine($"        Género: {this.Genero}");
            Console.WriteLine($"        Copias Disponibles: {this.Copias}");
            Console.WriteLine($"        Duración: {this.Duracion} min");
            Console.WriteLine($"        Clasificación: {this.MPA_Rating}");
        }
    }
}
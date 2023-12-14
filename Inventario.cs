using System;

namespace BlockBuster{

    class Inventario{

        private Dictionary<string, Productos> listaProductos = new Dictionary<string, Productos>();

        public Inventario(){
            ArchivoPeliculas archivoPeliculas = new ArchivoPeliculas();
            ArchivoJuegos archivoJuegos = new ArchivoJuegos();

            Dictionary<string, Pelicula> peliculas = archivoPeliculas.archivoPeliculas(".\\data\\Peliculas.txt");
            Dictionary<string, Juego> juegos = archivoJuegos.archivoJuegos(".\\data\\Juegos.txt");

            foreach (var keys in peliculas){
            listaProductos.Add(keys.Key, keys.Value);
            }
            foreach (var keys in juegos){
                listaProductos.Add(keys.Key, keys.Value);
            }
        }

        public void Agregar(string codigo){
            listaProductos[codigo].Copias++;
        }
        public void Quitar(string codigo){
            listaProductos[codigo].Copias--;
        }

        public Dictionary<string, Productos> ListaProductos{
            get { return listaProductos; }
        }
    }
}
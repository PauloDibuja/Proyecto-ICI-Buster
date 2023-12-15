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

        public void ActualizarArchivoInventario(){
            List<Pelicula> peliculas = new List<Pelicula>();
            List<Juego> juegos = new List<Juego>();
            foreach(var clave_producto in ListaProductos){
                if(clave_producto.Value is Pelicula) peliculas.Add((Pelicula)clave_producto.Value);
                else if(clave_producto.Value is Juego) juegos.Add((Juego)clave_producto.Value);
            }
            using(StreamWriter peliFile = new StreamWriter(".\\data\\Peliculas.txt")){
                foreach(var peli in peliculas)
                    peliFile.WriteLine($"{peli.Codigo};{peli.Nombre};{peli.Anio};{peli.Genero};{peli.MPA_Rating};{peli.Duracion};{peli.Copias}");
            }
            using(StreamWriter juegoFile = new StreamWriter(".\\data\\Juegos.txt")){
                foreach(var juego in juegos)
                    juegoFile.WriteLine($"{juego.Codigo};{juego.Nombre};{juego.Anio};{juego.Genero};{juego.ESRB_Rating};{juego.Plataforma};{juego.Copias}");
            }
        }

        public void Agregar(string codigo){
            listaProductos[codigo].Copias++;
        }
        public void Quitar(string codigo){
            if(listaProductos[codigo].Copias <= 0){
                Console.WriteLine("No existe una copia de ese producto.");
                return;
            }
            listaProductos[codigo].Copias--;
        }

        public Dictionary<string, Productos> ListaProductos{
            get { return listaProductos; }
        }

        public static Inventario operator +(Inventario inventario, string codigo){
            if(!inventario.ListaProductos.ContainsKey(codigo)){
                Console.WriteLine("No existe un producto con ese código.");
                return inventario;
            }
            inventario.Agregar(codigo);
            return inventario;
        }

        public static Inventario operator -(Inventario inventario, string codigo){
            if(!inventario.ListaProductos.ContainsKey(codigo)){
                Console.WriteLine("No existe un producto con ese código.");
                return inventario;
            }
            inventario.Quitar(codigo);
            return inventario;
        }
    }
}
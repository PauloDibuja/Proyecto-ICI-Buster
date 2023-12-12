using System;
using Rating;
using BlockBuster;
using System.Reflection.PortableExecutable;

class Program{

    static void ImprimirPagina(List<Pelicula> peliculas, int pagina_actual){
        Console.Clear();
        int index_elemento_inicial = pagina_actual * 4;
        int index_elemento_final = Math.Min(index_elemento_inicial + 4, peliculas.Count);

        for (int i = index_elemento_inicial; i < index_elemento_final; i++)
        {
            Pelicula pelicula = peliculas[i];
            Console.WriteLine("----------------------------");
            Console.WriteLine($"Código: {pelicula.Codigo}");
            Console.WriteLine($"Título: {pelicula.Nombre}");
            Console.WriteLine($"        Año: {pelicula.Anio}");
            Console.WriteLine($"        Género: {pelicula.Genero}");
            Console.WriteLine($"        Copias Disponibles: {pelicula.Copias}");
            Console.WriteLine($"        Duración: {pelicula.Duracion} min");
            Console.WriteLine($"        Clasificación: {pelicula.MPA_Rating}");
        }
        Console.WriteLine("----------------------------");
    }

    static void Enpaginado(List<Pelicula> peliculas){
        int pagina_actual = 0;
        int cantidad_paginas = peliculas.Count % 4 == 0 ? peliculas.Count / 4 : (peliculas.Count / 4) + 1;
        ImprimirPagina(peliculas, pagina_actual);
        while(true){
            Console.WriteLine("Utilice las flechas <- y -> para desplazarse en el inventario. Presione Esc para salir.");
            ConsoleKeyInfo tecla = Console.ReadKey(true);
            if (tecla.Key == ConsoleKey.RightArrow)
            {
                pagina_actual++;
                pagina_actual = Math.Clamp(pagina_actual, 0, cantidad_paginas - 1);
                ImprimirPagina(peliculas, pagina_actual);
            }
            else if(tecla.Key == ConsoleKey.LeftArrow){
                pagina_actual--;
                pagina_actual = Math.Clamp(pagina_actual, 0, cantidad_paginas - 1);
                ImprimirPagina(peliculas, pagina_actual);
            }
            else if (tecla.Key == ConsoleKey.Escape)
            {
                break;
            }
        }

    }

    static void Main(string[] args){
        ArchivoPeliculas archivoPeliculas = new ArchivoPeliculas();
        ArchivoJuegos archivoJuegos = new ArchivoJuegos();

        List<Pelicula> peliculas = archivoPeliculas.archivoPeliculas(".\\data\\Peliculas.txt");
        List<Juego> juegos = archivoJuegos.archivoJuegos(".\\data\\Juegos.txt");

        Enpaginado(peliculas);
    }
}
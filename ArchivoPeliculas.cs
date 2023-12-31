using System;
using Rating;

namespace BlockBuster
{
   class ArchivoPeliculas{
   public Dictionary<string, Pelicula> archivoPeliculas(string archivo){

        Dictionary<string, Pelicula> peliculas = new Dictionary<string, Pelicula>();
        RatingInfo ratingInfo = new RatingInfo();

        String? linea;
            try{
                StreamReader sr = new StreamReader(archivo);
                linea = sr.ReadLine();
                
                while (linea != null){
                    string[] datos;
                    datos = linea.Split(";");

                    if (datos.Length == 7){

                        Pelicula pelicula = new Pelicula(datos[0], datos[1], Convert.ToInt32(datos[2]), datos[3], ratingInfo.MPA_Ratings[datos[4]], datos[5], Convert.ToInt32(datos[6]));
                        peliculas.Add(datos[0], pelicula);
                    }
                    linea = sr.ReadLine();
                }
                sr.Close();
            }catch (FileNotFoundException ){
                Console.WriteLine("No se encuentra el archivo");
            }catch (IOException){
                Console.WriteLine("Error al intentar leer el archivo");
            }
            return peliculas;
        }
    }
}

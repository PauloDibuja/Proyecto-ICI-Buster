using System;

namespace BlockBuster
{
   class ArchivoPeliculas{
   public List<Pelicula> archivoPeliculas(string archivo){

        List<Pelicula> peliculas = new List<Pelicula> ();

        String? linea;
            try{
                StreamReader sr = new StreamReader(archivo);
                linea = sr.ReadLine();
                
                while (linea != null){
                    string[] datos;
                    datos = linea.Split(";");

                    if (datos.Length == 5){
                        Pelicula pelicula = new Pelicula(datos[0], Convert.ToInt32(datos[1]), datos[2], datos[3], datos[4]);
                        peliculas.Add(pelicula);
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

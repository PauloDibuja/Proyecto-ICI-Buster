using System;
using Rating;
namespace BlockBuster
{
   class ArchivoJuegos{
   public List<Juego> archivoJuegos(string archivo){

        List<Juego> juegos = new List<Juego> ();
        RatingInfo ratingInfo = new RatingInfo();
        String? linea;
            try{
                StreamReader sr = new StreamReader(archivo);
                linea = sr.ReadLine();
                
                while (linea != null){
                    string[] datos;
                    datos = linea.Split(";");

                    if (datos.Length == 7){
                        Juego juego = new Juego(datos[0], datos[1], Convert.ToInt32(datos[2]), datos[3], ratingInfo.ESRB_Ratings[datos[4]], datos[5], Convert.ToInt32(datos[6]));
                        juegos.Add(juego);
                    }
                    linea = sr.ReadLine();
                }
                sr.Close();
            }catch (FileNotFoundException ){
                Console.WriteLine("No se encuentra el archivo");
            }catch (IOException){
                Console.WriteLine("Error al intentar leer el archivo");
            }
            return juegos;
        }
    }
}

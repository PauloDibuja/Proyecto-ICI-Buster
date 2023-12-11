using System;

namespace BlockBuster
{
   class ArchivoJuegos{
   public List<Juego> archivoJuegos(string archivo){

        List<Juego> juegos = new List<Juego> ();

        String? linea;
            try{
                StreamReader sr = new StreamReader(archivo);
                linea = sr.ReadLine();
                
                while (linea != null){
                    string[] datos;
                    datos = linea.Split(";");

                    if (datos.Length == 5){
                        Juego juego = new Juego(datos[0], Convert.ToInt32(datos[1]), datos[2], datos[3], datos[4]);
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

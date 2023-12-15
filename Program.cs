using System;
using Rating;
using BlockBuster;
using BlockBusterUI;
using System.Runtime.CompilerServices;

class Program{

     // Métodos Principales

    static void Main(string[] args){
        Console.WriteLine("BIENVENIDO A DIBUJA'S BUSTER");
        Console.WriteLine("El local donde puedes alquilar entretenimiento a precio de unos lápices.");
        Inventario inventario = new Inventario();
        Funcionamiento funcionamiento = new Funcionamiento();
        Interfaz interfaz = new Interfaz();
        Cliente cliente = new Cliente();
        funcionamiento.ObtenerDatosCliente(cliente);
        int precioArriendo = 6500;
        int precioVenta = 6500; 
        //interfaz.Enpaginado(inventario.ListaProductos);
        int peticion;
        do{
            funcionamiento.imprimirSeparador();
            peticion = funcionamiento.ElegirMetodo();
            switch(peticion){
                case 0:
                    funcionamiento.ArrendarProducto(inventario, cliente, precioArriendo);
                    break;
                case 1:
                    funcionamiento.ComprarProducto(inventario, cliente, precioVenta);
                    break;
                case 2:
                    // Devolver
                    funcionamiento.DevolverProducto(inventario, cliente);
                    break;
                case 3:
                    // Ver Productos
                    interfaz.Enpaginado(inventario);
                    break;
                case 4:
                    // Recomendar Producto
                    funcionamiento.RecomendarProducto(inventario);
                    break;
                default:
                    break;
            }
            inventario.ActualizarArchivoInventario();
            funcionamiento.imprimirSeparador();
        }while(peticion != 5);

        Console.WriteLine("Está bien, nos vemos luego. Que tenga un excelente día. :D");
        funcionamiento.imprimirSeparador();
        // 2 devolver

        



    }
}
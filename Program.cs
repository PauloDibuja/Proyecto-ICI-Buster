using System;
using Rating;
using BlockBuster;
using BlockBusterUI;
using System.Runtime.CompilerServices;

class Program{

     // Métodos Principales

    static void Main(string[] args){
        Inventario inventario = new Inventario();
        Funcionamiento funcionamiento = new Funcionamiento();
        Interfaz interfaz = new Interfaz();
        Cliente cliente = new Cliente();
        funcionamiento.ObtenerDatosCliente(cliente);
        //interfaz.Enpaginado(inventario.ListaProductos);
        int peticion;
        do{
            peticion = funcionamiento.ElegirMetodo();
            switch(peticion){
                case 0:
                    funcionamiento.ArrendarProducto(inventario.ListaProductos, cliente);
                    break;
                case 1:
                    funcionamiento.ComprarProducto(inventario.ListaProductos, cliente);
                    break;
                case 2:
                    // Devolver
                    break;
                case 3:
                    // Ver Productos
                    break;
                case 4:
                    // Recomendar Producto
                    break;
                default:
                    break;
            }
        }while(peticion != 5);

        
        // 2 devolver

        



    }
}
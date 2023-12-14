using System;
using BlockBuster;
using Money;


namespace ImpresionBoleta{
    class Boleta{
        
        public void ImprimirBoleta(Cliente cliente, 
                                    List<Productos> carrito, int precio, bool es_arriendo, int proximaOrden = -1){


            int j = 1;
            using(StreamWriter boleta = new StreamWriter(".\\data\\Boleta.txt")){
                boleta.WriteLine("DIBUJA'S BUSTER");
                boleta.WriteLine("ARRIENDO - VIDEOCLUB");
                boleta.WriteLine();
                boleta.WriteLine("----------------------------------");
                boleta.WriteLine("CLIENTE:");
                boleta.WriteLine($"Nombre: {cliente.Nombre}");
                boleta.WriteLine($"Edad: {cliente.Edad}");
                boleta.WriteLine($"Dirreción: {cliente.Direccion}");
                boleta.WriteLine($"Telefono: {cliente.Telefono}");
                boleta.WriteLine("----------------------------------");
                foreach (var producto in carrito){
                    boleta.WriteLine($"{"Item", -5}{"Código", -8}{"Título", -30} {"Valor", -11}");
                    boleta.WriteLine($"{j, -5}{producto.Codigo, -8}{producto.Nombre, -30} {"$6500", -11}");
                }
                boleta.WriteLine();
                boleta.WriteLine($"Total: $ {precio}");
                boleta.WriteLine("----------------------------------");
                boleta.WriteLine($"Tipo de Servicio: {(es_arriendo ? "Arriendo" : "Compra")}");
                if(proximaOrden >= 0 && es_arriendo) boleta.WriteLine($"Orden de Arriendo: A{proximaOrden.ToString().PadLeft(3, '0')}");
            }
        }
        public void ImprimirFacturacion(int monto, int vuelto){
            using(StreamWriter boleta = new StreamWriter(".\\data\\Boleta.txt", true)){
                boleta.WriteLine("Metodo de Pago: Efectivo");
                boleta.WriteLine($"Monto: $ {monto}");
                boleta.WriteLine($"Entregado: $ {vuelto + monto}");
                boleta.WriteLine($"Vuelto: $ {vuelto}");
            }
        }
        public void ImprimirFacturacion(int monto, IMetodoPago metodoPago){
            using(StreamWriter boleta = new StreamWriter(".\\data\\Boleta.txt", true)){
                if(metodoPago is Debito){
                boleta.WriteLine("Método de Pago: Débito");
                }else if(metodoPago is Credito){
                    boleta.WriteLine("Método de Pago: Crédito");
                }else{
                    boleta.WriteLine($"Método de Pago: -");
                }

                boleta.WriteLine($"Monto: $ {monto}");
            }
        }

    }
}

/*
 boleta.WriteLine("----<FACTURACIÓN>------");
                if(metodoPago is Efectivo) ImprimirMetodoPago(boleta, metodoPago, precio, entregado).;
*/
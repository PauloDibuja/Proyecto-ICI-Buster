namespace Money{
    class Efectivo : IMetodoPago{

        public void Pagar(int monto){
            Console.WriteLine($"Se ha realizado un pago de $ {monto} en Efectivo");
        }
    }


    class Debito : IMetodoPago{
        public void Pagar(int monto){
            Console.WriteLine($"Se ha realizado un pago de $ {monto} en Debito");
        }
    }


    class Credito : IMetodoPago{
        public void Pagar(int monto){
            Console.WriteLine($"Se ha realizado un pago de $ {monto} en Credito");
        }
    }
}



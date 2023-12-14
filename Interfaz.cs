using System;
using Rating;
using BlockBuster;
using System.Text.RegularExpressions;
using Money;
using ImpresionBoleta;

namespace BlockBusterUI
{
    class Interfaz
    {

        public void ImprimirDetalles(Pelicula producto){
            Console.WriteLine($"        Duración: {producto.Duracion} min");
            Console.WriteLine($"        Clasificación: {producto.MPA_Rating}");
        }
        public void ImprimirDetalles(Juego producto){
            Console.WriteLine($"        Plataforma: {producto.Plataforma} min");
            Console.WriteLine($"        Clasificación: {producto.ESRB_Rating}");
        }

        public void ImprimirPagina(List<Productos> productos, int pagina_actual){
            Console.Clear();
            int index_elemento_inicial = pagina_actual * 4;
            int index_elemento_final = Math.Min(index_elemento_inicial + 4, productos.Count);

            for (int i = index_elemento_inicial; i < index_elemento_final; i++)
            {
                Productos producto = productos[i];
                Console.WriteLine("----------------------------");
                Console.WriteLine($"Código: {producto.Codigo}");
                Console.WriteLine($"Título: {producto.Nombre}");
                Console.WriteLine($"        Año: {producto.Anio}");
                Console.WriteLine($"        Género: {producto.Genero}");
                Console.WriteLine($"        Copias Disponibles: {producto.Copias}");
                if (producto is Pelicula) ImprimirDetalles((Pelicula)producto);
                if (producto is Juego) ImprimirDetalles((Juego)producto);
            }
            Console.WriteLine("----------------------------");
        }

        public void Enpaginado(Dictionary<string, Productos> productos){
            int pagina_actual = 0;
            int cantidad_paginas = productos.Count % 4 == 0 ? productos.Count / 4 : (productos.Count / 4) + 1;
            List<Productos> listaParaMenu = new List<Productos>();
            ImprimirPagina(productos.Values.ToList(), pagina_actual);
            while (true)
            {
                Console.WriteLine("Utilice las flechas <- y -> para desplazarse en el inventario. Presione Esc para salir.");
                ConsoleKeyInfo tecla = Console.ReadKey(true);
                if (tecla.Key == ConsoleKey.RightArrow)
                {
                    pagina_actual++;
                    pagina_actual = Math.Clamp(pagina_actual, 0, cantidad_paginas - 1);
                    ImprimirPagina(productos.Values.ToList(), pagina_actual);
                }
                else if (tecla.Key == ConsoleKey.LeftArrow)
                {
                    pagina_actual--;
                    pagina_actual = Math.Clamp(pagina_actual, 0, cantidad_paginas - 1);
                    ImprimirPagina(productos.Values.ToList(), pagina_actual);
                }
                else if (tecla.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }



        // Funcionamiento

        
    }

    class Funcionamiento{
        public int ElegirMetodo(){
            // Saludar al cliente
            bool opcionElegida = false;
            int opcion = -1;

            Console.WriteLine($"¿Qué desea?\n");
            Console.WriteLine($"0 - Arrendar Producto");
            Console.WriteLine($"1 - Comprar Producto");
            Console.WriteLine($"2 - Devolver Producto");
            Console.WriteLine($"3 - Ver Productos");
            Console.WriteLine($"4 - Recomendar Producto");
            Console.WriteLine($"5 - Adiós\n");

            while (!opcionElegida)
            {
                try
                {
                    Console.Write(">> ");
                    opcion = Convert.ToInt32(Console.ReadLine());
                    opcionElegida = opcion >= 0 && opcion <= 5;
                    if (!opcionElegida) Console.WriteLine("Número no válido");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Por favor, ingrese un número para indicar qué desea.\n");
                }
            }
            return opcion;
        }

        // Ingresar Datos CLiente

        public string IngresarDatosCliente(Func<char, bool> function, string mensaje1, string mensaje2, string mensaje3){
            string? textoSolicitado;
            do{
                Console.Write($"{mensaje1}\n>> ");
                textoSolicitado = Console.ReadLine();

                if(textoSolicitado == "" || textoSolicitado == null){
                    Console.WriteLine($"{mensaje2}\n");
                    imprimirSeparador();
                } else if(textoSolicitado.All(function) == false){
                    Console.WriteLine($"{mensaje3}\n");
                    imprimirSeparador();
                    textoSolicitado = "";
                }
            }while(textoSolicitado == null || textoSolicitado == "");
            return textoSolicitado;
        }
        public string IngresarDatosCliente(string regexPatron, string mensaje1, string mensaje2, string mensaje3){
            Regex expr = new Regex(regexPatron);
            string? textoSolicitado = "";
            do{
                Console.Write($"{mensaje1}\n>> ");
                textoSolicitado = Console.ReadLine();
                if(textoSolicitado == "" || textoSolicitado == null){
                    Console.WriteLine($"{mensaje2}\n");
                    imprimirSeparador();
                }else if(expr.IsMatch(textoSolicitado) == false){
                    textoSolicitado = "";
                    Console.WriteLine($"{mensaje3}\n");
                    imprimirSeparador();
                }
            }while(textoSolicitado == null || textoSolicitado == "");
            return textoSolicitado;
        }
        public string IngresarDatosCliente(string mensaje1, string mensaje2){
            string? textoSolicitado;
            do{
                Console.Write($"{mensaje1}\n>> ");
                textoSolicitado = Console.ReadLine();
                if(textoSolicitado == null || textoSolicitado == ""){
                    Console.WriteLine($"{mensaje2}\n");
                    imprimirSeparador();
                }
            }while(textoSolicitado == null || textoSolicitado == "");
            return textoSolicitado;
        }
        public string IngresarDatosCliente(string mensaje){
            string? textoSolicitado;
            Console.Write($"{mensaje}\n>> ");
                textoSolicitado = Console.ReadLine();
                if(textoSolicitado == null || textoSolicitado == ""){
                    imprimirSeparador();
                    return "-";
                }
            return textoSolicitado;
        }
        public void imprimirSeparador(){
            Console.WriteLine("----------------------------------------------------------------");
        }
        public void ObtenerDatosCliente(Cliente cliente){
            // Nombre del cliente
            List<char> caracteresAdicionales = new List<char> {' ', 'Ñ', 'ñ'};
            imprimirSeparador();
            string nombreCliente = IngresarDatosCliente(c => char.IsLetter(c) || caracteresAdicionales.Contains(c), 
                                                        "Ingrese su nombre.", 
                                                        "Por favor, escriba su nombre.", 
                                                        "Su nombre no puede tener números o símbolos.");
            imprimirSeparador();                                            
            string edadCliente = IngresarDatosCliente(c => char.IsNumber(c) == true,
                                                    "Ingrese su edad.",
                                                    "Por favor, escriba su edad.",
                                                    "Tiene que ser un número.");
            imprimirSeparador();
            string telefonoCliente = IngresarDatosCliente(@"^(\+56)?9\d{8}$",
                                                    "Ingrese su telefono. Formato: +569-------- o  9--------",
                                                    "Por favor, escriba su telefono.",
                                                    "Escriba bien su RUT. Formato: +569-------- o  9--------");
            imprimirSeparador();
            string direccionCliente = IngresarDatosCliente("Ingrese su dirección.",
                                                         "Por favor, ingrese su dirección.");
            //

            cliente.Nombre = nombreCliente;
            cliente.Edad = Convert.ToInt32(edadCliente);
            cliente.Direccion = direccionCliente;
            cliente.Telefono = telefonoCliente;

            imprimirSeparador();
            return;
        }

        // Métodos Complementarios

        public bool Si_O_No(){
            string? respuesta = "";
            Console.WriteLine("Responda S (Si) o  N (No).\n");
            while(true){
                Console.Write("(Si o No) >> ");
                respuesta = Console.ReadLine();
                if (respuesta == "" || respuesta == null)
                {
                    Console.WriteLine("Por favor, responda S (Si) o  N (No).\n");
                    continue;
                }
                respuesta.TrimStart(' ').TrimEnd(' ');
                respuesta = respuesta.ToLower();
                if (respuesta == "s")
                {
                    return true;
                }
                else if (respuesta == "n")
                {
                    return false;
                }
            }
        }
        public bool ConfirmarProducto(Productos producto){
            bool confirma;
            string tipo = "";
            if (producto is Pelicula) tipo = "Película";
            if (producto is Juego) tipo = "Juego";
            Console.WriteLine($"Seleccionó un producto tipo {tipo}, con el título {producto.Nombre}.\n");
            Console.WriteLine($"¿Estás seguro de tu elección?");
            confirma = Si_O_No();
            imprimirSeparador();
            return confirma;
        }

        public List<Productos> ElegirProductosCarrito(Dictionary<string, Productos> inventario){
            List<Productos> carrito = new List<Productos>();
            string? codigoIngresado = "";
            bool seguirConElCarrito = true;
            Console.WriteLine("Ingrese el código de algún producto.\n");
            Console.WriteLine("PP--- : Película");
            Console.WriteLine("PV--- : Videojuego\n");
            while (seguirConElCarrito){
                imprimirSeparador();
                Console.Write("(Ingrese algún código de producto al carrito) >> ");
                codigoIngresado = Console.ReadLine();
                if (codigoIngresado == "" || codigoIngresado == null)
                {
                    Console.WriteLine("Por favor, ingrese a algo.");
                    continue;
                }
                if (inventario.ContainsKey(codigoIngresado))
                {
                    imprimirSeparador();
                    bool confirmacion = ConfirmarProducto(inventario[codigoIngresado]);
                    if(confirmacion){
                        if(inventario[codigoIngresado].Copias <= 0){
                            Console.WriteLine("Lo siento, no tenemos copias de ese producto.\n");
                        }else{
                            Console.WriteLine("Perfecto, añadido al carrito.\n");
                            carrito.Add(inventario[codigoIngresado]);
                        }
                    }
                }else{
                    Console.WriteLine("No se ha encontrado un título con ese código de producto.\n");
                    continue;
                }

                Console.WriteLine("Necesitas algo más?");
                bool algoMas = Si_O_No();

                seguirConElCarrito = algoMas;
            }
            return carrito;
        }

        public int ImprimirPrecioFinal(List<Productos> carrito){
            int total = 0;
            imprimirSeparador();
            Console.WriteLine("Excelente, esto es lo que tienes en el carrito: \n");
            foreach(var producto in carrito){
                Console.WriteLine($"{producto.Codigo, -7}{producto.Nombre, -20}$ 6500");
                total += 6500;
            }
            imprimirSeparador();
            Console.WriteLine($"Total: $ {total}");
            return total;
        }

        public void ArrendarProducto(Dictionary<string, Productos> inventario, Cliente cliente) {
            List<Productos> carrito = ElegirProductosCarrito(inventario);
            int total = ImprimirPrecioFinal(carrito);
            IMetodoPago metodoUsado = ManejarTipoPago();
            int vuelto = 0;
            if(metodoUsado is Efectivo) vuelto = ManejarEfectivo(total);
            Boleta boleta = new Boleta();
            Dictionary<string, Arriendo> arriendos = new Dictionary<string, Arriendo>();
            string ultimoCodigo = arriendos.Keys.Last().Replace("A", "");
            int proximoCódigo;
            if(ultimoCodigo == null || ultimoCodigo == "") proximoCódigo = 0;
            try{
                proximoCódigo = Convert.ToInt32(ultimoCodigo) + 1;
            } catch (FormatException){
                proximoCódigo = 0;
            }
            boleta.ImprimirBoleta(cliente, carrito, total, true, proximoCódigo);
            //cliente.Pagar(total, metodoUsado);
        }

        public void ComprarProducto(Dictionary<string, Productos> inventario, Cliente cliente){
            imprimirSeparador();
            List<Productos> carrito = ElegirProductosCarrito(inventario);
            int total = ImprimirPrecioFinal(carrito);
            IMetodoPago metodoUsado = ManejarTipoPago();
            int vuelto = 0;
            if(metodoUsado is Efectivo) vuelto = ManejarEfectivo(total);
            Boleta boleta = new Boleta();
            boleta.ImprimirBoleta(cliente, carrito, total, false);
        }

        public Dictionary<string, Arriendo> LeerArriendos(){
            string? linea;
            Dictionary<string, Arriendo> arriendos = new Dictionary<string, Arriendo>();
            
            try{
                StreamReader sr = new StreamReader(".\\data\\RegistroArriendos.txt");
                linea = sr.ReadLine();
                
                while (linea != null){
                    string[] datos;
                    datos = linea.Split(";");

                    if (datos.Length == 4){
                        //Arriendo arriendo = new Arriendo();
                        //arriendos..Add(datos[0],arriendo));
                    }
                    linea = sr.ReadLine();
                }
                sr.Close();
            }catch{

            }




            return arriendos;
        }

        public void DevolverProducto(Dictionary<string, Productos> inventario, Dictionary<string, Arriendo> arriendos){
            
        }




        // Métodos de Pago

         public IMetodoPago ManejarTipoPago(){
            Console.WriteLine("¿Cómo cancela?");

            Console.WriteLine("[0] - Efectivo");
            Console.WriteLine("[1] - Débito");
            Console.WriteLine("[2] - Crédito");
            int opcionPago = -1;
            while(opcionPago >= 3 || opcionPago < 0){
                Console.Write(">> ");
                try{
                    opcionPago = Convert.ToInt32(Console.ReadLine());
                    if(opcionPago < 0 || opcionPago >= 4){
                        Console.WriteLine("Error!, elija una de las opciones que se le mostraron: ");
                        continue;
                    }
                    switch(opcionPago){
                        case 0:
                            IMetodoPago efectivo = new Efectivo();
                            return efectivo;
                        case 1:
                            IMetodoPago debito = new Debito();
                            return debito;
                        case 2: 
                            IMetodoPago credito = new Credito();
                            return credito;
                        default:
                            break;
                    }
                }
                catch (FormatException){
                    Console.WriteLine("Error!, Ingrese un número");
                }
            }
            return new Efectivo();      // Retorna el metodo de efectivo por defecto. (En rara ocasión pasará por este return)
        }

        public int ManejarEfectivo(int monto){
            imprimirSeparador();
            int montoDado = 0;
            do{
                Console.Write("Ingrese su cantidad en efectivo: $ ");
                try{
                    montoDado = Convert.ToInt32(Console.ReadLine());
                    if(montoDado < monto){
                        Console.WriteLine("Eso es menos de lo que debe pagar.");
                        imprimirSeparador();
                        continue;
                    }
                }catch(FormatException){
                    Console.WriteLine("Debe ingresar un número entero");
                    imprimirSeparador();
                    continue;
                }
            } while(montoDado < monto);
            return montoDado - monto;
        } 
    }
}
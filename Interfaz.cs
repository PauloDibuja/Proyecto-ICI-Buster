
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
        public void ImprimirPagina(List<Productos> productos, int pagina_actual){
            Console.Clear();
            int index_elemento_inicial = pagina_actual * 4;
            int index_elemento_final = Math.Min(index_elemento_inicial + 4, productos.Count);

            for (int i = index_elemento_inicial; i < index_elemento_final; i++)
            {
                Productos producto = productos[i];
                producto.ImprimirDetalles();
            }
            Console.WriteLine("----------------------------");
        }
        public void Enpaginado(Inventario inventario){
            int pagina_actual = 0;
            int cantidad_paginas = inventario.ListaProductos.Count % 4 == 0 ? inventario.ListaProductos.Count / 4 : (inventario.ListaProductos.Count / 4) + 1;
            List<Productos> listaParaMenu = new List<Productos>();
            ImprimirPagina(inventario.ListaProductos.Values.ToList(), pagina_actual);
            while (true)
            {
                Console.WriteLine("Utilice las flechas <- y -> para desplazarse en el inventario. Presione Esc para salir.");
                ConsoleKeyInfo tecla = Console.ReadKey(true);
                if (tecla.Key == ConsoleKey.RightArrow)
                {
                    pagina_actual++;
                    pagina_actual = Math.Clamp(pagina_actual, 0, cantidad_paginas - 1);
                    ImprimirPagina(inventario.ListaProductos.Values.ToList(), pagina_actual);
                }
                else if (tecla.Key == ConsoleKey.LeftArrow)
                {
                    pagina_actual--;
                    pagina_actual = Math.Clamp(pagina_actual, 0, cantidad_paginas - 1);
                    ImprimirPagina(inventario.ListaProductos.Values.ToList(), pagina_actual);
                }
                else if (tecla.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }
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
        
        // Sobrecarga de Métodos (+ Función lambda)
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
        // Fin Sobrecarga de Métodos
        
        
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
                                                    "Escriba bien su telefono. Formato: +569-------- o  9--------");
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
        public List<Productos> ElegirProductosCarrito(Inventario inventario){
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
                if (inventario.ListaProductos.ContainsKey(codigoIngresado))
                {
                    imprimirSeparador();
                    bool confirmacion = ConfirmarProducto(inventario.ListaProductos[codigoIngresado]);
                    if(confirmacion){
                        if(inventario.ListaProductos[codigoIngresado].Copias <= 0){
                            Console.WriteLine("Lo siento, no tenemos copias de ese producto.\n");
                        }else{
                            Console.WriteLine("Perfecto, añadido al carrito.\n");
                            carrito.Add(inventario.ListaProductos[codigoIngresado]);
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
        public int ImprimirPrecioFinal(List<Productos> carrito, int precio){
            int total = 0;
            imprimirSeparador();
            Console.WriteLine("Excelente, esto es lo que tienes en el carrito: \n");
            foreach(var producto in carrito){
                Console.WriteLine($"{producto.Codigo, -7}{producto.Nombre, -35}$ {precio}");
                total += precio;
            }
            imprimirSeparador();
            Console.WriteLine($"Total: $ {total}");
            return total;
        }
        public void Pago(int total){
            IMetodoPago metodoUsado = ManejarTipoPago();
            int vuelto = 0;
            if(metodoUsado is Efectivo){
                vuelto = ManejarEfectivo(total);
                imprimirSeparador();
                Console.WriteLine($"Excelente, su vuelto es de $ {vuelto}");
            }
        }
        public void PrepararRegistro(List<Productos> carrito, Cliente cliente, string codigoRegistro, Dictionary<string, Arriendo> arriendos){
            List<string> codigosArrendados = new List<string>();
            foreach(var producto in carrito){
                if(producto.Codigo == null || producto.Codigo == "") continue;
                codigosArrendados.Add(producto.Codigo);
            }
            DateTime date = DateTime.Today;
            if(cliente.Nombre == null || cliente.Nombre == "") cliente.Nombre = "No Especificdo";
            Arriendo nuevoArriendo = new Arriendo(cliente.Nombre, string.Join(',', codigosArrendados), date.AddDays(7));
            Console.WriteLine($"Su código para devolver el(los) producto(s) es: {codigoRegistro}");
            Console.WriteLine($"Usted debe devolver el(los) producto(s) el día: {date.AddDays(7).ToString("dd-MM-yyyy")}");
            arriendos.Add(codigoRegistro, nuevoArriendo);
            EscribirArriendos(arriendos);
        }
        public void ProducirBoleta(Dictionary<string, Arriendo> arriendos, bool es_arriendo, Cliente cliente, List<Productos> carrito, int total){
            Boleta boleta = new Boleta();
            int proximoCódigo = -1;
            string codigoString = "";
            if(es_arriendo){
                if(arriendos.Count > 0){
                    string ultimoCodigo = arriendos.Last().Key.Replace("A", "");
                    if(ultimoCodigo == null || ultimoCodigo == "") proximoCódigo = 0;
                    try{
                        proximoCódigo = Convert.ToInt32(ultimoCodigo) + 1;
                    } catch (FormatException){
                        proximoCódigo =0;
                    }
                }else proximoCódigo = 0;
                codigoString = $"A{proximoCódigo.ToString().PadLeft(3, '0')}";
                PrepararRegistro(carrito, cliente, codigoString, arriendos);
            }
            boleta.ImprimirBoleta(cliente, carrito, total, es_arriendo, proximoCódigo);
        }
        public void ArrendarProducto(Inventario inventario, Cliente cliente, int precio) {
            List<Productos> carrito = ElegirProductosCarrito(inventario);
            if(carrito.Count == 0) return;
            int total = ImprimirPrecioFinal(carrito, precio);
            Pago(total);
            Quitar_del_Inventario(inventario, carrito);
            Dictionary<string, Arriendo> arriendos = LeerArriendos();
            ProducirBoleta(arriendos, true, cliente, carrito, total);
    
        }
        public int ConsultarTipo(){
            int tipo = -1;
            Console.WriteLine("¿Qué tipo de producto quiere que le recomiende?\n");
            Console.WriteLine("[0] - Película");
            Console.WriteLine("[1] - Juego\n");
            while(tipo < 0  || tipo > 1){
                Console.Write(">> ");
                try{
                    tipo = Convert.ToInt32(Console.ReadLine());
                }catch(FormatException){
                    Console.WriteLine("Ingrese un número, por favor.");
                }
            }
            return tipo;
        }
        public List<Juego> JuegosElegidos(Inventario inv){
            List<Juego> listado = new List<Juego>();
            foreach(var element in inv.ListaProductos){
                if(element.Value is Juego) listado.Add((Juego)element.Value);
            }      
            return listado;
        }
        public List<Pelicula> PeliculasElegidas(Inventario inv){
            List<Pelicula> listado = new List<Pelicula>();
            foreach(var element in inv.ListaProductos){
                if(element.Value is Pelicula) listado.Add((Pelicula)element.Value);
            }      
            return listado;
        }
        public void RecomendarProducto(Inventario inventario){
            // Preguntar el tipo de producto
            Random random = new Random();
            int valorRandom;
            int tipo = ConsultarTipo();
            List<Pelicula> peliculas;
            List<Juego> juegos;
            if(tipo == 0){
                peliculas = PeliculasElegidas(inventario);
                valorRandom = random.Next(peliculas.Count);
                Console.WriteLine($"Pues, yo le recomiendo la película {peliculas[valorRandom].Nombre}, del año {peliculas[valorRandom].Anio}. A lo mejor le interesa.");
            }
            else if(tipo == 1){
                juegos = JuegosElegidos(inventario);
                valorRandom = random.Next(juegos.Count);
                Console.WriteLine($"Pues, yo le recomiendo un videojuego llamado {juegos[valorRandom].Nombre}, está para {juegos[valorRandom].Plataforma}. A lo mejor le interesa.");
            }
            return;

        }
        public void ComprarProducto(Inventario inventario, Cliente cliente, int precio){
            imprimirSeparador();
            List<Productos> carrito = ElegirProductosCarrito(inventario);
            if(carrito.Count == 0) return;
            int total = ImprimirPrecioFinal(carrito, precio);
            Pago(total);
            imprimirSeparador();
            Quitar_del_Inventario(inventario, carrito);
            Boleta boleta = new Boleta();
            boleta.ImprimirBoleta(cliente, carrito, total, false);
            Console.WriteLine("Ha concretado su compra con éxito.");
            imprimirSeparador();
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
                        Arriendo arriendo = new Arriendo(datos[1], datos[2], DateTime.Parse(datos[3]));
                        arriendos.Add(datos[0], arriendo);
                    }
                    linea = sr.ReadLine();
                }
                sr.Close();
            }catch{

            }
            return arriendos;
        }
        public void EscribirArriendos(Dictionary<string, Arriendo> arriendos){
            using(StreamWriter registro = new StreamWriter(".\\data\\RegistroArriendos.txt")){
                foreach(var linea_registro in arriendos){
                    Arriendo arriendo = linea_registro.Value;
                    registro.WriteLine($"{linea_registro.Key};{arriendo.Nombre};{string.Join(',', arriendo.Arriendos)};{arriendo.FechaArriendo}");
                }
            }
        }
        
        
        // Aplicamos sobrecarga de operadores aquí (Implementación en clase Inventario)
        public void Devolver_a_Inventario(Inventario inventario, Arriendo arriendo){
            foreach(var codigo in arriendo.Arriendos){
                // Aplicacion de la sobrecarga de operador 
                inventario += codigo;
            }
        }
        public void Quitar_del_Inventario(Inventario inventario, List<Productos> carrito){
            foreach(var producto in carrito){
                if(producto.Codigo == null || producto.Codigo == "") continue;
                // Aplicacion de la sobrecarga de operador 
                inventario -= producto.Codigo;
            }
        }
        // Fin Aplicacion de la sobrecarga de operador 


        public void RespuestaAnteFecha(Arriendo arriendo){
            imprimirSeparador();
            DateTime fecha = DateTime.Today.Date;
            if(arriendo.FechaArriendo == null) return;
            DateTime devolucion = DateTime.Parse(arriendo.FechaArriendo).Date;
            if (fecha < devolucion){
                Console.WriteLine("OHHH, lo entregaste antes. Muy bien.");
            }else if(fecha == devolucion){
                Console.WriteLine("Excelente, lo entrego a tiempo. Muy bien.");
            }else{
                int dias_diferencia = fecha.Subtract(devolucion).Days;
                Console.WriteLine($"Oiga, lo acaba de entregar con {dias_diferencia} días de diferencia. Le vamos a poner una multa.");
                Console.WriteLine($"Serían $ 500 x día = $ {dias_diferencia * 500}");
                Pago(dias_diferencia * 500);
                Console.WriteLine("No lo vuelva a hacer. Sea más resposable.");
            }
            imprimirSeparador();
        }
        public void DevolverProducto(Inventario inventario, Cliente cliente){
            Dictionary<string, Arriendo> arriendos = LeerArriendos();
            string? codigoDevolver;
            while (true){
                try{
                    Console.Write("Ingrese su codigo para devolver el/los productos\n\n>> ");
                    codigoDevolver = Console.ReadLine();

                    if (codigoDevolver == "" || codigoDevolver == null){
                        Console.WriteLine("Ingrese bien el codigo!");
                        continue;
                    }
                    if (arriendos.ContainsKey(codigoDevolver)){
                        Devolver_a_Inventario(inventario, arriendos[codigoDevolver]);
                        RespuestaAnteFecha(arriendos[codigoDevolver]);
                        arriendos.Remove(codigoDevolver);
                        Console.WriteLine("Productos devueltos correctamente!");
                        break;
                    }
                }finally{
                }
            }
            EscribirArriendos(arriendos);
        }
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
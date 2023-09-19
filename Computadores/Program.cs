using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace ConsoleAppTiendaPerfumes
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "server=localhost;user=root;password=;database=computadordb;";
            MySqlConnection connection = new MySqlConnection(connectionString);

            int numeroComputadores;
            string cedula, nombre_computador, apellido_computador, nombre, marca;
            double precio;
            double totalVentas = 0;

            Console.WriteLine("Digite el número de computadores: ");
            numeroComputadores = Int32.Parse(Console.ReadLine());

            for (int i = 0; i < numeroComputadores; i++)
            {

                Console.WriteLine("Digite la cedula del empleado: ");
                cedula = Console.ReadLine();
                Console.WriteLine("Digite el nombre del empleado: ");
                nombre_computador = Console.ReadLine();
                Console.WriteLine("Digite el apellido del empleado: ");
                apellido_computador = Console.ReadLine();
                Console.WriteLine("Digite el nombre del computador: ");
                nombre = Console.ReadLine();
                Console.WriteLine("Digite la marca del computador: ");
                marca = Console.ReadLine();
                Console.WriteLine("Digite el precio del computador: ");
                precio = Double.Parse(Console.ReadLine());

                string insertQuery = $"INSERT INTO computadores (cedula, nombre_computador, apellido_computador,nombre, marca, precio) VALUES ('{cedula}', '{nombre_computador}', '{apellido_computador}','{nombre}', '{marca}', {precio})";

                MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                connection.Open();
                insertCommand.ExecuteNonQuery();
                connection.Close();
            }

            // Menú de opciones
            while (true)
            {
                Console.WriteLine("\nMENU DE OPCIONES:");
                Console.WriteLine("1. Actualizar computador");
                Console.WriteLine("2. Eliminar computador");
                Console.WriteLine("3. Listar computadores");
                Console.WriteLine("4. Calcular total de ventas");
                Console.WriteLine("5. Salir");
                Console.Write("Seleccione una opción: ");
                int opcion = Int32.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        Console.Write("Digite la cedula del empleado a actualizar: ");
                        string cedulaActualizar = Console.ReadLine();
                        Console.Write("Nuevo Nombre: ");
                        string nuevoNombre_computador = Console.ReadLine();
                        Console.Write("Nuevo Apellido: ");
                        string nuevoApellido_computador = Console.ReadLine();
                        Console.Write("Digite el nombre del computador a actualizar: ");
                        string perfumeActualizar = Console.ReadLine();
                        Console.Write("Nueva Marca: ");
                        string nuevaMarca = Console.ReadLine();
                        Console.Write("Nuevo Precio: ");
                        double nuevoPrecio = Double.Parse(Console.ReadLine());
                        ActualizarComputador(cedulaActualizar, nuevoNombre_computador, nuevoApellido_computador, perfumeActualizar, nuevaMarca, nuevoPrecio, connection);
                        break;
                    case 2:
                        Console.Write("Digite el nombre del computador a eliminar: ");
                        string computadorEliminar = Console.ReadLine();
                        EliminarComputador(computadorEliminar, connection);
                        break;
                    case 3:
                        ListarComputadores(connection);
                        break;
                    case 4:
                        totalVentas = CalcularTotalVentas(connection);
                        Console.WriteLine("El total de ventas es: " + totalVentas);
                        break;
                    case 5:
                        Console.WriteLine("Saliendo del programa.");
                        return;
                    default:
                        Console.WriteLine("Opción no válida. Seleccione una opción válida del menú.");
                        break;
                }
            }
        }

        // Función para actualizar un computador por su nombre
        static void ActualizarComputador(string cedula, string nuevoNombre_computador, string nuevoApellido_computador, string nombre, string nuevaMarca, double nuevoPrecio, MySqlConnection connection)
        {
            string updateQuery = $"UPDATE computadores SET nombre_computador = '{nuevoNombre_computador}', apellido_computador = '{nuevoApellido_computador}',marca = '{nuevaMarca}', precio = {nuevoPrecio} WHERE nombre = '{nombre}'";

            MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
            connection.Open();
            updateCommand.ExecuteNonQuery();
            connection.Close();

            Console.WriteLine("Perfume actualizado exitosamente.");
        }

        // Función para eliminar un computador por su nombre
        static void EliminarComputador(string nombre_computador, MySqlConnection connection)
        {
            string deleteQuery = $"DELETE FROM computadores WHERE nombre_computador = '{nombre_computador}'";

            MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
            connection.Open();
            deleteCommand.ExecuteNonQuery();
            connection.Close();

            Console.WriteLine("Computador eliminado exitosamente.");
        }

        // Función para calcular el total de ventas
        static double CalcularTotalVentas(MySqlConnection connection)
        {
            string selectQuery = "SELECT precio FROM computadores";
            MySqlCommand command = new MySqlCommand(selectQuery, connection);
            connection.Open();

            double totalVentas = 0;
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    double precio = reader.GetDouble("precio");
                    totalVentas += precio;
                }
            }

            connection.Close();
            return totalVentas;
        }

        // Función para listar los computadores
        static void ListarComputadores(MySqlConnection connection)
        {
            string selectQuery = "SELECT nombre, marca, precio FROM computadores";
            MySqlCommand command = new MySqlCommand(selectQuery, connection);
            connection.Open();

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                Console.WriteLine("Lista de computadores:");
                while (reader.Read())
                {
                    string cedula = reader.GetString("cedula");
                    string nombre_computador = reader.GetString("nombre");
                    string apellido_computador = reader.GetString("apellido");
                    string nombre = reader.GetString("nombre");
                    string marca = reader.GetString("marca");
                    double precio = reader.GetDouble("precio");
                    Console.WriteLine($"Cédula: {cedula}, Nombre_computador: {nombre_computador}, Apellido_computador: {apellido_computador},Nombre: {nombre}, Marca: {marca}, Precio: {precio}");
                }
            }

            connection.Close();
        }
    }
}

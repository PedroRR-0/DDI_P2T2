using aeropuerto;
using aeropuerto.Modelo;
using aeropuerto.Vista;
using MySqlConnector;
namespace TestAeropuerto1
{
    // Comprueba que el método obtenerConexion() de la clase Conexion
    // devuelve una conexión válida
    public class UnitTest1:IDisposable
    {
        // Declaramos conexión de prueba y clase Conexion
        private readonly MySqlConnection conexionPrueba;
        private Conexion instanciaConexion;
        public UnitTest1() {
            // Obtenemos la instancia de Conexion y abrimos la conexion de prueba
            instanciaConexion = Conexion.getInstance();
            string cadenaConexion = "Server=localhost;Database=aeropuertocharp;User ID=rot;Password=root";
            conexionPrueba = new MySqlConnection(cadenaConexion);
            conexionPrueba.Open();
        }

        // Método para limpiar los objetos creados por el test
        public void Dispose()
        {
            conexionPrueba.Close();
            conexionPrueba?.Dispose();
            instanciaConexion.cerrarConexion();
        }


        [Fact(DisplayName = "obtenerConexíon() devuelve conexión válida")]
        public void TestBD()
        {
            // Comparamos los parámetros de la conexión devuelta y la de prueba
            MySqlConnection conexionMetodo = instanciaConexion.obtenerConexion();
            Assert.Equal(conexionPrueba.ConnectionString, conexionMetodo.ConnectionString);
            Assert.Equal(conexionPrueba.State, conexionMetodo.State);

        }
    }

    // Comprueba que el método ObtenerAvionesConEstado(1)
    // devuelve la lista de aviones operativos
    public class UnitTest2 : IDisposable
    {
        // Declaramos la lista de prueba y la devuelta
        private List<int> listAvionesBD = new List<int>();
        private List<int> listAvionesPrueba = new List<int>();
        private Conexion c;
        public UnitTest2()
        {
            // Obtenemos la conexion y añadimos a la lista de prueba los aviones que deben salir
            c = Conexion.getInstance();
            listAvionesPrueba.Add(1);
            listAvionesPrueba.Add(2);
        }

        // Método para limpiar los objetos creados por el test
        public void Dispose()
        {   
            listAvionesPrueba.Clear();
            listAvionesBD.Clear();
        }

        
        [Fact(DisplayName = "Listar aviones operativos")]
        public void TestListaOperativos()
        {
            // Comparamos la lista de aviones que devuelve el método con la de prueba
            listAvionesBD = c.ObtenerAvionesConEstado(1);
            Assert.Equal(listAvionesPrueba, listAvionesBD);

        }
    }
}
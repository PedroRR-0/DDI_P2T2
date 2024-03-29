using aeropuerto;
using aeropuerto.Modelo;
using aeropuerto.Vista;
using MySqlConnector;
namespace TestAeropuerto1
{
    // Comprueba que el m�todo obtenerConexion() de la clase Conexion
    // devuelve una conexi�n v�lida
    public class UnitTest1:IDisposable
    {
        // Declaramos conexi�n de prueba y clase Conexion
        private readonly MySqlConnection conexionPrueba;
        private Conexion instanciaConexion;
        public UnitTest1() {
            // Obtenemos la instancia de Conexion y abrimos la conexion de prueba
            instanciaConexion = Conexion.getInstance();
            string cadenaConexion = "Server=localhost;Database=aeropuertocharp;User ID=rot;Password=root";
            conexionPrueba = new MySqlConnection(cadenaConexion);
            conexionPrueba.Open();
        }

        // M�todo para limpiar los objetos creados por el test
        public void Dispose()
        {
            conexionPrueba.Close();
            conexionPrueba?.Dispose();
            instanciaConexion.cerrarConexion();
        }


        [Fact(DisplayName = "obtenerConex�on() devuelve conexi�n v�lida")]
        public void TestBD()
        {
            // Comparamos los par�metros de la conexi�n devuelta y la de prueba
            MySqlConnection conexionMetodo = instanciaConexion.obtenerConexion();
            Assert.Equal(conexionPrueba.ConnectionString, conexionMetodo.ConnectionString);
            Assert.Equal(conexionPrueba.State, conexionMetodo.State);

        }
    }

    // Comprueba que el m�todo ObtenerAvionesConEstado(1)
    // devuelve la lista de aviones operativos
    public class UnitTest2 : IDisposable
    {
        // Declaramos la lista de prueba y la devuelta
        private List<int> listAvionesBD = new List<int>();
        private List<int> listAvionesPrueba = new List<int>();
        private Conexion c;
        public UnitTest2()
        {
            // Obtenemos la conexion y a�adimos a la lista de prueba los aviones que deben salir
            c = Conexion.getInstance();
            listAvionesPrueba.Add(1);
            listAvionesPrueba.Add(2);
        }

        // M�todo para limpiar los objetos creados por el test
        public void Dispose()
        {   
            listAvionesPrueba.Clear();
            listAvionesBD.Clear();
        }

        
        [Fact(DisplayName = "Listar aviones operativos")]
        public void TestListaOperativos()
        {
            // Comparamos la lista de aviones que devuelve el m�todo con la de prueba
            listAvionesBD = c.ObtenerAvionesConEstado(1);
            Assert.Equal(listAvionesPrueba, listAvionesBD);

        }
    }
}
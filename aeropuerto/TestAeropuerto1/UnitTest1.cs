using aeropuerto;
using aeropuerto.Modelo;
using aeropuerto.Vista;
using MySqlConnector;
namespace TestAeropuerto1
{
    public class UnitTest1:IDisposable
    {
        private readonly MySqlConnection conexionPrueba;
        private Conexion instanciaConexion;
        public UnitTest1() {
            instanciaConexion = Conexion.getInstance();
            string cadenaConexion = "Server=localhost;Database=aeropuertocharp;User ID=root;Password=root";
            conexionPrueba = new MySqlConnection(cadenaConexion);
            conexionPrueba.Open();
        }
        public void Dispose()
        {
            conexionPrueba.Close();
            conexionPrueba?.Dispose();
            instanciaConexion.cerrarConexion();
        }

        [Fact(DisplayName = "obtenerConexíon() devuelve conexión válida")]
        public void TestBD()
        {
            MySqlConnection conexionMetodo = instanciaConexion.obtenerConexion();
            Assert.Equal(conexionPrueba.ConnectionString, conexionMetodo.ConnectionString);
            Assert.Equal(conexionPrueba.State, conexionMetodo.State);

        }
    }

    public class UnitTest2 : IDisposable
    {
        private List<int> listAvionesBD = new List<int>();
        private List<int> listAvionesPrueba = new List<int>();
        private Conexion c;
        public UnitTest2()
        {
            c = Conexion.getInstance();
            listAvionesPrueba.Add(1);
            listAvionesPrueba.Add(2);
        }

        public void Dispose()
        {   
            listAvionesPrueba.Clear();
            listAvionesBD.Clear();
        }

        [Fact(DisplayName = "Listar aviones operativos")]
        public void TestListaOperativos()
        {
            listAvionesBD = c.ObtenerAvionesConEstado(1);
            Assert.Equal(listAvionesPrueba, listAvionesBD);

        }
    }
}
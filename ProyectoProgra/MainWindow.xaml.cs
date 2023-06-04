using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace ProyectoProgra
{
    public partial class MainWindow : Window
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private DataTable inventarioTable;
        private DataRowView selectedRow;

        public MainWindow()
        {
            InitializeComponent();
            LoadInventario();
        }

        private SqlConnection CrearConexion()
        {
            return new SqlConnection(connectionString);
        }

        private void LoadInventario()
        {
            try
            {
                using (SqlConnection connection = CrearConexion())
                {
                    string query = "SELECT ID, Producto, Cantidad, Precio, Descripcion FROM Inventario";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    inventarioTable = new DataTable();
                    adapter.Fill(inventarioTable);

                    InventarioDataGrid.ItemsSource = inventarioTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el inventario: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void InventarioDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (InventarioDataGrid.SelectedItem != null)
            {
                selectedRow = (DataRowView)InventarioDataGrid.SelectedItem;

                TxtIDValue.Text = selectedRow["ID"].ToString();
                TxtProducto.Text = selectedRow["Producto"].ToString();
                TxtCantidad.Text = selectedRow["Cantidad"].ToString();
                TxtPrecio.Text = selectedRow["Precio"].ToString();
                TxtDescripcion.Text = selectedRow["Descripcion"].ToString();
            }
        }

        private void BtnActualizar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selectedRow != null)
                {
                    using (SqlConnection connection = CrearConexion())
                    {
                        string query = "UPDATE Inventario SET Producto = @producto, Cantidad = @cantidad, Precio = @precio, Descripcion = @descripcion WHERE ID = @id";

                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@producto", TxtProducto.Text);

                        int cantidad;
                        if (int.TryParse(TxtCantidad.Text, out cantidad))
                            command.Parameters.AddWithValue("@cantidad", cantidad);
                        else
                        {
                            MessageBox.Show("La cantidad debe ser un valor numérico.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        decimal precio;
                        if (decimal.TryParse(TxtPrecio.Text, out precio))
                            command.Parameters.AddWithValue("@precio", precio);
                        else
                        {
                            MessageBox.Show("El precio debe ser un valor numérico.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        command.Parameters.AddWithValue("@descripcion", TxtDescripcion.Text);

                        int id = (int)selectedRow["ID"];
                        command.Parameters.AddWithValue("@id", id);

                        connection.Open();
                        command.ExecuteNonQuery();
                        LoadInventario();
                        ClearInputs();
                    }
                }
                else
                {
                    MessageBox.Show("No se ha seleccionado ningún producto del inventario.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar los datos: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selectedRow != null)
                {
                    MessageBoxResult result = MessageBox.Show("¿Está seguro de que desea eliminar este producto del inventario?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        using (SqlConnection connection = CrearConexion())
                        {
                            string query = "DELETE FROM Inventario WHERE ID = @id";
                            SqlCommand command = new SqlCommand(query, connection);
                            int id = (int)selectedRow["ID"];
                            command.Parameters.AddWithValue("@id", id);

                            connection.Open();
                            command.ExecuteNonQuery();

                            LoadInventario();
                            ClearInputs();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No se ha seleccionado ningún producto del inventario.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el producto del inventario: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Chequeo de que si el producto ya existe
                string producto = TxtProducto.Text.Trim();
                if (InventarioContainsProduct(producto))
                {
                    MessageBox.Show("El producto ya existe en el inventario.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                // Aquí se avisa que el precio y cantidad sea un número entero
                using (SqlConnection connection = CrearConexion())
                {
                    string query = "INSERT INTO Inventario (Producto, Cantidad, Precio, Descripcion) VALUES (@producto, @cantidad, @precio, @descripcion)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@producto", producto);

                    int cantidad;
                    if (int.TryParse(TxtCantidad.Text, out cantidad))
                        command.Parameters.AddWithValue("@cantidad", cantidad);
                    else
                    {
                        MessageBox.Show("La cantidad debe ser un número y no texto.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    decimal precio;
                    if (decimal.TryParse(TxtPrecio.Text, out precio))
                        command.Parameters.AddWithValue("@precio", precio);
                    else
                    {
                        MessageBox.Show("El precio debe ser un número y no texto.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    command.Parameters.AddWithValue("@descripcion", TxtDescripcion.Text);

                    connection.Open();
                    command.ExecuteNonQuery();

                    LoadInventario();
                    ClearInputs();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el producto al inventario: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool InventarioContainsProduct(string producto)
        {
            foreach (DataRow row in inventarioTable.Rows)
            {
                if (string.Equals(row["Producto"].ToString(), producto, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        private void ClearInputs()
        {
            TxtIDValue.Text = "";
            TxtProducto.Text = "";
            TxtCantidad.Text = "";
            TxtPrecio.Text = "";
            TxtDescripcion.Text = "";
            selectedRow = null;
        }
    }
}


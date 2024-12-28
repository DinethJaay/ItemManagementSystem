using Microsoft.Data.SqlClient;
using System.Data;

namespace ItemManagement
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var connectionString = "Server=MSI\\SQLEXPRESS;Database=ItemManagement;Trusted_Connection=True;TrustServerCertificate=True;";

            var query = "INSERT INTO Item(name,price) Values(@name,@price)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    //MessageBox.Show("Database connected Successfully");
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@name", txtName.Text);
                        cmd.Parameters.AddWithValue("@price", Convert.ToDecimal(txtPrice.Text));

                        int rowsAffected = cmd.ExecuteNonQuery();
                        MessageBox.Show(rowsAffected > 0 ? "Item Added Successfully !" : "Failed to add item.");
                        DisplayItem();
                        ClearValues();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void itemGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DisplayItem();
        }
        private void DisplayItem()
        {
            string connectionString = "Server=MSI\\SQLEXPRESS;Database=ItemManagement;Trusted_Connection=True;TrustServerCertificate=True;";

            string query = "Select * from Item";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    itemGridView.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearValues();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void ClearValues()
        {
            txtName.Text = "";
            txtPrice.Text = "";
            txtId.Text = "";
        }

        private void itemGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //Get Selected Row
                DataGridViewRow selectedRow = itemGridView.Rows[e.RowIndex];

                //Display selected rows Data in textbox
                txtId.Text = selectedRow.Cells["Id"].Value.ToString();
                txtName.Text = selectedRow.Cells["Name"].Value.ToString();
                txtPrice.Text = selectedRow.Cells["Price"].Value.ToString();



            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(txtId.Text);
            var query = "UPDATE Item SET name=@name, price=@price WHERE Id=@id";
            var connectionString = "Server=MSI\\SQLEXPRESS;Database=ItemManagement;Trusted_Connection=True;TrustServerCertificate=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@name", txtName.Text);
                        cmd.Parameters.AddWithValue("@price", Convert.ToDecimal(txtPrice.Text));

                        int rowsAffected = cmd.ExecuteNonQuery();
                        MessageBox.Show(rowsAffected > 0 ? "Item Updated Successfully !" : "Failed to update item.");
                        DisplayItem();
                        ClearValues();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var connectionString = "Server=MSI\\SQLEXPRESS;Database=ItemManagement;Trusted_Connection=True;TrustServerCertificate=True;";

            var SelectedItem = txtId.Text;
            if (SelectedItem == "")
            {
                MessageBox.Show("Please select an item to delete");
                return;
            }
            else
            {
                var query = "DELETE FROM Item WHERE Id=@id";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        using (SqlCommand cmd = new SqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@id", Convert.ToInt32(SelectedItem));

                            int rowsAffected = cmd.ExecuteNonQuery();
                            MessageBox.Show(rowsAffected > 0 ? "Item Deleted Successfully !" : "Failed to delete item.");
                            DisplayItem();
                            ClearValues();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TVMazeClient.Models;
using TVMazeClient.Services;

namespace LW3_Task5_MiA
{
    public partial class Form1 : Form
    {
        private readonly ApiClient _apiClient;
        private List<Show> _shows;

        public Form1()
        {
            InitializeComponent();
            _apiClient = new ApiClient();
        }

        // Завантаження шоу по запиту
        private async void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                button1.Enabled = false;
                label1.Text = "Loading...";
                // Отримуємо шоу за допомогою API
                _shows = await _apiClient.GetShowsAsync(textBox1.Text);
                // Відображення шоу в DataGridView
                dataGridView1.DataSource = _shows;
                // Налаштування розтягування колонок
                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                // Якщо хочете вручну налаштувати ширину колонок, можна зробити так:
                dataGridView1.Columns["Id"].Width = 50;
                dataGridView1.Columns["Name"].Width = 200;
                dataGridView1.Columns["Language"].Width = 100;
                dataGridView1.Columns["Premiered"].Width = 120;
                dataGridView1.Columns["Summary"].Width = 270;
                // Для забезпечення гарного вигляду колонок можна додати мінімальну ширину
                dataGridView1.Columns["Id"].MinimumWidth = 50;
                dataGridView1.Columns["Name"].MinimumWidth = 100;
                dataGridView1.Columns["Language"].MinimumWidth = 80;
                dataGridView1.Columns["Premiered"].MinimumWidth = 100;
                dataGridView1.Columns["Summary"].MinimumWidth = 150;
                label1.Text = $"{_shows.Count} shows loaded.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                button1.Enabled = true;
            }
        }
        // Обробка кліку по шоу для отримання детальної інформації
        private async void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int id = (int)dataGridView1.Rows[e.RowIndex].Cells["Id"].Value;
                var showDetails = await _apiClient.GetShowDetailsAsync(id);
                // Відображення деталей шоу
                label2.Text = showDetails.Name;
                label3.Text = showDetails.Summary;
                label4.Text = showDetails.Language;
                label5.Text = showDetails.Premiered;
            }
        }
        // Оновлення списку шоу
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            btnLoad_Click(sender, e);
        }
    }
}
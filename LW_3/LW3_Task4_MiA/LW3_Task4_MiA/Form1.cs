using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DogGalleryApp
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient _http = new HttpClient();
        private List<string> dogImages = new List<string>();
        private int currentIndex = 0;

        public Form1()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;  // Переконайся, що pictureBox1 — це правильне ім'я елемента

            // Задаємо початковий URL
            textBox1.Text = "https://dog.ceo/api/breeds/image/random";
        }

        // Метод для завантаження зображень
        private async Task LoadImages(int count)
        {
            try
            {
                string url = $"https://dog.ceo/api/breeds/image/random/{count}";
                var json = await _http.GetStringAsync(url);
                var dogResponse = JsonConvert.DeserializeObject<DogResponse>(json);

                // Вивести отриманий JSON у textBox2
                textBox2.Text = JsonConvert.SerializeObject(dogResponse, Formatting.Indented);  // Форматуємо JSON для читабельності

                if (dogResponse != null && dogResponse.message.Count > 0)
                {
                    dogImages = dogResponse.message;
                    currentIndex = 0; // Починаємо з першого зображення
                    DisplayImage();
                }
                else
                {
                    MessageBox.Show("Не вдалося отримати зображення.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Метод для відображення зображення
        private void DisplayImage()
        {
            if (dogImages.Count > 0 && currentIndex >= 0 && currentIndex < dogImages.Count)
            {
                var imageUrl = dogImages[currentIndex];
                var imageBytes = _http.GetByteArrayAsync(imageUrl).Result;
                using (var ms = new MemoryStream(imageBytes))
                {
                    pictureBox1.Image = Image.FromStream(ms);
                }
            }
        }

        // Кнопка для завантаження N зображень
        private async void buttonLoad_Click(object sender, EventArgs e)
        {
            int count;
            // Перевірка, чи введене значення є числом
            if (int.TryParse(textBox3.Text, out count) && count >= 1)
            {
                await LoadImages(count); // Завантажуємо зображення
            }
            else
            {
                MessageBox.Show("Будь ласка, введіть коректне число більше 0.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Кнопка для перегляду попереднього зображення
        private void buttonPrev_Click(object sender, EventArgs e)
        {
            if (dogImages.Count > 0 && currentIndex > 0)
            {
                currentIndex--;
                DisplayImage();
            }
        }

        // Кнопка для перегляду наступного зображення
        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (dogImages.Count > 0 && currentIndex < dogImages.Count - 1)
            {
                currentIndex++;
                DisplayImage();
            }
        }

        // Кнопка для скидання всього
        private void buttonReset_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            pictureBox1.Image = null;
            dogImages.Clear();
            currentIndex = 0; // Скидаємо індекс
            textBox3.Clear(); // Очищаємо TextBox
        }
    }

    // Клас для парсингу відповіді з API
    public class DogResponse
    {
        public List<string> message { get; set; } = new List<string>();
        public string status { get; set; } = string.Empty;
    }
}

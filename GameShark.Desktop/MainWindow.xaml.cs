using System.Windows;
using GameShark.Desktop.Services;

namespace GameShark.Desktop;

public partial class MainWindow : Window
{
    private readonly ApiService _apiService;

    public MainWindow()
    {
        InitializeComponent();
        _apiService = new ApiService();
    }

    private async void btnTestarApi_Click(object sender, RoutedEventArgs e)
    {
        txtStatus.Text = "Conectando à Matriz...";
        txtStatus.Foreground = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#fbd304")); // Amarelo

        // Chama a API de forma assíncrona para não travar a tela
        string resultado = await _apiService.TestarConexaoAsync();

        txtStatus.Text = resultado;

        if (resultado.Contains("SUCESSO"))
        {
            txtStatus.Foreground = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#39ff14")); // Verde Neon
        }
        else
        {
            txtStatus.Foreground = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#ed1c24")); // Vermelho
        }
    }
}
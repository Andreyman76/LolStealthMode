using Avalonia.Controls;
using System.Reflection;

namespace LolStealthMode.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        var attribute = Assembly.GetEntryAssembly()
            ?.GetCustomAttribute<AssemblyFileVersionAttribute>();
        Title = $"LOL Stealth Mode v.{attribute?.Version}";
    }
}
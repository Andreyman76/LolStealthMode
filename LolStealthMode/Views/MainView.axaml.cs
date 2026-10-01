using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using LolStealthMode.Models;
using System.Security.Principal;

namespace LolStealthMode.Views;

public partial class MainView : UserControl
{
    private readonly IBrush _enabledCircleBrush = new LinearGradientBrush
    {
        StartPoint = RelativePoint.Parse("0%,0%"),
        EndPoint = RelativePoint.Parse("100%,100%"),
        GradientStops =
        [
            new GradientStop(Color.Parse("#1FCFCF"), 0),
            new GradientStop(Color.Parse("#00AD96"), 0.5),
            new GradientStop(Color.Parse("#006B5F"), 1)
        ]
    };

    private readonly IBrush _disabledCircleBrush = new LinearGradientBrush
    {
        StartPoint = RelativePoint.Parse("0%,0%"),
        EndPoint = RelativePoint.Parse("100%,100%"),
        GradientStops =
        [
            new GradientStop(Color.Parse("#1B5787"), 0),
            new GradientStop(Color.Parse("#0B2A44"), 0.5),
            new GradientStop(Color.Parse("#0E2C47"), 1)
        ]
    };

    private bool _enabled;
    private readonly LolStealthManager _manager = new();

    public MainView()
    {
        InitializeComponent();

        var hasAdminRules = new WindowsPrincipal(WindowsIdentity.GetCurrent())
            .IsInRole(WindowsBuiltInRole.Administrator);

        if (hasAdminRules)
        {
            SetLabelText();
        }
        else
        {
            Label.Text = "No admin rules";
            Circle.IsEnabled = false;
        }
    }

    private void SetLabelText()
    {
        _enabled = _manager.IsStealthEnabled();

        if (_enabled)
        {
            Label.Text = "Disable";
            Circle.Background = _enabledCircleBrush;
        }
        else
        {
            Label.Text = "Enable";
            Circle.Background = _disabledCircleBrush;
        }
    }

    public void OnClick(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        if (_enabled)
        {
            _manager.DisableStealth();
        }
        else
        {
            _manager.EnableStealth();
        }

        SetLabelText();
    }
}
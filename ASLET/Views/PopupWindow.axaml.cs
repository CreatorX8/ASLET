using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ASLET.Views;

public partial class PopupWindow : Window
{
    public PopupWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
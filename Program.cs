using Gtk;

class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        Application.Init();

        var window = new MainWindow();
        window.ShowAll();

        Application.Run();
    }
}
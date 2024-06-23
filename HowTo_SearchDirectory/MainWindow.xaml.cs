using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HowTo_SearchDirectory;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    delegate void Display(FileInfo fi);
    public MainWindow()
    {
        InitializeComponent();
        Title = @"C:\Users\Username";
    }

    private async void btnSearch_Click(object sender, RoutedEventArgs e)
    {
        string searchText = tbInput.Text;
        string searchDir = Title;
        if (string.IsNullOrEmpty(searchText)) { return; }

        int itemsFound = await Task.Run(()=> search(searchDir, searchText));
        tbInput.Text = $"{itemsFound} items found";
    }

    private int search(string searchDir, string searchText)
    {
        EnumerationOptions options = new EnumerationOptions
        {
            IgnoreInaccessible = true,
            RecurseSubdirectories = true,
            MatchCasing = MatchCasing.CaseInsensitive
        };


        var results = Directory.EnumerateFiles(searchDir, $"*{searchText}*", options);
        foreach ( var result in results )
        {
            Dispatcher.Invoke(()=> lvResults.Items.Add(result));
        }
        return results.Count();
    }

    private void btnClear_Click(object sender, RoutedEventArgs e)
    {
        lvResults.Items.Clear();
    }
}
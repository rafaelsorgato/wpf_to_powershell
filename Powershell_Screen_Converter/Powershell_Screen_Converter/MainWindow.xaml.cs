using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xaml.Permissions;
using System.Text.RegularExpressions;
using System.IO;
using Microsoft.Win32;
using System.Xml;

namespace Powershell_Screen_Converter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string powershellcodefinal = "";
        string powershellcode = @"
[void][System.Reflection.Assembly]::LoadWithPartialName('presentationframework')
                
function minimize_powershell_window {
Add-Type -TypeDefinition @'
using System;
using System.Runtime.InteropServices;

public class User32 {
    [DllImport(""user32.dll"")]
    public static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
}
'@   

$processes = Get-Process | Where-Object { $_.ProcessName -like ""*terminal*"" }

foreach ($process in $processes) {
    [IntPtr]$hwnd = $process.MainWindowHandle
    [User32]::ShowWindowAsync($hwnd, 2) # 2 corresponds to SW_MINIMIZE
}
}       

$input = @'
var_location
'@             

$input = $input -replace '^<Window.*', '<Window' -replace 'mc:Ignorable=""d""','' -replace ""x:N"",'N' 
[xml]$xaml = $input
$xmlreader=(New-Object System.Xml.XmlNodeReader $xaml)
$xamlForm=[Windows.Markup.XamlReader]::Load($xmlreader)

$xaml.SelectNodes(""//*[@Name]"") | ForEach-Object -Process {
    Set-Variable -Name ($_.Name) -Value $xamlForm.FindName($_.Name)
}    

#----------------------------------------------
# code to run before screen opening
#----------------------------------------------
minimize_powershell_window

#----------------------------------------------
# PUT DOWN YOUR INTERACTIONS/FUNCTIONS
#
# examples of interactions/functions:
#
#$button = $xamlForm.FindName(""button"")
#$button.Add_Click({
#    $textbox1.Text = ""Button clicked!""
#})
#$checkbox = $xamlForm.FindName(""checkbox"")
#
#$checkbox.Add_Checked({
#    do something
#})
#
#$listbox = $xamlForm.FindName(""listbox"")
#$listbox.Add_SelectionChanged({
#    $selectedItem = $listbox.SelectedItem
#    $textbox1.Text = ""Selected: $selectedItem""
#})
#----------------------------------------------
$xamlForm.ShowDialog()
";
        public MainWindow()
        {
            InitializeComponent();

            label1.Content = Environment.CurrentDirectory;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            powershellcodefinal = powershellcode.Replace("var_location", textbox1.Text);
            string pattern = @"\bClick\s*=\s*""[^""]*""\s*";
            powershellcodefinal = Regex.Replace(powershellcodefinal, pattern, "", RegexOptions.IgnoreCase);
            pattern = @"\bTextChanged\s*=\s*""[^""]*""\s*";
            powershellcodefinal = Regex.Replace(powershellcodefinal, pattern, "", RegexOptions.IgnoreCase);
            pattern = @"(d:ItemsSource\s*=\s*"")[^""]*(""\s*)";
            powershellcodefinal = Regex.Replace(powershellcodefinal, pattern, "", RegexOptions.IgnoreCase);


            string button = @"(button\s*=\s*"")[^""]*(""\s*)";
            MatchCollection buttonMatches = Regex.Matches(powershellcode, pattern);

            foreach (Match match in buttonMatches)
            {
                string originalContent = match.Groups[1].Value;
                string modifiedContent = "a" + originalContent;

                powershellcode = powershellcode.Replace(originalContent, modifiedContent);
            }



            textbox1.Text = powershellcodefinal;
        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, textbox1.Text);
        }
    }
}

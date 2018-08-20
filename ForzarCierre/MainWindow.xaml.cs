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
using System.Diagnostics;
namespace ForzarCierre
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Properties.Settings settings;
        List<string> programasACerrar;
        public MainWindow()
        {
            InitializeComponent();
            settings = new Properties.Settings();
            programasACerrar = new List<string>();
            UpdateList();
        }

        private void UpdateList()
        {
            programasACerrar.Clear();
            if (settings.ProgramasGuardados.Contains(';'))
            {
                programasACerrar.AddRange(settings.ProgramasGuardados.Split(';'));
            }
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Process[] porCerrar;
            for (int i = 0; i < programasACerrar.Count; i++)
            {
                try
                {
                    porCerrar = System.Diagnostics.Process.GetProcessesByName(programasACerrar[i]);
                    for (int j = 0; j < porCerrar.Length; j++)
                        try
                        {
                            porCerrar[j].Kill();
                        }
                        catch { }
                }
                catch { }
            }
            this.Close();
        }

        private void btnCerrar_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            string[] programas;
            AñadirProgramas winAñadirProgramas;
            StringBuilder strProgramas;
            if(Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                winAñadirProgramas = new AñadirProgramas(programasACerrar);
                winAñadirProgramas.ShowDialog();
                programas = winAñadirProgramas.ProgramasAñadidos;
                strProgramas = new StringBuilder("programa");
                for (int i = 0; i < programas.Length; i++)
                    strProgramas.Append(";" + programas[i]);
                settings.ProgramasGuardados = strProgramas.ToString();
                UpdateList();
                settings.Save();
            }
        }
    }
}

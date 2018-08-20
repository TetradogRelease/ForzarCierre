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
using System.Windows.Shapes;

namespace ForzarCierre
{
    /// <summary>
    /// Lógica de interacción para AñadirProgramas.xaml
    /// </summary>
    public partial class AñadirProgramas : Window
    {
        public AñadirProgramas(IList<string> programasAñadidos)
        {
            InitializeComponent();
            for (int i = 1; i < programasAñadidos.Count; i++)
                lstProgramasAñadidos.Items.Add(programasAñadidos[i]);
            btnActualizarLista_Click();
        }

        private void btnActualizarLista_Click(object sender = null, RoutedEventArgs e = null)
        {
            System.Diagnostics.Process[] procesosAbiertos = System.Diagnostics.Process.GetProcesses();
            lstProgramasAbiertos.Items.Clear();
            for (int i = 0; i < procesosAbiertos.Length; i++)
                lstProgramasAbiertos.Items.Add(procesosAbiertos[i].ProcessName);

        }
        public string[] ProgramasAñadidos
        {
            get
            {
                string[] programas = new string[lstProgramasAñadidos.Items.Count];
                for (int i = 0; i < programas.Length; i++)
                    programas[i] = lstProgramasAñadidos.Items[i].ToString();
                return programas;
            }
        }

        private void lstProgramasAbiertos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lstProgramasAbiertos.SelectedItem != null && !lstProgramasAñadidos.Items.Contains(lstProgramasAbiertos.SelectedItem))
            {
                lstProgramasAñadidos.Items.Add(lstProgramasAbiertos.SelectedItem);
                lstProgramasAbiertos.Items.Remove(lstProgramasAbiertos.SelectedItem);
            }
        }

        private void lstProgramasAñadidos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lstProgramasAñadidos.SelectedItem != null)
                lstProgramasAñadidos.Items.Remove(lstProgramasAñadidos.SelectedItem);
        }
    }
}

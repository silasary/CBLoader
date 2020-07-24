using Character_Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EmbeddedUI
{
    /// <summary>
    /// Interaction logic for AboutWizardPage.xaml
    /// </summary>
    public partial class AboutWizardPage : UserControl, IWizardControl, IComponentConnector
    {
        public AboutWizardPage()
        {
            InitializeComponent();
        }

        public object FindElement(string name)
        {
            try
            {
                return FindName(name);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public DockPanel NextStepDock()
        {
            return null;
        }

        public void Update()
        {

        }
    }
}

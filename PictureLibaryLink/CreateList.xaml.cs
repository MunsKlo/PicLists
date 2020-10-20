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

namespace PictureLibaryLink
{
    /// <summary>
    /// Interaktionslogik für CreateList.xaml
    /// </summary>
    public partial class CreateList : Window
    {
        public CreateList()
        {
            InitializeComponent();
        }

        private void btnAbort_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (tbNewList.Text.Length > 0)
            {
                Lists.listsOfImages.Add(new ImageList {
                    ListName = tbNewList.Text
                    });
                Close();
            }
        }
    }
}

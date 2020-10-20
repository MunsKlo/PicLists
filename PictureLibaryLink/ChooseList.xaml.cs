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
    /// Interaktionslogik für ChooseList.xaml
    /// </summary>
    public partial class ChooseList : Window
    {
        public string PathImg { get; set; }
        public ChooseList(string pathImg)
        {
            InitializeComponent();
            FillComboBox();

            PathImg = pathImg;
        }

        void FillComboBox()
        {
            cbLists.Items.Clear();
            cbLists.Items.Refresh();
            if (Lists.listsOfImages.Count > 0)
            {
                foreach (var item in Lists.listsOfImages)
                {
                    cbLists.Items.Add(item.ListName);
                }
                cbLists.SelectedIndex = 0;
            }
        }

        private void btnAbort_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnChoose_Click(object sender, RoutedEventArgs e)
        {
            if(cbLists.SelectedIndex != -1)
            {
                Logic.AddImageToObject(PathImg, Logic.GetImageList(cbLists.SelectedItem.ToString()));
                Close();
            }
        }

        private void btnNewList_Click(object sender, RoutedEventArgs e)
        {
            var win = new CreateList();
            win.ShowDialog();
            if(cbLists.Items.Count != Lists.listsOfImages.Count)
            {
                FillComboBox();
                cbLists.SelectedIndex = Lists.listsOfImages.Count - 1;
            }
        }
    }
}

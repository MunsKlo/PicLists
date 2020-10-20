using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Windows.Threading;

namespace PictureLibaryLink
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool WorkingImg { get; set; } = true;

        public bool Random { get; set; } = false;
        public bool Sequence { get; set; } = false;

        public int ImgIndex { get; set; } = 0;

        ImageList CurrentList { get; set; }
        public string CurrentImage { get; set; }

        Regex numberRegex = new Regex("[^0-9]+");
        DispatcherTimer timer = new DispatcherTimer();
        Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();

            timer.Tick += TimerTick;

            FillComboBox();
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
            }
        }

        private void tbLink_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                imgImage.Source = new BitmapImage(new Uri(tbLink.Text, UriKind.Absolute));
                WorkingImg = true;
            }
            catch (Exception)
            {

                WorkingImg = false;
            }
        }

        private void btnAddList_Click(object sender, RoutedEventArgs e)
        {
            if (WorkingImg)
            {
                var win = new ChooseList(tbLink.Text);
                win.ShowDialog();

                if(Lists.listsOfImages.Count != cbLists.Items.Count)
                {
                    FillComboBox();
                }
            }
        }

        private void cbLists_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrentList = Logic.GetImageList(cbLists.SelectedItem.ToString());

            if(CurrentList.Images.Count > 0)
            {
                imgImage.Source = new BitmapImage(new Uri(CurrentList.Images[0], UriKind.Absolute));
            }
        }

        private void tbInterval_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (numberRegex.IsMatch(tbInterval.Text))
            {
            }
        }

        void TimerTick(object sender, EventArgs e)
        {
            if (Random)
            {
                while (true)
                {
                    var img = CurrentList.Images[random.Next(0, CurrentList.Images.Count)];

                    if (CurrentImage == img && CurrentList.Images.Count > 1)
                    {
                        continue;
                    }
                    CurrentImage = img;
                    imgImage.Source = new BitmapImage(new Uri(img));
                    break;
                }
               
            }
            else
            {
                if (ImgIndex == CurrentList.Images.Count)
                {
                    ImgIndex = 0;
                }

                CurrentImage = CurrentList.Images[ImgIndex];

                imgImage.Source = new BitmapImage(new Uri(CurrentList.Images[ImgIndex++]));
            }
        }

        private void btnSequence_Click(object sender, RoutedEventArgs e)
        {
            SetTimer(false);
        }

        private void btnRandom_Click(object sender, RoutedEventArgs e)
        {
            SetTimer(true);
        }

        void SetTimer(bool isRandom)
        {
            if (Double.TryParse(tbInterval.Text, out double numb))
            {
                timer.Stop();
                timer.Interval = TimeSpan.FromSeconds(Convert.ToDouble(tbInterval.Text));
                Sequence = !isRandom;
                Random = isRandom;
                timer.Start();
            }
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Json-File (*.json) |*.json";
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, JsonHandler.ConvertToJson(Lists.listsOfImages));
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Json-File (*.json)|*.json";
            if (openFileDialog.ShowDialog() == true)
            {
                Lists.listsOfImages = JsonHandler.ConvertToListImageList(File.ReadAllText(openFileDialog.FileName));
            }

            if(Lists.listsOfImages.Count > 0)
            {
                FillComboBox();
            }
        }
    }
}

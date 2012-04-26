using System;
using System.Windows.Controls;

namespace agTetribricks
{
    public partial class Main : UserControl
    {
        private double _originalHeight = 1000 ;
        private double _originalWidth = 1236;


        public Main()
        {
            InitializeComponent();
            App.Current.Host.Content.Resized += Content_Resized;
        }

        private void Content_Resized(object sender, EventArgs e)
        {
            double currentWidth = App.Current.Host.Content.ActualWidth;
            double currentHeight = App.Current.Host.Content.ActualHeight;
        }
    }
}
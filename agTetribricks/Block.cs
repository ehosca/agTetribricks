using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace TetriBricks
{
    public class Block  
    {

        public int Row
        {
            get { return (int)GetValue(RowProperty); }
            set { SetValue(RowProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Row.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowProperty =
            DependencyProperty.Register("Row", typeof(int), typeof(Block), new UIPropertyMetadata(0));

        public int Col
        {
            get { return (int)GetValue(ColProperty); }
            set { SetValue(ColProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Row.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColProperty =
            DependencyProperty.Register("Col", typeof(int), typeof(Block), new UIPropertyMetadata(0));



    }
}

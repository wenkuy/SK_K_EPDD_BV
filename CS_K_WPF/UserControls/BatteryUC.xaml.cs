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

namespace CS_K_WPF.UserControls
{
    /// <summary>
    /// BatteryUC.xaml 的交互逻辑
    /// </summary>
    public partial class BatteryUC : UserControl
    {
        public BatteryUC()
        {
            InitializeComponent();
        
        }


        //0~100
        public float Electricity
        {
            get { return (float)GetValue(ElectricityProperty); }
            set { SetValue(ElectricityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Electricity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ElectricityProperty =
            DependencyProperty.Register("Electricity", typeof(float), typeof(BatteryUC), new PropertyMetadata(0f, ElectricityPropertyCallback));

        private static void ElectricityPropertyCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
        }



        public float Temperature
        {
            get { return (float)GetValue(TemperatureProperty); }
            set { SetValue(TemperatureProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Temperature.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TemperatureProperty =
            DependencyProperty.Register("Temperature", typeof(float), typeof(BatteryUC), new PropertyMetadata(0f, TemperaturePropertycallback));

        private static void TemperaturePropertycallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
           
        }





        public Color BatteryColor
        {
            get { return (Color)GetValue(BatteryColorProperty); }
            set { SetValue(BatteryColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BatteryColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BatteryColorProperty =
            DependencyProperty.Register("BatteryColor", typeof(Color), typeof(BatteryUC), new PropertyMetadata(Color.FromRgb(30, 30, 30),ColorPropertyChangedCallback));

        private static void ColorPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
           
        }
    }
}

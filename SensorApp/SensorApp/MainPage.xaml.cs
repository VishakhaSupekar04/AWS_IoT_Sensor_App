using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XFGloss;
using SensorApp.Model;
using SensorApp.Services;
using Microcharts;
using Microcharts.Forms;

using Entry = Microcharts.Entry;

namespace SensorApp
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<TemperatureData> Data;
        public static List<Entry> listEntry1 = new List<Entry> { };
        private static bool first = true;

        public MainPage()
        {
            InitializeComponent();
            var bkgrndGradient = new Gradient()
            {
                Rotation = 150,
                Steps = new GradientStepCollection()
                {
                   new GradientStep(Color.Orange, 0),
                   new GradientStep(Color.Yellow, .25),
                   new GradientStep(Color.Goldenrod, 0.5),
                   new GradientStep(Color.LightYellow, 1)
                }
            };
            ContentPageGloss.SetBackgroundGradient(this, bkgrndGradient);
            Data = new ObservableCollection<TemperatureData>();
        }

        private async void GetChart(object sender, EventArgs e)
        {
            float maxTemperature = float.MinValue; // max is 0.000
            float minTemperature = float.MaxValue; // min is Highest value of float
            if (first)
            {
                ApiService service = new ApiService();
                var temperatureValues = await service.GetTemperatureValues();
                foreach (var value in temperatureValues)
                {
                    Data.Add(value);
                }

                GetChartData getChartData = new GetChartData();
                listEntry1.Clear();
                try
                {
                    getChartData.DisplayChart(Data, listEntry1);
                    //List<Entry> SortedList = listEntry1.OrderBy(o => o.Value).ToList();
                    Chart1.Chart = new LineChart
                    {
                        //Entries = SortedList,
                        Entries = listEntry1,
                        LineMode = LineMode.Spline,
                        LineSize = 9,
                        PointMode = PointMode.None,
                        LabelTextSize = 40,
                    };

                    var count = Data.Count;
                    float sum = 0;
                    for (int i = 0; i < count; i++)
                    {
                        float val = float.Parse(Data[i].Temperature.ToString());
                        sum = sum + val;
                        if (maxTemperature < val)
                        {
                            maxTemperature = val;
                        }
                        if (minTemperature > val)
                        {
                            minTemperature = val;
                        }
                    }
                    
                    Highest.Text = maxTemperature.ToString();
                    Lowest.Text =  minTemperature.ToString();
                    Average.Text = (sum / count).ToString();
                }
                catch (NullReferenceException)
                {
                    await DisplayAlert("Error", "no data to display", "Ok");
                }
            }
            first = false;
        }
    }
}

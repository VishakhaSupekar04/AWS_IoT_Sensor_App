using System;
using Entry = Microcharts.Entry;
using System.Collections.Generic;
using Microcharts;
using SkiaSharp;
using Xamarin.Forms;
//using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SensorApp.Model
{
    public class GetChartData
    {
        Entry e;
        public void DisplayChart(ObservableCollection<TemperatureData> Data,List<Entry> listEntry)
        {
            var count = Data.Count;
            int label_value_index = count / 10;
            if(label_value_index<=0)
            {
                label_value_index = 1;
            }
            for (int i=0;i<count;i++)
            {
                float val = float.Parse(Data[i].Temperature.ToString());
                if(i%6==0)
                {
                    e = new Entry(val)
                    {
                        Color = SKColor.Parse("#3498db"),
                        Label = "",
                        ValueLabel = val.ToString()
                    };
                }
                else
                {
                    e = new Entry(val)
                    {
                        Color = SKColor.Parse("#3498db")    
                    };
                }
                listEntry.Add(e);
            }
        }
    }
}

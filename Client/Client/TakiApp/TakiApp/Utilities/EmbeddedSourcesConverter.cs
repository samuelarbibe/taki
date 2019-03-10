using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace TakiApp.Utilities
{
    public static class EmbeddedSourcesConverter
    {
        //gets a normal source as written in database
        //converts it to embeddedResource format as string
        public static string Convert(string source)
        {
            string temp = "TakiApp.Resources.";

            temp += source.Remove(0, 19);

            return temp;
        }

        public static string ConvertWithoutTrim(string source)
        {
            string temp = "TakiApp.Resources.";

            temp += source;

            return temp;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Form
{
    public static class Check
    {
        public static bool nullCheck(List<TextBox> textBoxList)
        {
            bool textBoxNotEmpty = true;
            foreach (var textBox in textBoxList)
            {
                if (textBox.Text.Length == 0)
                {
                    textBox.Background = new SolidColorBrush(Color.FromArgb(224, 236, 101, 95));
                    textBoxNotEmpty = false;
                }
            }

            return textBoxNotEmpty;
        }
    }
}

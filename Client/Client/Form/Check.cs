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
        public static bool nullCheck(List<Control> inputList)
        {
            bool inputNotEmpty = true;
            foreach (var input in inputList)
            {
                if(input is TextBox){
                    if (((TextBox)input).Text.Length == 0)
                    {
                        input.Background = new SolidColorBrush(Color.FromArgb(224, 236, 101, 95));
                        inputNotEmpty = false;
                    }
                }else if(input is PasswordBox){
                    if (((PasswordBox)input).Password.Length == 0)
                    {
                        input.Background = new SolidColorBrush(Color.FromArgb(224, 236, 101, 95));
                        inputNotEmpty = false;
                    }
                }
            }

            return inputNotEmpty;
        }
    }
}

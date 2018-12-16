using Form.TakiService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form.Utilities
{
    public static class Algorithm
    {
        public static bool SimpleCheck(Card given, Card table)
        {
            if (table.VALUE != Card.Value.Zero)
            {
                if(given.VALUE <= Card.Value.Nine && given.VALUE != Card.Value.PlusTwo)
                {
                    if (given.VALUE == table.VALUE || given.COLOR == table.COLOR) return true;
                }
                else
                {
                    switch (given.VALUE)
                    {
                        case Card.Value.Stop: // skip one turn (local action)


                            break;

                        case Card.Value.Plus:

                            break;

                        case Card.Value.SwitchDirection:

                            break;

                        case Card.Value.Taki:

                            break;

                        case Card.Value.SwitchColor:

                            break;

                        case Card.Value.SwitchHand:

                            break;

                        case Card.Value.PlusThree:

                            break;

                    }
                }
                return false;
            }
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_AutoMarkUp
{
    class GenerateMarkName
    {
        public static string GenerateName()
        {
            string pref = main.TextBoxPrefix.Text;
            string name = main.NumericUpDownStartWith.Text;
            string suff = main.TextBoxSuffix.Text;
            int MarkNumber = main.MarkNumber;

            string generatedName = pref + (name+MarkNumber) + suff;


            return generatedName;
        }
    }
}

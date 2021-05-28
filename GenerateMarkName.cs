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
            string pref = main.tb_prefix.Text;
            string name = main.tb_startnumber.Text;
            string suff = main.tb_suffix.Text;
            int MarkNumber = main.MarkNumber;

            string generatedName = pref + (name+MarkNumber) + suff;


            return generatedName;
        }
    }
}

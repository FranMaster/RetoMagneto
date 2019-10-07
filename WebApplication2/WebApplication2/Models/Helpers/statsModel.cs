using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Helpers
{
    public class statsModel
    {      
       public ulong count_mutant_dna { get; set; }
       public ulong count_human_dna { get; set; }
       public double Ratio { get; set; }
       public void CalcularRatio()
        {
            if (count_human_dna > 0)
                Ratio = double.Parse(count_mutant_dna.ToString()) / double.Parse(count_human_dna.ToString());
            else
                Ratio = 0;
        }
           
        
    }
}
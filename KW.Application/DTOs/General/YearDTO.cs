using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KW.Application.DTO
{
    public class YearDTO
    {
        public int Year { get; set; }

        public YearDTO(int year)
        {
            if (year == 0) return;
            this.Year = year;
        }

        public static YearDTO From(int year)
        {
            return new YearDTO(year);
        }

        public static IList<YearDTO> From(IList<int> years)
        {
            IList<YearDTO> collection = new List<YearDTO>();
            foreach (int year in years)
            {
                collection.Add(new YearDTO(year));
            }
            return collection;
        }
    }
}

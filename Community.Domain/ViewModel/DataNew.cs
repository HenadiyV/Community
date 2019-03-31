using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Domain.Entities
{
   public class DataNew
    {
        public int day { set; get; }
        public int month { set; get; }
        public int year { set; get; }
        public List<string> SearchDate = new List<string>() { "1950", "1960", "1970", "1980", "1990", "2000", "2010" };
        public DataNew UserBirthDay(string NowDay)
        {
            if (NowDay != null)
            {
                int day = 0;
                int month = 0;
                int year = 0;
                int ind = NowDay.IndexOf("-");
                if (ind > 0)
                {
                    DataNew _data = new DataNew();
                    int.TryParse(NowDay.Substring(0, ind), out year);
                    int.TryParse(NowDay.Substring(ind + 1, 2), out month);
                    // string m = NowDay.Substring(ind + 1, 2);
                    int.TryParse(NowDay.Substring(NowDay.LastIndexOf("-") + 1), out day);
                    // string d = NowDay.Substring(NowDay.LastIndexOf("-") + 1);
                    if (day > 0 && month > 0 && year > 0)
                    {

                        _data.day = day;
                        _data.month = month;
                        _data.year = year;

                    }

                    else
                    {
                        return null;
                    }

                    return _data;
                }

                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }
        public DataNew UserNewData(string NowDay)
        {
            if (NowDay != null)
            {
                int day = 0;
                int month = 0;
                int year = 0;
                int ind = NowDay.IndexOf(".");
                if (ind > 0)
                {
                    DataNew _data = new DataNew();
                    if (int.TryParse(NowDay.Substring(0, ind), out day) && int.TryParse(NowDay.Substring(ind + 1, NowDay.IndexOf(".")), out month) && int.TryParse(NowDay.Substring(NowDay.LastIndexOf(".") + 1), out year))
                    {

                        _data.day = day;
                        _data.month = month;
                        _data.year = year;

                    }

                    else
                    {
                        return null;
                    }

                    return _data;
                }

                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }
    }
}

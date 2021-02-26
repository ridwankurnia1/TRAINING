using System;

namespace TRAINING.API.Helper
{
    public class CommonMethod
    {
        public static DateTime? NumericToDateNullable(decimal _prmDate)
        {
            if (_prmDate != 0)
            {
                string strDate = Convert.ToString(_prmDate);

                int iYear = Convert.ToInt32(strDate.Substring(0, 4));
                int iMonth = Convert.ToInt32(strDate.Substring(4, 2));
                int iDay = Convert.ToInt32(strDate.Substring(6, 2));

                DateTime _date;

                _date = new DateTime(iYear, iMonth, iDay);

                return _date;
            }
            else
            {
                return null;
            }
        }

    }
}
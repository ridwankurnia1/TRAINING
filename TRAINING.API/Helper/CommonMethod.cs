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

        public static decimal DateToNumeric(DateTime _prmDate)
        {
            string _day = String.Empty, _month = String.Empty, _year = String.Empty;
            string _strDate = String.Empty;

            _day = _prmDate.ToString("dd").Trim();
            _month = _prmDate.ToString("MM").Trim();
            _year = _prmDate.ToString("yyyy").Trim();

            _strDate = _year + _month + _day;

            return Convert.ToDecimal(_strDate);
        }

        public static decimal TimeToNumeric(DateTime _prmDate)
        {
            string _hour = String.Empty, _minute = String.Empty, _second = String.Empty;
            string _strTime = String.Empty;

            _hour = _prmDate.ToString("HH").Trim();
            _minute = _prmDate.ToString("mm").Trim();
            _second = _prmDate.ToString("ss").Trim();

            _strTime = _hour + _minute + _second;

            return Convert.ToDecimal(_strTime);
        }

    }
}
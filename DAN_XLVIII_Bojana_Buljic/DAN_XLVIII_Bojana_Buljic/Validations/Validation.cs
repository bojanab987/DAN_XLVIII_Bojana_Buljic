using System;

namespace DAN_XLVIII_Bojana_Buljic.Validations
{
    /// <summary>
    /// Class for check validation of JMBG
    /// </summary>
    class Validation
    {
        /// <summary>
        /// Method for validating JMBG number
        /// </summary>
        /// <param name="JMBG"></param>
        /// <returns>ture or false</returns>
        public static bool IsValidJMBG(string JMBG)
        {
            //check lenght of JMBG
            if (JMBG.Length != 13)
                return false;

            //check if all characters in JMBG are number values
            for (int i = 0; i < JMBG.Length; i++)
            {
                if (!Char.IsNumber(JMBG, i))
                    return false;
            }

            DateTime now = DateTime.Now;
            int year = 0;
            //check if year part of the JMBG no is correct year
            if (Char.GetNumericValue(JMBG[4]) == 0)
            {
                year = 2000 + 10 * (int)Char.GetNumericValue(JMBG[5]) + (int)Char.GetNumericValue(JMBG[6]);

                if (year > now.Year)
                    return false;

            }
            else if (Char.GetNumericValue(JMBG[4]) == 9)
            {
                year = 1900 + 10 * (int)Char.GetNumericValue(JMBG[5]) + (int)Char.GetNumericValue(JMBG[6]);
            }
            else
                return false;

            //check if month part of JMBG no is correct month
            int month = (int)Char.GetNumericValue(JMBG[2]) * 10 + (int)Char.GetNumericValue(JMBG[3]);
            if (year == now.Year && month > now.Month)
                return false;

            if (month == 0 || month > 12)
                return false;

            //check if day part of JMBG no is correct day no.
            int day = (int)Char.GetNumericValue(JMBG[0]) * 10 + (int)Char.GetNumericValue(JMBG[1]);
            if (year == now.Year && month == now.Month && day >= now.Day)
            {
                return false;
            }
            if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
            {
                if (day == 0 || day > 31)
                    return false;
            }
            else if (month == 4 || month == 6 || month == 9 || month == 11)
            {
                if (day == 0 || day > 30)
                    return false;
            }
            else if (month == 2)
            {
                if (DateTime.IsLeapYear(year))
                {
                    if (day == 0 || day > 29)
                        return false;
                }
                else
                {
                    if (day == 0 || day > 28)
                        return false;
                }
            }
            return true;
        }
    }
}

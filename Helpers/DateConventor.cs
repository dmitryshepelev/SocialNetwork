using System;

namespace SocialNetwork.Helpers
{
    public static class DateConventor
    {
        public static DateTime? ConvertFromString(string str)
        {
            DateTime? result = null;
            if (str != null)
            {
                string[] dateDivide = str.Split('/');
                string newDateString = String.Format("{0}/{1}/{2}", dateDivide[1], dateDivide[0], dateDivide[2]);
                result = DateTime.Parse(newDateString);
            }
            return result;
        }
    }
}
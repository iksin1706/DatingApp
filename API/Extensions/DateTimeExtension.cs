using System.Globalization;

namespace API.Extensions
{
    public static class DateTimeExtension
    {
        public static int CalculateAge(this DateOnly birth){
            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            var age = today.Year - birth.Year;

            if(birth>today.AddDays(-age)) age--;

            return age;
        }       
    }
}
namespace NyanSpecialty.Assistance.Web.Data.Utility
{
    public class RandomPhoneNumberGenerator
    {
        public static string GenerateRandomPhoneNumber()
        {
            Random random = new Random();
            string phoneNumber = "";

            for (int i = 0; i < 10; i++)
            {
                phoneNumber += random.Next(0, 10).ToString();
            }

            return phoneNumber;
        }
    }
    public class RandomEmaiGenerator
    {
        public static string GenerateRandomEmailNumber(string name)
        {
            return name + "@gmail.com";
        }
    }
}

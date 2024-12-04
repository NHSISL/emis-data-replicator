namespace data_replicator;

public class NHSNumberGenerator
{
    public static string GenerateNHSNumber()
    {
        string nineDigits;
        int checksum;

        // Generate NHS number and validate until the checksum is valid
        do
        {
            // Generate 9 random digits
            Random random = new Random();
            nineDigits = "";
            for (int i = 0; i < 9; i++)
            {
                nineDigits += random.Next(0, 10).ToString();
            }

            // Calculate the checksum (the 10th digit)
            checksum = CalculateChecksum(nineDigits);
        } while (checksum == 10); // Regenerate if checksum is 10

        // Return the full NHS number
        return nineDigits + checksum;
    }

    // Function to calculate checksum for the NHS number
    private static int CalculateChecksum(string nineDigits)
    {
        int[] weights = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int sum = 0;
        for (int i = 0; i < 9; i++)
        {
            sum += int.Parse(nineDigits[i].ToString()) * weights[i];
        }

        int remainder = sum % 11;
        int checksum = 11 - remainder;

        if (checksum == 11)
            return 0;
        return checksum;
    }
}
namespace NpBioApi.Models;

public static class NationalParkDataSeeder
{
    public static void Seed(NpBioApiContext context)
    {
        // Use CsvHelper or similar libraries to read CSV files and convert them to Park and Species lists

        // Add those lists to the database using context.Parks.AddRange(parkList) and context.Species.AddRange(speciesList)
        context.SaveChanges();
    }
}

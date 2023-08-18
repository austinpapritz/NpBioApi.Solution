using CsvHelper;
using CsvHelper.Configuration;
using NpBioApi.DataTransferObjects;
using NpBioApi.Models;
using System.Globalization;

public static class NationalParkDataSeeder
{
    public static void Seed(NpBioApiContext context)
    {
        var parksFilePath = Path.Combine("Data", "parks.csv");
        var speciesFilePath = Path.Combine("Data", "species.csv");

        // Reading parks data
        using (var reader = new StreamReader(parksFilePath))
        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
        {
            var parkDtos = csv.GetRecords<ParkDto>().ToList();
            var parks = parkDtos.Select(p => new Park
            {
                ParkCode = p.ParkCode,
                ParkName = p.ParkName,
                State = p.State,
                Acres = p.Acres,
                Latitude = p.Latitude,
                Longitude = p.Longitude
            }).ToList();

            context.Parks.AddRange(parks);
            context.SaveChanges();
        }

        // Reading species data
        using (var reader = new StreamReader(speciesFilePath))
        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
        {
            var speciesDtos = csv.GetRecords<SpeciesDto>().ToList();
            var speciesList = speciesDtos.Select(s => new Species
            {
                SpeciesId = s.SpeciesId,
                ParkId = context.Parks.FirstOrDefault(p => p.ParkName == s.ParkName).Id,
                ParkName = s.ParkName,
                Category = s.Category ?? "Unknown",
                Order = s.Order ?? "Unknown",
                Family = s.Family ?? "Unknown",
                ScientificName = s.ScientificName ?? "Unknown",
                CommonNames = s.CommonNames ?? "Unknown",
                RecordStatus = s.RecordStatus ?? "Unknown",
                Occurrence = s.Occurrence ?? "Unknown",
                Nativeness = s.Nativeness ?? "Unknown",
                Abundance = s.Abundance ?? "Unknown",
                Seasonality = s.Seasonality ?? "Unknown",
                ConservationStatus = s.ConservationStatus ?? "Unknown"
            }).ToList();


            context.Species.AddRange(speciesList);
            context.SaveChanges();
        }

    }
}

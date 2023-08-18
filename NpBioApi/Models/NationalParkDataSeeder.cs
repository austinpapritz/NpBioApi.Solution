using CsvHelper;
using CsvHelper.Configuration;
using NpBioApi.DataTransferObjects;
using NpBioApi.Models;
using System.Globalization;
using System.IO;
using System.Linq;

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
                ParkCode = p.ParkCode ?? "Unknown",
                ParkName = p.ParkName ?? "Unknown",
                State = p.State ?? "Unknown",
                Acres = p.Acres ?? 0,
                Latitude = p.Latitude ?? 0,
                Longitude = p.Longitude ?? 0
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
                SpeciesId = s.SpeciesId ?? "Unknown",
                ParkId = context.Parks.FirstOrDefault(p => p.ParkName == s.ParkName)?.Id ?? 1,
                ParkName = s.ParkName ?? "Unknown",
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

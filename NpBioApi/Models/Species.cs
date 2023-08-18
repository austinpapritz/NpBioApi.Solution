public class Species
{
    public int Id { get; set; } // primary key
    public string SpeciesId { get; set; }
    public string Category { get; set; }
    public string Order { get; set; }
    public string Family { get; set; }
    public string ScientificName { get; set; }
    public string CommonNames { get; set; }
    public string RecordStatus { get; set; }
    public string Occurrence { get; set; }
    public string Nativeness { get; set; }
    public string Abundance { get; set; }
    public string Seasonality { get; set; }
    public string ConservationStatus { get; set; }

    public int ParkId { get; set; }
    public Park Park { get; set; }
}
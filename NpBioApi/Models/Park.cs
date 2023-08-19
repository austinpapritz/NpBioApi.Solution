using System.Text.Json.Serialization;

namespace NpBioApi.Models;
public class Park
{
    public int Id { get; set; }
    public string ParkCode { get; set; }
    public string ParkName { get; set; }
    public string State { get; set; }
    public int Acres { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    [JsonIgnore]
    public ICollection<Species> Species { get; set; }
}

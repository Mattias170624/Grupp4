namespace Grupp4.Models;

//Variables that holds the connection with MongoDB

public class PlanetDBSettings {
    public string ConnectionURI { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string CollectionName { get; set; } = null!;
};
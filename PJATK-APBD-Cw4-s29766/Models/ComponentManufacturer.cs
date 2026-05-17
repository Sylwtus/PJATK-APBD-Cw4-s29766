namespace PJATK_APBD_Cw4_s29766.Models;

public class ComponentManufacturer
{
    public int Id { get; set; }
    public string Abbreviation { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public DateTime FoundationDate { get; set; }
    
    public ICollection<Component> Components { get; set; } = new List<Component>(); //jeden producent - wiele components

}
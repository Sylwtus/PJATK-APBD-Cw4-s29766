namespace PJATK_APBD_Cw4_s29766.Models;

public class Component
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int ComponentManufacturerId { get; set; }
    public int ComponentTypeId { get; set; }

    public ComponentManufacturer ComponentManufacturer { get; set; } = null!; // jeden component - jeden producent
    public ComponentType ComponentType { get; set; } = null!; // jeden component - jeden typ
    
    public ICollection <PCComponent> PCComponents { get; set; } = new List<PCComponent>(); //jeden component w wielu pc
}
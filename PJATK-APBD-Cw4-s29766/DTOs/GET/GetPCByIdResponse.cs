namespace PJATK_APBD_Cw4_s29766.DTOs.GET;

public class GetPCByIdResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public double Weight { get; set; }
    public int Warranty { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Stock { get; set; }

    public List<ComponentResponse> Components { get; set; } = new();
}
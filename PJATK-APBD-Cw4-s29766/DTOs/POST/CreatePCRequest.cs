namespace PJATK_APBD_Cw4_s29766.DTOs.POST;

public class CreatePCRequest
{
    public string Name { get; set; } = null!;
    public double Weight { get; set; }
    public int Warranty { get; set; }
    public int Stock { get; set; }

    public List<CreatePCComponentRequest> Components { get; set; } = new();
}

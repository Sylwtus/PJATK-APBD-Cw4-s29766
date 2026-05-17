namespace PJATK_APBD_Cw4_s29766.Models;

public class PCComponent
{
    public int PCId { get; set; }
    public string ComponentCode { get; set; } = null!;
    public int Amount { get; set; }

    public PC PC { get; set; } = null!;  // jeden Pccomponent dotyczy jednego pc
    public Component Component { get; set; } = null!; // jeden pccomponent dotyczy jednego component

}
public class ZoneModel
{
    public VariantCube[,] zoneFirst;

    public VariantCube[,] zoneSecond;

    public VariantCube[,] zoneThird;

    /// <summary>
    /// Current count variants first category
    /// </summary>
    public int currentCVFC;

    /// <summary>
    /// Current count variants second category
    /// </summary>
    public int currentCVSC;

    public ZoneModel()
    {
        zoneFirst = new VariantCube[3, 3];
        zoneSecond = new VariantCube[3, 3];
        zoneThird = new VariantCube[3, 3];
    }
}

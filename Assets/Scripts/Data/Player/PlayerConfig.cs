public class PlayerConfig
{
    public MainAttachableBase StartingMain { get; private set; }

    public void SetStartingMain(MainAttachableBase startingMain)
    {
        StartingMain = startingMain;
    }
}
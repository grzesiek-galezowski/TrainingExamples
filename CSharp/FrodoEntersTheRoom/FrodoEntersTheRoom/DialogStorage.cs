namespace FrodoEntersTheRoom;

public static class DialogStorage
{
    //bug static variable
    private static readonly Dialog Dialog = new();

    public static Dialog ReadDialog()
    {
        return Dialog;
    }
}
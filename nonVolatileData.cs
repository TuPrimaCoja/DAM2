public class nonVolatileData
{ 
    //Esta clase es para guardar variables que no se deben destruir a lo largo del transcurso del programa

    public static string userDNI = "";
    private static string items = "";


    //Getters y Setters
    public static void SetDNI(string theDni)
    {
        userDNI = theDni;
    }

    public static string GetDNI()
    {
        return userDNI;
    }

    public static void SetItems(string theItems)
    {
        items = theItems;
    }

    public static string GetItems()
    {
        return items;
    }
}
using System;
public static class ExceptionHandler
{
    public static void Throw(object thrownObject)
    {
        throw new Exception($"An exception has been thrown for: {thrownObject}!");
    }
}

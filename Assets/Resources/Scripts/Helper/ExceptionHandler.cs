using System;
public static class ExceptionHandler
{
    public static void Throw(string message)
    {
        throw new Exception($"An exception has been thrown for: {message}");
    }
}

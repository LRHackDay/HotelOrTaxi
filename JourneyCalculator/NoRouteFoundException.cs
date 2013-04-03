using System;

public class NoRouteFoundException : Exception
{
    public NoRouteFoundException(): base("No taxi route found for the journey specified")
    {}
}
using System;
using System.IO;
using Logger;

class Program
{
    static void Main(string[] args)
    {
        var logger = new Logger.Logger();
        logger.LogMessage("application.log", "User logged in", "INFO");
        logger.LogMessage("application.log", "Failed login attempt", "WARNING");

    }
}
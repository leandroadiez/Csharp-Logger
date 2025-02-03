using NUnit.Framework;
using Logger;
using System.IO;


[TestFixture]
public class LoggerTests
{
    private Logger.Logger _Logger;
    private string _filepath = "application.log";

    [SetUp]
    public void Setup()
    {
        _Logger = new Logger.Logger();
        if (File.Exists(_filepath))
        {
            File.Delete(_filepath);
        }
    }

    [TearDown]
    public void TearDown()
    {
        if (File.Exists(_filepath))
        {
            File.Delete(_filepath);
        }
    }

    [Test]
    public void LogMessage_DateShouldMatch()
    {
        string timestamp = DateTime.Now.Date.ToString("yyyy-MM-dd");
        _Logger.LogMessage(_filepath, "User logged in", "INFO");

        string[] logEntries = File.ReadAllLines(_filepath);

        Assert.IsTrue(logEntries[0].Contains(timestamp));
    }

    [TestCase("User logged in", "INFO")]
    [TestCase("Failed login attempt", "WARNING")]
    public void LogMessage_IncludeEntryType(string message, string entryType)
    {
        _Logger.LogMessage(_filepath, message, entryType);
        string[] logEntries = File.ReadAllLines(_filepath);

        Assert.That(1, Is.EqualTo(logEntries.Length));
        Assert.IsTrue(logEntries[0].Contains(message));
        Assert.IsTrue(logEntries[0].Contains(entryType));
    }

    [Test]
    public void LogMessage_MultipleLogs()
    {
        _Logger.LogMessage(_filepath, "User logged in", "INFO");
        _Logger.LogMessage(_filepath, "Failed login attempt", "WARNING");

        string[] logEntries = File.ReadAllLines(_filepath);

        Assert.That(2, Is.EqualTo(logEntries.Length));
        Assert.IsTrue(logEntries[0].Contains("User logged in"));
        Assert.IsTrue(logEntries[0].Contains("INFO"));
        Assert.IsTrue(logEntries[1].Contains("Failed login attempt"));
        Assert.IsTrue(logEntries[1].Contains("WARNING"));

    }


}
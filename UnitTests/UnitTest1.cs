using NUnit.Framework;
using Logger;
using System.IO;


[TestFixture]
public class LoggerTests
{
    private LogEvents _logEvents;
    private const string filepath = "application.log";

    [SetUp]
    public void Setup()
    {
        _logEvents = new LogEvents();
        if (File.Exists(filepath))
        {
            File.Delete(filepath);
        }
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up after tests
        if (File.Exists(filepath))
        {
            File.Delete(filepath);
        }
    }

    [TestCase("User logged in", "INFO")]
    [TestCase("Failed login attempt", "WARNING")]
    [TestCase("Debugging...", "DEBUG")]
    [TestCase("Error in line...", "ERROR")]
    [TestCase("Please contact admin...", "CRITICAL")]
    public void LogMessage_ShouldLogInfoLevelMessage(string message, string level)
    {
        // For each TestCase call log_message and check sopme assertions
        _logEvents.log_message(message, level);

        // Assertions
        string[] logEntries = File.ReadAllLines(filepath);

        Assert.That(1, Is.EqualTo(logEntries.Length));
        Assert.IsTrue(logEntries[0].Contains(message));
        Assert.IsTrue(logEntries[0].Contains(level));
    }

    [Test]
    public void LogMessage_NotLogging()
    {
        // Call log_message with an invalid log level, so the file is not created
        _logEvents.log_message("testing", "TEST");
        Assert.IsTrue(!File.Exists(filepath));
    }

    [Test]
    public void LogMessage_DateMatch()
    {
        string timestamp = DateTime.Now.Date.ToString("yyyy-MM-dd");

        // Check that the date is matching in the log
        _logEvents.log_message("User logged in", "INFO");

        // Assertions
        string[] logEntries = File.ReadAllLines(filepath);

        Assert.IsTrue(logEntries[0].Contains(timestamp));
    }

    [Test]
    public void LogMessage_MultipleLogs()
    {
        _logEvents.log_message("Adding a new product", "INFO");
        _logEvents.log_message("The object is empty... ", "WARNING");

        string[] logEntries = File.ReadAllLines(filepath);

        Assert.That(2, Is.EqualTo(logEntries.Length));
        Assert.IsTrue(logEntries[0].Contains("Adding a new product"));
        Assert.IsTrue(logEntries[1].Contains("The object is empty... "));
    }


}
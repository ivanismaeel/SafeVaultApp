using NUnit.Framework;
using SafeVaultApp.Data;

[TestFixture]
public class TestInputValidation
{
    [Test]
    public void TestForSQLInjection()
    {
        string maliciousInput = "'; DROP TABLE Users; --";
        var db = new Database("YourConnectionStringHere");
        Assert.DoesNotThrow(() => db.AddUser(maliciousInput, "test@example.com"));
    }

    [Test]
    public void TestForXSS()
    {
        string maliciousScript = "<script>alert('XSS');</script>";
        string sanitizedInput = ValidationHelpers.SanitizeInput(maliciousScript);
        bool containsScriptTags = sanitizedInput.Contains("<script>") || sanitizedInput.Contains("</script>");
        Assert.That(containsScriptTags, Is.False);
    }
}

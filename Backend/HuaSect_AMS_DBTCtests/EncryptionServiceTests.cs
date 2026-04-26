using System.Security.Cryptography;
using HuaSect_AMS_DBTC.Service;
using HuaSect_AMS_DBTCclasslib.Interfaces;

[TestClass]
public class EncryptionServiceTests
{
    private IEncryptionService _encryptionService;
    private const string TestKey = "T045npzt2IF3Gl4Oml8UnKbhtUgmIlXkwhZv6QTN9tv45OzXpmF4xAaH000BrI6W";

    [TestInitialize]
    public void Setup()
    {
        _encryptionService = new AesEncryptionService(TestKey);
    }

    [TestMethod]
    public void EncryptDecrypt_RoundTrip_ReturnsOriginal()
    {
        const string plainText = "Sensitive Employee Data: SSN-123-45-6789";

        var encrypted = _encryptionService.Encrypt(plainText);
        var decrypted = _encryptionService.Decrypt(encrypted);

        Assert.AreEqual(plainText, decrypted);
    }

    [TestMethod]
    public void Encrypt_ProducesDifferentCiphertext_EachTime()
    {
        const string plainText = "Same secret";

        var enc1 = _encryptionService.Encrypt(plainText);
        var enc2 = _encryptionService.Encrypt(plainText);

        Assert.AreNotEqual(enc1, enc2, "Encryption should be non-deterministic (uses IV/nonce)");
    }

    [TestMethod]
    [DynamicData(nameof(GetEdgeCaseData), DynamicDataSourceType.Method)]
    public void Encrypt_HandlesEdgeCases(string input)
    {
        var encrypted = _encryptionService.Encrypt(input);
        var decrypted = _encryptionService.Decrypt(encrypted);
        Assert.AreEqual(input, decrypted);
    }

    [TestMethod]
    public void Decrypt_InvalidCiphertext_ThrowsException()
    {
        Assert.ThrowsException<CryptographicException>(() =>
            _encryptionService.Decrypt("not-valid-base64-or-tampered"));
    }

    public static IEnumerable<object[]> GetEdgeCaseData()
    {
        yield return new object[] { "" };
        yield return new object[] { "A" };
        yield return new object[] { "A".PadLeft(1000, 'X') };
    }
}
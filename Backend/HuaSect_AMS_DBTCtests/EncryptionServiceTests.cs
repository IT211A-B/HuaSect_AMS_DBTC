using System.Security.Cryptography;
using HuaSect_AMS_DBTC.Service;
using HuaSect_AMS_DBTCclasslib.Interfaces;

[TestClass]
public class EncryptionServiceTests
{
    private IEncryptionService _encryptionService;
    private const string TestKey = "T045npzt2IF3Gl4Oml8UnKbhtUgmIlXkwhZv6QTN9tv45OzXpmF4xAaH000BrI6W";
    private const string TestIv = "tWQq496CdVaKYB3T0pQ64i9jebrDs6TCtylC8W6tM8WUlI1aoyCy1TMn5RZtub6X";

    [TestInitialize]
    public void Setup()
    {
        _encryptionService = new AesEncryptionService(TestKey, TestIv);
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
    [DataRow("")]
    [DataRow("A")]
    [DataRow("A".PadLeft(1000, 'X'))]
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
}
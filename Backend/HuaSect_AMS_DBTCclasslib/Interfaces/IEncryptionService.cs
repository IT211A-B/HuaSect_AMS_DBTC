namespace HuaSect_AMS_DBTCclasslib.Interfaces;

public interface IEncryptionService
{
    string Encrypt(string plainText);
    string Decrypt(string cipherText);
}
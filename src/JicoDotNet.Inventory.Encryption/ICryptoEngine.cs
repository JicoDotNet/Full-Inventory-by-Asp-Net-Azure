namespace JicoDotNet.Inventory.Encryption
{
    public interface ICryptoEngine
    {
        string Encrypt(string input);
        string Decrypt(string input);
    }
}

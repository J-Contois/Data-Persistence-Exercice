
public class EncryptedPayload
{
    public string Salt { get; set; }
    public string Nonce { get; set; }
    public string Tag { get; set; }
    public string Data { get; set; }
}
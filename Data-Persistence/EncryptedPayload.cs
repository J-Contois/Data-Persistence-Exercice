public class EncryptedPayload
{
    public string Salt { get; set; } = string.Empty;
    public string Nonce { get; set; } = string.Empty;
    public string Tag { get; set; } = string.Empty;
    public string Data { get; set; } = string.Empty;
}

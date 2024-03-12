namespace ArtHubBO.DTO;

public class S3ResponseDto
{
    public int StatusCode { get; set; } = 200;
    public string Message { get; set; } = string.Empty;
    public string LinkSource { get; set; } = string.Empty;
}

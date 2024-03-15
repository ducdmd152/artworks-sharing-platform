namespace ArtHubBO.Constants;

public class S3Constants
{
    private S3Constants() { }

    public static readonly string AccessKey = "AwsConfiguration:AWSAccessKey";
    public static readonly string SecretKey = "AwsConfiguration:AWSSecretKey";
    public static readonly string BucketName = "AwsConfiguration:AWSBucketName";
    public static readonly string UploadSuccess = "Upload successfull."; 
    public static readonly string DeleteSuccess = "Delete successfull.";
    public static readonly string BaseUrlS3 = "https://bird-trading-platform.s3.ap-southeast-1.amazonaws.com/";
    public static readonly string FolderS3 = "Artwork/";
}

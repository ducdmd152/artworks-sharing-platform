using ArtHubBO.DTO;
using ArtHubBO.Models;

namespace ArtHubService.Interface;

public interface IStorageService
{
    Task<S3ResponseDto> UploadFileAsync(S3ObjectModel s3Object, AwsCredentials awsCredentials);
    Task<S3ResponseDto> DeleteFileAsync(S3ObjectModel s3Object, AwsCredentials awsCredentials);
}

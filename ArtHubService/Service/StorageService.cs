using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using ArtHubBO.Constants;
using ArtHubBO.DTO;
using ArtHubBO.Models;
using ArtHubService.Interface;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace ArtHubService.Service;

public class StorageService : IStorageService
{
    public async Task<S3ResponseDto> UploadFileAsync(S3ObjectModel s3Object, AwsCredentials awsCredentials)
    {
        //Add AWS credentials
        var credentials = new BasicAWSCredentials(awsCredentials.AwsKey, awsCredentials.AwsSecret);
        //Specify the region
        var config = new AmazonS3Config()
        {
            RegionEndpoint = Amazon.RegionEndpoint.APSoutheast1
        };
        var response = new S3ResponseDto();

        try
        {
            var uploadRequest = new TransferUtilityUploadRequest()
            {
                InputStream = s3Object.InputStream,
                Key = s3Object.Name,
                BucketName = s3Object.BucketName,
                CannedACL = S3CannedACL.NoACL
            };

            //Create S3 client
            using var client = new AmazonS3Client(credentials, config);

            //Upload utility to S3
            var transferUtility = new TransferUtility(client);            
            await transferUtility.UploadAsync(uploadRequest);

            response.StatusCode = (int) HttpStatusCode.OK;
            response.Message = S3Constants.UploadSuccess;
            response.LinkSource = "https://bird-trading-platform.s3.ap-southeast-1.amazonaws.com/" + s3Object.Name;
        } catch (AmazonS3Exception ex)
        {
            response.StatusCode = (int) ex.StatusCode;
            response.Message = ex.Message;
        } catch (Exception ex) 
        {
            response.StatusCode = (int) HttpStatusCode.InternalServerError;
            response.Message = ex.Message;
        }
        return response;
    }

    public async Task<S3ResponseDto> DeleteFileAsync(S3ObjectModel s3Object, AwsCredentials awsCredentials)
    {
        // Add AWS credentials
        var credentials = new BasicAWSCredentials(awsCredentials.AwsKey, awsCredentials.AwsSecret);
        // Specify the region
        var config = new AmazonS3Config()
        {
            RegionEndpoint = Amazon.RegionEndpoint.APSoutheast1
        };
        var response = new S3ResponseDto();

        try
        {
            // Create S3 client
            using var client = new AmazonS3Client(credentials, config);

            // Create delete request
            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = s3Object.BucketName,
                Key = s3Object.Name
            };

            // Delete object from S3
            await client.DeleteObjectAsync(deleteRequest);

            response.StatusCode = (int)HttpStatusCode.OK;
            response.Message = S3Constants.DeleteSuccess;
        }
        catch (AmazonS3Exception ex)
        {
            response.StatusCode = (int)ex.StatusCode;
            response.Message = ex.Message;
        }
        catch (Exception ex)
        {
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.Message = ex.Message;
        }
        return response;
    }

}

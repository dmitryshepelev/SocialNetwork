using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace SocialNetwork.Helpers
{
    public static class UploadImage
    {
        private static Account cloudinaryAccount = new Account("slideshowapp", "738528734478378", "Yytgqtd5iklPE9L23nmH0xskUxw");

        public static string Upload(HttpPostedFileBase imageToUpload)
        {
            ImageUploadParams uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(imageToUpload.FileName, imageToUpload.InputStream)
            };
            CloudinaryDotNet.Cloudinary cloudinary = new CloudinaryDotNet.Cloudinary(cloudinaryAccount);
            ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
            return uploadResult.Uri.ToString();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace Biz4CMS.Areas.Admin.Controllers
{
    public class ImageBrowserController : Kendo.Mvc.UI.EditorImageBrowserController
    {
        private const string contentFolderRoot = "~/Content/";
        private const string prettyName = "Images/";
        private static readonly string[] foldersToCopy = new[] { "~/Content/shared/" };
        private readonly Biz4CMS.Models.ThumbnailCreator thumbnailCreator;
        public ImageBrowserController()
        {
            thumbnailCreator = new Biz4CMS.Models.ThumbnailCreator();
        }

        /// <summary>
        /// Gets the base paths from which content will be served.
        /// </summary>
        public override string ContentPath
        {
            get
            {
                return CreateUserFolder();
            }
        }

        private string CreateUserFolder()
        {
            var virtualPath = Path.Combine(contentFolderRoot,  prettyName);

           
            return virtualPath;
        }
        private string NormalizePath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return ToAbsolute(ContentPath);
            }

            return CombinePaths(ToAbsolute(ContentPath), path);
        }
        private string ToAbsolute(string virtualPath)
        {
            return VirtualPathUtility.ToAbsolute(virtualPath);
        }

        private string CombinePaths(string basePath, string relativePath)
        {
            return VirtualPathUtility.Combine(VirtualPathUtility.AppendTrailingSlash(basePath), relativePath);
        }

        [OutputCache(Duration = 86400, VaryByParam = "path")]
        public virtual ActionResult SmallImage(string path)
        {
            try
            {  
                path = NormalizePath(path);

                var physicalPath = Server.MapPath(path);

                if (System.IO.File.Exists(physicalPath))
                {
                    Response.AddFileDependency(physicalPath);
                    return CreateThumbnail(physicalPath, 300, 300);
                }
                else
                {
                throw new HttpException(404, "File Not Found");
                }
            }
            catch (Exception)
            {

                throw new HttpException(404, "File Not Found"); 
            }

        }

        [OutputCache(Duration = 86400, VaryByParam = "path;w;h")]
        public virtual ActionResult CropImage(string path, int w =200, int h=300)
        {
            try{
                path = NormalizePath(path);
           
                var physicalPath = Server.MapPath(path);

                if (System.IO.File.Exists(physicalPath))
                {
                    Response.AddFileDependency(physicalPath);
                    return GetCropImage(physicalPath, w, h);
                }
                else
                {
                    throw new HttpException(404, "File Not Found");
                }
            }
            catch (Exception)
            {

                throw new HttpException(404, "File Not Found"); 
            }
           
        }
        

        private FileContentResult CreateThumbnail(string physicalPath, int width = 80, int height = 80)
        {
            using (var fileStream = System.IO.File.OpenRead(physicalPath))
            {
                var desiredSize = new Biz4CMS.Models.ImageSize
                {
                    Width = width,
                    Height = height
                };

                const string contentType = "image/png";

                return File(thumbnailCreator.Create(fileStream, desiredSize, contentType), contentType);
            }
        }

        private FileContentResult GetCropImage(string physicalPath, int w = 80, int h = 80)
        {
            using (var fileStream = System.IO.File.OpenRead(physicalPath))
            {
                var desiredSize = new Biz4CMS.Models.ImageSize
                {
                    Width = w,
                    Height = h
                };

                const string contentType = "image/png";

                return File(thumbnailCreator.Crop(fileStream, desiredSize, contentType), contentType);
            }
        }

        private void CopyFolder(string source, string destination)
        {
            if (!Directory.Exists(destination))
            {
                Directory.CreateDirectory(destination);
            }

            foreach (var file in Directory.EnumerateFiles(source))
            {
                var dest = Path.Combine(destination, Path.GetFileName(file));
                System.IO.File.Copy(file, dest);
            }

            foreach (var folder in Directory.EnumerateDirectories(source))
            {
                var dest = Path.Combine(destination, Path.GetFileName(folder));
                CopyFolder(folder, dest);
            }
        }
    }
}

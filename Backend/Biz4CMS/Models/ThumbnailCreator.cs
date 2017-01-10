namespace Biz4CMS.Models
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System;

    public class ThumbnailCreator
    {
        private static readonly IDictionary<string, ImageFormat> ImageFormats = new Dictionary<string, ImageFormat>{
            {"image/png", ImageFormat.Png},
            {"image/gif", ImageFormat.Gif},
            {"image/jpeg", ImageFormat.Jpeg}
        };

        private readonly ImageResizer resizer;

        public ThumbnailCreator()
        {
            this.resizer = new ImageResizer();
        }

        public byte[] Create(Stream source, ImageSize desiredSize, string contentType)
        {
            using (var image = Image.FromStream(source))
            {
                var originalSize = new ImageSize
                {
                    Height = image.Height,
                    Width = image.Width
                };

                var size = resizer.Resize(originalSize, desiredSize);

                using (var thumbnail = new Bitmap(size.Width, size.Height))
                {
                    ScaleImage(image, thumbnail);

                    using (var memoryStream = new MemoryStream())
                    {
                        thumbnail.Save(memoryStream, ImageFormats[contentType]);

                        return memoryStream.ToArray();
                    }
                }
            }
        }
        public byte[] Crop(Stream source, ImageSize desiredSize, string contentType)
        {
            using (var image = Image.FromStream(source))
            {
                var originalSize = new ImageSize
                {
                    Height = image.Height,
                    Width = image.Width
                };


                using (var thumbnail = new Bitmap(desiredSize.Width, desiredSize.Height))
                {
                    CropImage(desiredSize,image, thumbnail);

                    using (var memoryStream = new MemoryStream())
                    {
                        thumbnail.Save(memoryStream, ImageFormats[contentType]);

                        return memoryStream.ToArray();
                    }
                }
            }
        }

        private void ScaleImage(Image source, Image destination)
        {
            using (var graphics = Graphics.FromImage(destination))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                graphics.DrawImage(source, 0, 0, destination.Width, destination.Height);
            }
        }

        public void CropImage(ImageSize desiredSize, Image sourceImage, Image destination)
        {
            // variable for percentage resize 
            float percentageResize = 0;
            float percentageResizeW = 0;
            float percentageResizeH = 0;

            // variables for the dimension of source and cropped image 
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            // Create a bitmap object file from source file 
            //Bitmap sourceImage = new Bitmap(sourceFilePath);

            // Set the source dimension to the variables 
            int sourceWidth = sourceImage.Width;
            int sourceHeight = sourceImage.Height;

            // Calculate the percentage resize 
            percentageResizeW = ((float)desiredSize.Width / (float)sourceWidth);
            percentageResizeH = ((float)desiredSize.Height / (float)sourceHeight);

            // Checking the resize percentage 
            if (percentageResizeH < percentageResizeW)
            {
                percentageResize = percentageResizeW;
                destY = System.Convert.ToInt16((desiredSize.Height - (sourceHeight * percentageResize)) / 2);
            }
            else
            {
                percentageResize = percentageResizeH;
                destX = System.Convert.ToInt16((desiredSize.Width - (sourceWidth * percentageResize)) / 2);
            }

            // Set the new cropped percentage image
            int destWidth = (int)Math.Round(sourceWidth * percentageResize);
            int destHeight = (int)Math.Round(sourceHeight * percentageResize);

            // Create the image object 

            using (Graphics objGraphics = Graphics.FromImage(destination))
            {
                // Set the graphic format for better result cropping 
                objGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                objGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                objGraphics.DrawImage(sourceImage, new Rectangle(destX, destY, destWidth, destHeight), new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel);

            }
            
        }
    }
}
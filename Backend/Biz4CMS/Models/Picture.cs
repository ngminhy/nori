using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Biz4CMS.Models
{
    public class Picture
    {
        

        public Picture(ViewModels.PictureDto picture, int Productid)
        {
            // TODO: Complete member initialization
            PictureId = picture.PictureId;
            Name = picture.Name;
            Src = picture.Src;
            Tags = picture.Src;
            ProductId = Productid;            
        }

        public Picture()
        {
            // TODO: Complete member initialization
        }
        public int PictureId { get; set; }
        [StringLength(200)]
        public string Name { get; set; }
        [StringLength(200)]
        public string Src { get; set; }
        [StringLength(200)]
        public string Tags { get; set; }
        public bool IsCover { get; set; }
        [StringLength(1000)]
        public string MoreInfo { get; set; }
        public bool IsDeleted { get; set; }
        
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }


        public double Size { get; set; }
    }
}

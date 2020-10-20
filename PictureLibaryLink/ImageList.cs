using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibaryLink
{
    class ImageList
    {
        public int Id { get; set; }
        public List<string> Images { get; set; }
        public string ListName { get; set; }

        public ImageList()
        {
            Id = ++Controller.id;
            Images = new List<string>();
        }
    }
}

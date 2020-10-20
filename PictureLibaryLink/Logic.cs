using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibaryLink
{
    class Logic
    {
        public static void AddImageToObject(string pathImg, ImageList obj)
        {
            obj.Images.Add(pathImg);
        }

        public static ImageList GetImageList(string listName)
        {
            ImageList imgList = new ImageList();
            foreach (var item in Lists.listsOfImages)
            {
                if(listName == item.ListName)
                {
                    imgList = item;
                    break;
                }
            }
            return imgList;
        }
    }
}

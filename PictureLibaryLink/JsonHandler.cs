using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PictureLibaryLink
{
    class JsonHandler
    {
        public static string ConvertToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static List<ImageList> ConvertToListImageList(string json)
        {
            return JsonConvert.DeserializeObject<List<ImageList>>(json);
        }
    }
}

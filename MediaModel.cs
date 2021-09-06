using System;
using System.Collections.Generic;

namespace DigiPresence
{
    public class MediaModel
    {
        public List<HOMEDATA> homeDetails { get; set; }
    }
    public class MediaItem
    {
        public List<string> picture { get; set; }
        public List<string> videos { get; set; }


    }

    public class HOMEDATA
    {
        public string _id { get; set; }
        public int index { get; set; }
        public string guid { get; set; }
        public bool isLiked { get; set; }
        public MediaItem mediaItem { get; set; }
        public string userPicture { get; set; }
        public string name { get; set; }
        public string city { get; set; }
        public string about { get; set; }
        public string postedDate { get; set; }
        public string price { get; set; }
        public string picposted { get; set; }
        public string videoposted { get; set; }
        //public Dictionary<string,List<string>> mediaItem { get; set; }
    }



}

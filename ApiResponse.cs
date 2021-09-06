using Newtonsoft.Json;
using System.Collections.Generic;

namespace DigiPresence
{
    public class ApiResponse
    {

        public List<PostDetails> items { get; set; }

        public override string ToString()
        {
            return items.Count.ToString();
        }

    }
}
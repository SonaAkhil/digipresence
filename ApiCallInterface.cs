using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;

namespace DigiPresence
{
    [Headers("User-Agent: :request:")]
    interface IDigiPresenceGetApi
    {
        [Get("/1dd51ed7a9c749ca96d7")]
        Task<List<PostDetails>> GetUser();
    }
}
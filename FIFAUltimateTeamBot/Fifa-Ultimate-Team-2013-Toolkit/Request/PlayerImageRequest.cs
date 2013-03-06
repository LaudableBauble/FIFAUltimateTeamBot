using System;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Extension;

namespace UltimateTeam.Toolkit.Request
{
    public class PlayerImageRequest : RequestBase
    {
        public async Task<byte[]> GetImageAsync(long resourceId)
        {
            var baseId = resourceId.CalculateBaseId();
            var uriString = string.Format("http://cdn.content.easports.com/fifa/fltOnlineAssets/2013/fut/items/images/players/web/{0}.png", baseId);
            var uri = new Uri(uriString);
            var imageBytes = await Client.GetByteArrayAsync(uri);

            return imageBytes;
        }
    }
}
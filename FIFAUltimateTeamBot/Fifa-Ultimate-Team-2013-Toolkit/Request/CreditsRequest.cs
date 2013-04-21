using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constant;
using UltimateTeam.Toolkit.Model;

namespace UltimateTeam.Toolkit.Request
{
    public class CreditsRequest : RequestBase
    {
        public async Task<CreditsResponse> GetCreditsAsync()
        {
            var response = await Client.SendAsync(CreateRequestMessage(" ", Resources.Credits, HttpMethod.Get));
            response.EnsureSuccessStatusCode();

            return await Deserialize<CreditsResponse>(response);
        }
    }
}
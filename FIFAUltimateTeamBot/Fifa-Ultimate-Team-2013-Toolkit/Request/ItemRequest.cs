using System;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constant;
using UltimateTeam.Toolkit.Extension;
using UltimateTeam.Toolkit.Model;

namespace UltimateTeam.Toolkit.Request
{
    public class ItemRequest : RequestBase
    {
        public async Task<Item> GetItemAsync(long resourceId)
        {
            var baseId = resourceId.CalculateBaseId();
            var uri = new Uri(string.Format(Resources.Item, baseId));
            var response = await Client.GetAsync(uri);
            var itemWrapper = await Deserialize<ItemWrapper>(response);

            return itemWrapper.Item;
        }
    }
}
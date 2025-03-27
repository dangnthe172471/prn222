using Microsoft.AspNetCore.SignalR;

namespace Project_PRN221.Hubs
{
    public class CategoryHubs:Hub
    {
        public async Task SendCategoryUpdate(Guid categoryId, string action)
        {
            await Clients.All.SendAsync("ReceiveCategoryUpdate", categoryId, action);
        }
    }
}

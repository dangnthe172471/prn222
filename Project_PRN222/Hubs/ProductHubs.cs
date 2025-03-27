using Microsoft.AspNetCore.SignalR;

namespace Project_PRN221.Hubs
{
    public class ProductHubs:Hub
    {
        public async Task SendStudentUpdate(Guid productId, string action)
        {
            await Clients.All.SendAsync("ReceiveStudentUpdate", productId, action);
        }
    }
}

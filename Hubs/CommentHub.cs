using Microsoft.AspNetCore.SignalR;
using CourseProject.Models;

namespace CourseProject.Hubs
{
    public class CommentHub : Hub
    {
        public async Task PostComment(Comment comment)
        {
            await Clients.All.SendAsync("ReceiveComment", comment);
        }
    }
}
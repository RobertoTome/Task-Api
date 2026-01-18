using Microsoft.AspNetCore.SignalR;

namespace TestApi.Hubs
{
    public class TaskHub : Hub
    {
        // Este hub está vacío porque usaremos IHubContext desde el servicio
        // Opcionalmente puedes agregar métodos que los clientes puedan invocar

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            Console.WriteLine($"Client connected: {Context.ConnectionId}");
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
            Console.WriteLine($"Client disconnected: {Context.ConnectionId}");
        }
    }
}
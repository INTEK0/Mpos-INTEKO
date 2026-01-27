using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WindowsFormsApp2.App.Helpers.Enums;

namespace WindowsFormsApp2.App.Services
{
    public class BackgroundService
    {
        private readonly ApiService _apiService;
        private readonly ConcurrentQueue<(object data, ApiOperation operation)> _queue;
        private bool _isRunning = false;

        public BackgroundService(ApiService apiService)
        {
            _apiService = apiService;
            _queue = new ConcurrentQueue<(object, ApiOperation)>();
        }

        public void Enqueue(object data, ApiOperation operation)
        {
            _queue.Enqueue((data, operation));
            if (!_isRunning)
                _ = ProcessQueueAsync();
        }

        private async Task ProcessQueueAsync()
        {
            _isRunning = true;
            while (_queue.TryDequeue(out var item))
            {
                int attempt = 0;
                bool success = false;

                while (!success && attempt < 5) // max 5 retry
                {
                    attempt++;
                    var result = await _apiService.SendAsync(item.data, item.operation);

                    if (result.Ok)
                    {
                        Console.WriteLine($"✅ {item.operation} uğurla göndərildi: {result.Inserted} sətir");
                        success = true;
                    }
                    else
                    {
                        Console.WriteLine($"❌ {item.operation} göndərmə xətası: {result.Error}");
                        await Task.Delay(1000 * (int)Math.Pow(2, attempt)); // exponential backoff
                    }
                }

                if (!success)
                    Console.WriteLine($"⚠ {item.operation} göndərilə bilmədi. Queue-da saxlanılacaq.");
                // İstəsən burada fail-ları disk-ə / DB-ə yazıb later retry edə bilərsən
            }
            _isRunning = false;
        }
    }
}
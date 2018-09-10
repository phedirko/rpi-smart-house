using System.Collections.Generic;
using System.Linq;

namespace RpiSmartHouse.Monitoring.Api.Services
{
    public interface IEventRepository
    {
        IEnumerable<string> GetAll();

        void Add(string message);
    }

    public class EventRepository : IEventRepository
    {
        private static readonly MessagePersistance _messagePersistance = new MessagePersistance();

        public EventRepository()
        {
        }

        private readonly object _lock = new object();

        public void Add(string message)
        {
            lock (_lock)
                _messagePersistance.Storage.Add(message);
        }

        public IEnumerable<string> GetAll()
        {
            return _messagePersistance.Storage.ToList();
        }
    }
}

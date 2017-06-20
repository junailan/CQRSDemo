using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DomainRepository
    {
        public T Get<T>(Guid id) where T : SourcedAggregateRoot, new()
        {
            T aggregateRoot = new T();
            Snapshot snapshot;
            IEnumerable<Event> events;
            using (EventDBEntities _dbContext = new EventDBEntities())
            {
                snapshot = _dbContext.Snapshot.OrderByDescending(t => t.Version).FirstOrDefault(t => t.AggregateRootId == id);
                events = _dbContext.Event.Where(t => t.AggregateRootId == id);
            }
            if (snapshot != null)
            {
                aggregateRoot.BulidFromSnapshot(snapshot);
            }
            if (snapshot != null)
            {
                events = events.Where(t => t.Version > snapshot.Version);
            }

            aggregateRoot.BuildFromHistory(events);


            return aggregateRoot;
        }

        private ISnapshot _saveSnapshot = null;
        private List<IEvent> _saveEvents = new List<IEvent>();
        public void Save(SourcedAggregateRoot aggregateRoot)
        {
            if (aggregateRoot.UncommittedEvents.Any())
            {
                var version = aggregateRoot.Version;

                foreach (var @event in aggregateRoot.UncommittedEvents)
                {
                    version++;
                    if (version > 0 && version % 10 == 0) //10个Event生成1个Snapshot
                    {
                        ISnapshot snapshot = aggregateRoot.CreateSnapshot();
                        snapshot.Version = version;
                        _saveSnapshot = snapshot;
                    }
                    @event.Version = version;
                    _saveEvents.Add(@event);
                }
                aggregateRoot.UncommittedEvents.Clear();
            }
        }

        public void Commit()
        {
            using (EventDBEntities _dbContext = new EventDBEntities())
            {
                if (_saveSnapshot != null)
                {
                    _dbContext.Snapshot.Add(_saveSnapshot as Snapshot);
                }
                if (_saveEvents.Count > 0)
                {
                    foreach (var e in _saveEvents)
                    {
                        _dbContext.Event.Add(e as Event);
                    }
                }
            }
        }
    }
}

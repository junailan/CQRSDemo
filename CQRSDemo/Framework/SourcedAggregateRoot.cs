using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public abstract class SourcedAggregateRoot
    {
        public SourcedAggregateRoot() : this(Guid.NewGuid())
        {

        }
        public SourcedAggregateRoot(Guid id)
        {
            this.AggregateRootId = id;
        }

        private readonly List<IEvent> _uncommittedEvents = new List<IEvent>();

        public List<IEvent> UncommittedEvents
        {
            get
            {
                return _uncommittedEvents;
            }
        }

        public Guid AggregateRootId { get; set; }
        public int Version { get; set; }

        public abstract void DoBuildFromSnapshot(ISnapshot snapshot);

        public virtual void BulidFromSnapshot(ISnapshot snapshot)
        {
            this.DoBuildFromSnapshot(snapshot);
        }

        public virtual void BuildFromHistory(IEnumerable<IEvent> events)
        {
            dynamic d = this;
            foreach (var e in events)
            {
                Type type = Type.GetType(e.EventType);
                object obj = JsonConvert.DeserializeObject(e.Data);
                d.HandleEvent(System.Convert.ChangeType(obj, type));
            }
        }

        public abstract ISnapshot CreateSnapshot();

        public void RaiseEvent<T>(T @event) where T : IEvent
        {
            @event.AggregateRootId = this.AggregateRootId;
            @event.AggregateRootType = this.GetType().ToString();
            @event.Timestamp = DateTime.Now;
            @event.EventType = @event.GetType().ToString();
            @event.Data = JsonConvert.SerializeObject(this);

            dynamic d = this;

            d.HandleEvent(System.Convert.ChangeType(@event, @event.GetType()));

            this.UncommittedEvents.Add(@event);
        }

    }
}

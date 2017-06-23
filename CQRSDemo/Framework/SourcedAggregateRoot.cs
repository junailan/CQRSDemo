using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public abstract class SourcedAggregateRoot
    {
        private readonly List<IEvent> _uncommittedEvents = new List<IEvent>();

        [JsonIgnore]
        public List<IEvent> UncommittedEvents
        {
            get
            {
                return _uncommittedEvents;
            }
        }

        public  Guid AggregateRootId { get; set; }
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
                Assembly assembly = Assembly.Load("Events");
                Type type = assembly.GetType(e.EventType);
                object obj = JsonConvert.DeserializeObject(e.Data, type);
                d.HandleEvent(Converter.ChangeTo(obj, type));
                this.Version = e.Version;
            }
        }

        public abstract ISnapshot CreateSnapshot();

        public void RaiseEvent<T>(T @event) where T : IEvent
        {
            @event.AggregateRootType = this.GetType().ToString();
            @event.Timestamp = DateTime.Now;
            @event.EventType = @event.GetType().ToString();
            @event.Data = JsonConvert.SerializeObject(@event);
            @event.Version = this.Version;

            dynamic d = this;

            d.HandleEvent(Converter.ChangeTo(@event, @event.GetType()));

            this.UncommittedEvents.Add(@event);
        }

    }
}

using Framework;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace SynchronizationService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Synchronization service started @{0}.", DateTime.Now);

            try
            {
                while (true)
                {
                    using (EventDBEntities _dbContext = new EventDBEntities())
                    {
                        Handler.EventHandler handler = new Handler.EventHandler();
                        List<Event> unSynchronizedEvents = _dbContext.Event.Where(t => !t.Synchronized && t.FailedTimes < 3).OrderBy(t => t.Id).ToList();
                        if (unSynchronizedEvents.Count > 0)
                        {
                            foreach (Event @event in unSynchronizedEvents)
                            {
                                try
                                {
                                    Assembly assembly = Assembly.Load("Events");
                                    Type type = assembly.GetType(@event.EventType);
                                    object obj = JsonConvert.DeserializeObject(@event.Data, type);
                                    handler.Handle(obj);
                                    @event.Synchronized = true;

                                    Console.WriteLine("AggregateRootId:{0},EventType:{1},Version:{2}", @event.AggregateRootId, @event.EventType, @event.Version);
                                }
                                catch (Exception ex)
                                {
                                    @event.FailedTimes++;
                                    @event.FailedReason = ex.Message;
                                }

                                _dbContext.Entry(@event).State = EntityState.Modified;
                                _dbContext.SaveChanges();
                            }
                        }

                        Thread.Sleep(5000); //每隔5秒执行一次
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}

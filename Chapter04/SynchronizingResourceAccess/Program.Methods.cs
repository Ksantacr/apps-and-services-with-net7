using System.Threading;

partial class Program
{

    // Best practices: https://learn.microsoft.com/en-us/dotnet/standard/threading/managed-threading-best-practices
    // Field like events: https://learn.microsoft.com/en-us/archive/blogs/cburrows/field-like-events-considered-harmful
    // Threadsafe events: https://blog.stephencleary.com/2009/06/threadsafe-events.html
    static void MethodA()
    {
        try
        {
            if(Monitor.TryEnter(SharedObjects.Conch, TimeSpan.FromSeconds(15))) {
                //lock (SharedObjects.Conch)
                //{
                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(Random.Shared.Next(2000));
                    SharedObjects.Message += "A";
                    Interlocked.Increment(ref SharedObjects.Counter);
                    Write(".");
                }
                //}
            } else
            {
                WriteLine("Method A timed out when entering a monitor on conch.");
            }
        }
        finally
        {
            Monitor.Exit(SharedObjects.Conch);
        }
        
    }
    static void MethodB()
    {
        lock (SharedObjects.Conch)
        {
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(Random.Shared.Next(2000));
                SharedObjects.Message += "B";
                Interlocked.Increment(ref SharedObjects.Counter);
                Write(".");
            }
        }
    }
}

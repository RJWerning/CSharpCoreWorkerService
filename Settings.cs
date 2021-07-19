namespace WorkerService1
{
    public class Settings
    {
        public Task FirstWorker { get; set; }
        public Task MyNewWorker { get; set; }
    }

    public class Task
{
        public int IntervalMin { get; set; }
    }
}

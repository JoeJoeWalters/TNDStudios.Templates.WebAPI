namespace WebAPI.Workers
{   
    /// <summary>
    /// 
    /// </summary>
    public class DependencyWorkerState
    {
        /// <summary>
        /// 
        /// </summary>
        public DateTime LastRan { get; set; }

        /// <summary>
        /// Is the worker running?
        /// </summary>
        public Boolean IsRunning { get { return DateTime.UtcNow.AddSeconds(-5).Ticks < LastRan.Ticks; } }
    }
}

namespace WebAPI.Workers
{   
    /// <summary>
    /// 
    /// </summary>
    public interface IWorkerState
    {
        /// <summary>
        /// 
        /// </summary>
        DateTime LastRan { get; set; }

        /// <summary>
        /// Is the worker running?
        /// </summary>
        Boolean IsRunning { get; }
    }
}

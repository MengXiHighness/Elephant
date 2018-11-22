﻿using DS.AFP.Communication.SocketBase;
using System;
namespace DS.AFP.Communication.SocketEngine
{
    /// <summary>
    /// Interface of IPerformanceMonitor
    /// </summary>
    public interface IPerformanceMonitor : IDisposable
    {
        /// <summary>
        /// Start PerformanceMonitor.
        /// </summary>
        void Start();
        /// <summary>
        /// Stop PerformanceMonitor.
        /// </summary>
        void Stop();
        /// <summary>
        /// Invokes when status update.
        /// </summary>
        event Action<NodeStatus> OnStatusUpdate;
        /// <summary>
        /// Get or Set status update time in seconds.
        /// </summary>
        int StatusUpdateInterval { get; set; }
    }
}

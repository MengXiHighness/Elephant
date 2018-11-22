
#if (!NETCF)
#define HAS_READERWRITERLOCK
#endif

using System;

namespace DS.AFP.Common.Core
{
	
	public sealed class ReaderWriterLock
	{
		#region Instance Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <remarks>
		/// <para>
		/// Initializes a new instance of the <see cref="ReaderWriterLock" /> class.
		/// </para>
		/// </remarks>
		public ReaderWriterLock()
		{
#if HAS_READERWRITERLOCK
			m_lock = new System.Threading.ReaderWriterLock();
#endif
		}

		#endregion Private Instance Constructors
  
		#region Public Methods

		/// <summary>
		/// Acquires a reader lock
		/// </summary>
		/// <remarks>
		/// <para>
		/// <see cref="AcquireReaderLock"/> blocks if a different thread has the writer 
		/// lock, or if at least one thread is waiting for the writer lock.
		/// </para>
		/// </remarks>
		public void AcquireReaderLock()
		{
#if HAS_READERWRITERLOCK
			m_lock.AcquireReaderLock(-1);
#else
			System.Threading.Monitor.Enter(this);
#endif
		}

		/// <summary>
		/// Decrements the lock count
		/// </summary>
		/// <remarks>
		/// <para>
		/// <see cref="ReleaseReaderLock"/> decrements the lock count. When the count 
		/// reaches zero, the lock is released.
		/// </para>
		/// </remarks>
		public void ReleaseReaderLock()
		{
#if HAS_READERWRITERLOCK
			m_lock.ReleaseReaderLock();
#else
			System.Threading.Monitor.Exit(this);
#endif
		}

		/// <summary>
		/// Acquires the writer lock
		/// </summary>
		/// <remarks>
		/// <para>
		/// This method blocks if another thread has a reader lock or writer lock.
		/// </para>
		/// </remarks>
		public void AcquireWriterLock()
		{
#if HAS_READERWRITERLOCK
			m_lock.AcquireWriterLock(-1);
#else
			System.Threading.Monitor.Enter(this);
#endif
		}

		/// <summary>
		/// Decrements the lock count on the writer lock
		/// </summary>
		/// <remarks>
		/// <para>
		/// ReleaseWriterLock decrements the writer lock count. 
		/// When the count reaches zero, the writer lock is released.
		/// </para>
		/// </remarks>
		public void ReleaseWriterLock()
		{
#if HAS_READERWRITERLOCK
			m_lock.ReleaseWriterLock();
#else
			System.Threading.Monitor.Exit(this);
#endif
		}

		#endregion Public Methods

		#region Private Members

#if HAS_READERWRITERLOCK
		private System.Threading.ReaderWriterLock m_lock;
#endif

		#endregion
	}
}

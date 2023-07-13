using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace .Common
{
	public class AsyncFileSystemWatcher : IDisposable
	{
		private readonly FileSystemWatcher fileSystemWatcher;
		private readonly bool disposeFileSystemWatcher = true;
		
		private TaskCompletionSource<FileSystemEventArgs>? tcs = null;
		private CancellationToken? cancellationToken = null;

		public AsyncFileSystemWatcher(FileSystemWatcher fileSystemWatcher)
			: this(fileSystemWatcher, disposeFileSystemWatcher: true)
		{ }

		public AsyncFileSystemWatcher(FileSystemWatcher fileSystemWatcher, bool disposeFileSystemWatcher)
		{
			ArgumentNullException.ThrowIfNull(fileSystemWatcher);

			this.fileSystemWatcher = fileSystemWatcher;
			this.disposeFileSystemWatcher = disposeFileSystemWatcher;

			fileSystemWatcher.Changed += OnChange;
			fileSystemWatcher.Created += OnCreated;
			fileSystemWatcher.Deleted += OnDeleted;
			fileSystemWatcher.Disposed += OnDisposed;
			fileSystemWatcher.Error += OnError;
			fileSystemWatcher.Renamed += OnRenamed;
		}

		private void OnChange(object sender, FileSystemEventArgs e) => tcs?.TrySetResult(e);

		private void OnCreated(object sender, FileSystemEventArgs e) => tcs?.TrySetResult(e);

		private void OnDeleted(object sender, FileSystemEventArgs e) => tcs?.TrySetResult(e);

		private void OnDisposed(object? sender, EventArgs e) => tcs?.TrySetCanceled();

		private void OnError(object sender, ErrorEventArgs e) => tcs?.TrySetException(e.GetException());

		// this works because RenamedEventsArgs inherits from FileSystemEventArgs
		private void OnRenamed(object sender, RenamedEventArgs e) => tcs?.TrySetResult(e);

		public Task<FileSystemEventArgs> WatchAsync(CancellationToken cancellationToken)
		{
			// https://devblogs.microsoft.com/premier-developer/the-danger-of-taskcompletionsourcet-class/
			
			tcs ??= new TaskCompletionSource<FileSystemEventArgs>(TaskCreationOptions.RunContinuationsAsynchronously);

			this.cancellationToken = cancellationToken;

			this.cancellationToken.Value.Register(() => tcs.TrySetCanceled(this.cancellationToken.Value));
		
			return tcs.Task;
		}

		private bool disposedValue = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					if (fileSystemWatcher != null)
					{
						fileSystemWatcher.Changed -= OnChange;
						fileSystemWatcher.Created -= OnCreated;
						fileSystemWatcher.Deleted -= OnDeleted;
						fileSystemWatcher.Disposed -= OnDisposed;
						fileSystemWatcher.Error -= OnError;
						fileSystemWatcher.Renamed -= OnRenamed;

						if (disposeFileSystemWatcher)
						{
							fileSystemWatcher.Dispose();
						}

						tcs?.TrySetCanceled(cancellationToken ?? CancellationToken.None);
					}
				}

				disposedValue = true;
			}
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			
			GC.SuppressFinalize(this);
		}
	}
}
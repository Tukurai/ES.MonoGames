using System;
using System.IO;

namespace Helpers;

/// <summary>
/// Static class managing FileSystemWatcher and reload triggers for hot reload support.
/// </summary>
public static class HotReloadManager
{
    private static FileSystemWatcher? _watcher;
    private static string? _watchPath;
    private static bool _pendingReload;
    private static string? _pendingFile;
    private static DateTime _lastChangeTime;
    private static Exception? _lastError;

    // Debounce delay in milliseconds to prevent multiple reloads for a single save
    private const int DebounceDelayMs = 100;

    /// <summary>
    /// Whether hot reload is enabled.
    /// </summary>
    public static bool IsEnabled { get; private set; }

    /// <summary>
    /// Whether there's a pending reload to process.
    /// </summary>
    public static bool HasPendingReload => _pendingReload;

    /// <summary>
    /// The last error that occurred during reload.
    /// </summary>
    public static Exception? LastError => _lastError;

    /// <summary>
    /// Event fired when a file changes. Subscribers can handle custom reload logic.
    /// </summary>
    public static event Action<string>? OnFileChanged;

    /// <summary>
    /// Initialize hot reload watching for the specified directory.
    /// </summary>
    /// <param name="scenesPath">Path to the directory containing XML scene files.</param>
    public static void Initialize(string scenesPath)
    {
        if (_watcher is not null)
        {
            _watcher.Dispose();
            _watcher = null;
        }

        _watchPath = scenesPath;

        // Only enable if the directory exists
        if (!Directory.Exists(scenesPath))
        {
            System.Diagnostics.Debug.WriteLine($"HotReloadManager: Directory not found: {scenesPath}");
            IsEnabled = false;
            return;
        }

        try
        {
            _watcher = new FileSystemWatcher(scenesPath)
            {
                Filter = "*.xml",
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.CreationTime | NotifyFilters.FileName,
                EnableRaisingEvents = true,
                IncludeSubdirectories = true
            };

            _watcher.Changed += OnWatcherEvent;
            _watcher.Created += OnWatcherEvent;
            _watcher.Renamed += OnWatcherRenamed;

            IsEnabled = true;
            System.Diagnostics.Debug.WriteLine($"HotReloadManager: Watching {scenesPath}");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"HotReloadManager: Failed to initialize watcher: {ex.Message}");
            IsEnabled = false;
        }
    }

    /// <summary>
    /// Called from MainGame.Update to check for and process pending reloads.
    /// </summary>
    public static void Update()
    {
        if (!_pendingReload)
            return;

        // Debounce - wait a bit after the last change to let file writes complete
        var timeSinceChange = DateTime.Now - _lastChangeTime;
        if (timeSinceChange.TotalMilliseconds < DebounceDelayMs)
            return;

        _pendingReload = false;

        if (string.IsNullOrEmpty(_pendingFile))
            return;

        try
        {
            System.Diagnostics.Debug.WriteLine($"HotReloadManager: Reloading due to change in {_pendingFile}");

            // Fire event for custom handlers
            OnFileChanged?.Invoke(_pendingFile);

            // Trigger scene reinitialize
            SceneManager.ReinitializeActiveScene();

            _lastError = null;
        }
        catch (Exception ex)
        {
            _lastError = ex;
            System.Diagnostics.Debug.WriteLine($"HotReloadManager: Reload failed: {ex.Message}");
        }
    }

    /// <summary>
    /// Clear the last error.
    /// </summary>
    public static void ClearError()
    {
        _lastError = null;
    }

    /// <summary>
    /// Disable hot reload and clean up resources.
    /// </summary>
    public static void Shutdown()
    {
        if (_watcher is not null)
        {
            _watcher.EnableRaisingEvents = false;
            _watcher.Dispose();
            _watcher = null;
        }

        IsEnabled = false;
        _pendingReload = false;
        _pendingFile = null;
    }

    private static void OnWatcherEvent(object sender, FileSystemEventArgs e)
    {
        // Skip directory changes
        if (Directory.Exists(e.FullPath))
            return;

        ScheduleReload(e.FullPath);
    }

    private static void OnWatcherRenamed(object sender, RenamedEventArgs e)
    {
        // Skip directory renames
        if (Directory.Exists(e.FullPath))
            return;

        ScheduleReload(e.FullPath);
    }

    private static void ScheduleReload(string filePath)
    {
        _pendingReload = true;
        _pendingFile = filePath;
        _lastChangeTime = DateTime.Now;

        System.Diagnostics.Debug.WriteLine($"HotReloadManager: Change detected in {filePath}");
    }
}

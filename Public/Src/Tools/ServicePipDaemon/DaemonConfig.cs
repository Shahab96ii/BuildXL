// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Diagnostics.ContractsLight;
using BuildXL.Ipc.Interfaces;

namespace Tool.ServicePipDaemon
{
    /// <summary>
    ///     Daemon configuration.
    /// </summary>
    public sealed class DaemonConfig : IServerConfig, IClientConfig
    {
        /// <nodoc/>
        public IIpcLogger Logger { get; }

        /// <inheritdoc/>
        IIpcLogger IServerConfig.Logger => Logger;

        /// <inheritdoc/>
        IIpcLogger IClientConfig.Logger => Logger;

        #region ConfigOptions

        // ==================================================================================================
        // Config options
        // ==================================================================================================

        /// <summary>
        ///     Moniker for identifying client/server communications.
        /// </summary>
        public string Moniker { get; }

        /// <inheritdoc />
        public int MaxConcurrentClients => DefaultMaxConcurrentClients;

        /// <inheritdoc />
        public int MaxConcurrentRequestsPerClient => DefaultMaxConcurrentClients;

        /// <inheritdoc />
        public int MaxConnectRetries { get; }

        /// <inheritdoc />
        public TimeSpan ConnectRetryDelay { get; }

        /// <inheritdoc />
        public bool StopOnFirstFailure { get; }

        /// <summary>
        ///     Enable logging ETW events related to drop creation and finalization.
        /// </summary>
        public bool EnableCloudBuildIntegration { get; }

        /// <summary>
        ///     Log directory.
        /// </summary>
        public string LogDir { get; }

        /// <summary>
        ///     Enable verbose logging.
        /// </summary>
        public bool Verbose { get; }
        #endregion

        #region Defaults

        // ==================================================================================================
        // Defaults
        // ==================================================================================================

        /// <nodoc/>
        public const int DefaultMaxConcurrentClients = 5000;

        /// <nodoc/>
        public const int DefaultMaxConnectRetries = 1;

        /// <nodoc/>
        public const bool DefaultStopOnFirstFailure = false;

        /// <nodoc/>
        public static readonly TimeSpan DefaultConnectRetryDelay = TimeSpan.FromSeconds(5);

        /// <nodoc/>
        public static bool DefaultEnableCloudBuildIntegration { get; } = false;

        /// <nodoc/>
        public static bool DefaultVerbose { get; } = false;
        #endregion

        // ==================================================================================================
        // Constructor
        // ==================================================================================================

        /// <nodoc/>
        public DaemonConfig(
            IIpcLogger logger,
            string moniker,
            int? maxConnectRetries = null,
            TimeSpan? connectRetryDelay = null,
            bool? stopOnFirstFailure = null,
            bool? enableCloudBuildIntegration = null,
            string logDir = null,
            bool? verbose = null)
        {
            Contract.Requires(logger != null);
            Moniker = moniker;
            Logger = logger;
            MaxConnectRetries = maxConnectRetries ?? DefaultMaxConnectRetries;
            ConnectRetryDelay = connectRetryDelay ?? DefaultConnectRetryDelay;
            StopOnFirstFailure = stopOnFirstFailure ?? DefaultStopOnFirstFailure;
            EnableCloudBuildIntegration = enableCloudBuildIntegration ?? DefaultEnableCloudBuildIntegration;
            LogDir = logDir;
            Verbose = verbose ?? DefaultVerbose;
        }
    }
}

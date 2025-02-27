﻿namespace MessageQueue.Configuration
{
    public class FactorySettings
    {
        public required string HostName { get; set; }
        public required int Port { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public bool AutomaticRecoveryEnabled { get; set; } = true;
    }
}

﻿// -----------------------------------------------------------------------
// <copyright file="Permissions.cs" company="Exiled Team">
// Copyright (c) Exiled Team. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Exiled.Permissions
{
    using System;
    using Exiled.API.Features;
    using Exiled.API.Interfaces;
    using Exiled.Permissions.Events;

    /// <summary>
    /// Handles all plugin-related permissions, for executing commands, doing actions and so on.
    /// </summary>
    public class Permissions : Plugin
    {
        private Command command;

        /// <inheritdoc/>
        public override string Name => $"{base.Name} {Version.Major}.{Version.Minor}.{Version.Build}";

        /// <inheritdoc/>
        public override Version RequiredExiledVersion => new Version(2, 0, 0);

        /// <inheritdoc/>
        public override IConfig Config => new Config();

        /// <inheritdoc/>
        public override void OnEnabled()
        {
            if (!Config.IsEnabled)
                return;

            command = new Command();

            Exiled.Events.Handlers.Server.SendingRemoteAdminCommand += command.OnSendingRemoteAdminCommand;

            Extensions.Permissions.Create();
            Extensions.Permissions.Reload();
        }

        /// <inheritdoc/>
        public override void OnDisabled()
        {
            Extensions.Permissions.Groups.Clear();

            Exiled.Events.Handlers.Server.SendingRemoteAdminCommand -= command.OnSendingRemoteAdminCommand;

            command = null;
        }

        /// <inheritdoc/>
        public override void OnReloaded()
        {
        }
    }
}
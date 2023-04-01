// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

import * as Managed from "Sdk.Managed";
import * as Shared from "Sdk.Managed.Shared";
import * as SysMng from "System.Management";
import * as MacServices from "BuildXL.Sandbox.MacOS";

namespace Processes {

    @@public
    export const dll = BuildXLSdk.library({
        assemblyName: "BuildXL.Processes",
        sources: globR(d`.`, "*.cs"),
        generateLogs: true,
        references: [
            ...addIfLazy(!BuildXLSdk.isDotNetCore, () => [
                importFrom("System.Text.Json").withQualifier({targetFramework: "netstandard2.0"}).pkg,
                importFrom("System.Memory").withQualifier({ targetFramework: "netstandard2.0" }).pkg,
            ]),

            ...addIf(BuildXLSdk.isFullFramework,
                BuildXLSdk.NetFx.System.IO.Compression.dll,
                BuildXLSdk.NetFx.System.Management.dll,
                BuildXLSdk.NetFx.System.Net.Http.dll,
                NetFx.Netstandard.dll
            ),
            ...addIf(BuildXLSdk.isDotNetCoreOrStandard,
                SysMng.pkg.override<Shared.ManagedNugetPackage>({
                    runtime: [
                        Shared.Factory.createBinaryFromFiles(SysMng.Contents.all.getFile(r`runtimes/win/lib/netcoreapp2.0/System.Management.dll`))
                    ]
                }),
                importFrom("System.IO.Pipelines").pkg
            ),
            importFrom("BuildXL.Utilities").Native.dll,
            importFrom("BuildXL.Utilities").Interop.dll,
            importFrom("BuildXL.Utilities").Utilities.Core.dll,
        ],
        internalsVisibleTo: [
            "BuildXL.Engine",
            "Test.BuildXL.Engine",
            "Test.BuildXL.Processes",
            "Test.BuildXL.Processes.Detours",
            "ExternalToolTest.BuildXL.Scheduler",
            "BuildXL.ProcessPipExecutor",
        ],
        runtimeContent: [
            ...addIfLazy(Context.getCurrentHost().os === "win" && qualifier.targetRuntime === "win-x64", () => [
                importFrom("BuildXL.Sandbox.Windows").Deployment.detours,
            ]),
            ...addIfLazy(Context.getCurrentHost().os === "macOS" && qualifier.targetRuntime === "osx-x64", () => [
                MacServices.Deployment.bxlESDaemon,
            ]),
            ...addIfLazy(MacServices.Deployment.macBinaryUsage !== "none" && qualifier.targetRuntime === "osx-x64", () => [
                MacServices.Deployment.kext,
                MacServices.Deployment.sandboxMonitor,
                MacServices.Deployment.sandboxLoadScripts
            ]),
        ],
    });
}

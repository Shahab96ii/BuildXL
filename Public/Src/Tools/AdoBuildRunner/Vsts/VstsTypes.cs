﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BuildXL.AdoBuildRunner.Vsts
{

    #region Data objects for HTTP requests, to be serialized to and from JSON
    /// <nodoc />
    [DataContract]
    public class QueueBuildRequest
    {
        /// <nodoc />
        [DataMember]
        public QueueBuildDefinition Definition { get; set; }

        /// <nodoc />
        [DataMember]
        public string Parameters { get; set; }

        /// <nodoc />
        [DataMember]
        public Dictionary<string, string> TemplateParameters { get; set; }


        /// <nodoc />
        [DataMember]
        public Dictionary<string, string> TriggerInfo { get; set; }

        /// <nodoc />
        [DataMember]
        public string SourceBranch { get; set; }

        /// <nodoc />
        [DataMember]
        public string SourceVersion { get; set; }
    }

    /// <nodoc />
    [DataContract]
    public class QueueBuildDefinition
    {
        /// <nodoc />
        [DataMember]
        public int Id { get; set; }
    }

    /// <nodoc />
    [DataContract]
    public class AdoLink
    {
        /// <nodoc />
        [JsonPropertyName("href")]
        [DataMember(Name = "href")]
        public string Href { get; set; }
    }

    /// <nodoc />
    [DataContract]
    public class AdoLinks
    {
        /// <nodoc />
        [JsonPropertyName("self")]
        [DataMember(Name = "self")]
        public AdoLink Self { get; set; }

        /// <nodoc />
        [JsonPropertyName("web")]
        [DataMember(Name = "web")]
        public AdoLink Web { get; set; }
    }

    /// <nodoc />
    [DataContract]
    public class BuildData
    {
        /// <nodoc />
        [JsonPropertyName("_links")]
        [DataMember(Name = "_links")]
        public AdoLinks Links { get; set; }

        /// <nodoc />
        [JsonPropertyName("triggerInfo")]
        [DataMember(Name = "triggerInfo")]
        public Dictionary<string, string> TriggerInfo { get; set; }
    }
    #endregion
}
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using BuildXL.Cache.MemoizationStore.Interfaces.Sessions;

namespace BuildXL.Cache.MemoizationStore.VstsInterfaces
{
    /// <summary>
    /// The interface representing an http client to talk to a VSTS build cache service.
    /// </summary>
    public interface IBuildCacheHttpClient : IBuildCacheHttpClientCommon
    {
        /// <summary>
        /// Gets a content bag from the build cache service given a strong fingerprint for a particular request context.
        /// This represents
        /// 1) opaque bag with serialized bytes that can be transferred to the consumer.
        /// 2) a set of download URIs for content for the blobs that are contained by the content bag.
        /// </summary>
        Task<ContentHashListResponse> GetContentHashListAsync(
            string cacheNamespace,
            StrongFingerprint strongFingerprint);

        /// <summary>
        /// Adds a content bag to the L3 cache store. Adding a content bag also means
        /// 1) Referencing the blobbed items that belong to the content bag
        /// 2) Adding the content bag to the store
        /// 3) Adding a fingerprint selector that leads to this content bag being found
        /// </summary>
        Task<ContentHashListResponse> AddContentHashListAsync(
            string cacheNamespace,
            StrongFingerprint strongFingerprint,
            ContentHashListWithCacheMetadata contentHashList,
            bool forceUpdate);

        /// <summary>
        /// Returns a set of fingerprintSelectors that match the weak fingerprint being requested.
        /// If the cardinality of the content bag to fingerprintselector is such that there is only 1 content bag for that selector
        /// returns a maximum of <paramref name="maxContentBagsToFetch" /> that meet this criteria.
        /// </summary>
        Task<SelectorsResponse> GetSelectors(
            string cacheNamespace,
            Fingerprint weakFingerprint,
            int maxSelectorsToFetch,
            int maxContentBagsToFetch);

        /// <summary>
        /// Returns a set of fingerprintSelectors that match the weak fingerprint being requested.
        /// Does not return any content bags associated with that fingerprint selector.
        /// Returns a maximum of maxSelectorsToFetch selectors.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        Task<SelectorsResponse> GetSelectors(
            string cacheNamespace,
            Fingerprint weakFingerprint,
            int maxSelectorsToFetch);

        /// <summary>
        /// Returns a set of fingerprintSelectors that match the weak fingerprint being requested.
        /// Returns all associated fingerprintselectors, and returns no content bags.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        Task<SelectorsResponse> GetSelectors(string cacheNamespace, Fingerprint weakFingerprint);
    }
}

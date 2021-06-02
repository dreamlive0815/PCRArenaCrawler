using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

using SimpleHTTPClient;

using Core.Extensions;

namespace PCRArena
{
    public class PCRArenaCrawler
    {
        private static string authKey;

        public static string GetAuthKey()
        {
            if (string.IsNullOrWhiteSpace(authKey))
            {
                var content = File.ReadAllText("pcrarena_authkey.txt");
                authKey = content.Trim();
            }
            return authKey;
        }

        private static Client httpClient;

        private static Client GetHttpClient()
        {
            if (httpClient == null)
            {
                httpClient = new Client();
                httpClient.SetTimeoutMS(5000);
                httpClient.SetRequestHeader("authorization", GetAuthKey());
            }
            return httpClient;
        }

        public PCRArenaCrawler()
        {
        }

        public string AttackTeamQueryRaw(ArenaAttackTeamQueryParams args)
        {
            var r = GetHttpClient().PostJson("https://api.pcrdfans.com/x/v1/search", args);
            return r;
        }
    }

    public class ArenaAttackTeamQueryParams
    {
        public static readonly int REGION_ALL = 1;
        public static readonly int REGION_MAINLAND = 2;
        public static readonly int REGION_TAIWAN = 3;
        public static readonly int REGION_JAPAN = 4;
        public static readonly int REGION_INTERNATIONAL = 5;

        public static readonly int SORT_ALL = 1;
        public static readonly int SORT_TIME = 2;
        public static readonly int SORT_COMMENT = 3;

        [JsonPropertyName("_sign")]
        public string Sign { get; set; } = "a";

        [JsonPropertyName("nonce")]
        public string Nonce { get; set; } = "a";

        [JsonPropertyName("def")]
        public List<int> DefenceTeamIds { get; set; } = new List<int>();

        [JsonPropertyName("page")]
        public int Page { get; set; } = 1;

        [JsonPropertyName("sort")]
        public int Sort { get; set; } = SORT_ALL;

        [JsonPropertyName("region")]
        public int Region { get; set; } = REGION_ALL;

        [JsonPropertyName("ts")]
        public long Timestamp { get; set; } = DateTime.Now.ToUnixTimestamp();

        public ArenaAttackTeamQueryParams SetDefenceTeamIds(IEnumerable<int> defenceUnitIds)
        {
            DefenceTeamIds = defenceUnitIds.Select(id => id * 100 + 1).ToList();
            return this;
        }
    }
}
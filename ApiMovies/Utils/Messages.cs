namespace ApiMovies.Utils
{
    public static class Messages
    {
        public struct EndpointMetadata
        {
            public struct TrendingEndpoint
            {
                public const string MESSAGE_TRENDING_LIST_SUMMARY = "List of trending Movies";
                public const string MESSAGE_TRENDING_LIST_DESCRIPTION = "This endpoint returns the information about trending Movies";
            }

            public struct PopularEndpoint
            {
                public const string MESSAGE_POPULAR_LIST_SUMMARY = "List of Popular Movies";
                public const string MESSAGE_POPULAR_LIST_DESCRIPTION = "This endpoint returns the information about Popular Movies";
            }

            public struct SearchEndpoint
            {
                public const string MESSAGE_SEARCH_LIST_SUMMARY = "Find Movie by name";
                public const string MESSAGE_SEARCH_LIST_DESCRIPTION = "This endpoint returns the information about Movies";
            }

            public struct HomeEndpoint
            {
                public const string MESSAGE_HOME_LIST_SUMMARY = "List movies random";
                public const string MESSAGE_HOME_LIST_DESCRIPTION = "This endpoint returns the information about Movies random";
            }
        }
    }
}

public static class MappingDictionary
{
    public static readonly Dictionary<string, int> WarTagToValueMapping = new Dictionary<string, int>
    {
        { "war_1", 1 },
        { "war_3", 3 },
        { "war_5", 5 },
        { "war_neg_1", -1 }
        // Add more key-value pairs as needed
    };

    public static readonly Dictionary<string, int> CoinTagToValueMapping = new Dictionary<string, int>
    {
        { "coin_1", 1 },
        { "coin_3", 3 },
        { "coin_6", 6 }        
        // Add more key-value pairs as needed
    };

    public static readonly Dictionary<string, int> ExpaTagToValueMapping = new Dictionary<string, int>
    {
        { "expa_1", 1 },
        { "expa_2", 2 },
        { "expa_3", 3 },
        { "expa_4", 4 },
        { "expa_5", 5 },
        { "expa_6", 6 },
        { "expa_7", 7}
        // Add more key-value pairs as needed
    };

    public static readonly Dictionary<string, int> BlueTagToValueMapping = new Dictionary<string, int>
    {
        { "blue_2", 2 },
        { "blue_3", 3 },
        { "blue_4", 4 },
        { "blue_5", 5 },
        { "blue_6", 6 },
        { "blue_7", 7 },
        { "blue_8", 8}
        // Add more key-value pairs as needed
    };
}

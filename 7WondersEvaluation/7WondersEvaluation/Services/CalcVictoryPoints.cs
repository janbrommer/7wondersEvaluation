public class CalcVictoryPoints{
    private ApiResult _result;
    public CalcVictoryPoints(ApiResult result)
    {
        // Constructor logic goes here
        _result = result;        
    }

    public EvaluationData createEvaluationData()
    {
        EvaluationData evaluationData = new EvaluationData();
        evaluationData.Name = "Test";
        evaluationData.Red = calcRed();
        evaluationData.Coins = calcCoin();
        evaluationData.ExpansionStages = calcExpa();
        evaluationData.Blue = calcBlue();
        evaluationData.Yellow = 0;
        evaluationData.Violet = 0;
        evaluationData.Green = calcGreen();
        evaluationData.Sum = evaluationData.Red + evaluationData.Coins + evaluationData.ExpansionStages + evaluationData.Blue + evaluationData.Yellow + evaluationData.Violet + evaluationData.Green;
        return evaluationData;
    }

    private int calcRed()
    {
        int redCount = 0;
        
        // Access the TagToValueMapping dictionary from the static class
        Dictionary<string, int> tagToValueMapping = MappingDictionary.WarTagToValueMapping;
        if (_result.Predictions == null)
        {
            return redCount;
        }
        foreach (var prediction in _result.Predictions)
        {
            // Check if the tag name exists in the dictionary
            if (tagToValueMapping.TryGetValue(prediction.TagName, out int tagValue))
            {
                // If it does, add the value to the sum
                redCount += tagValue;;
                
            }
        }

        return redCount;
    }

    private int calcCoin()
    {
        int coinCount = 0;
        
        // Access the TagToValueMapping dictionary from the static class
        Dictionary<string, int> tagToValueMapping = MappingDictionary.CoinTagToValueMapping;
        if (_result.Predictions == null)
        {
            return coinCount;
        }
        foreach (var prediction in _result.Predictions)
        {
            // Check if the tag name exists in the dictionary
            if (tagToValueMapping.TryGetValue(prediction.TagName, out int tagValue))
            {
                // If it does, add the value to the sum
                coinCount += tagValue;;
                
            }
        }

        return  coinCount / 3;

    }
    private int calcExpa()
    {
        int expaSum = 0;
        
        // Access the TagToValueMapping dictionary from the static class
        Dictionary<string, int> tagToValueMapping = MappingDictionary.ExpaTagToValueMapping;
        if (_result.Predictions == null)
        {
            return expaSum;
        }
        foreach (var prediction in _result.Predictions)
        {
            // Check if the tag name exists in the dictionary
            if (tagToValueMapping.TryGetValue(prediction.TagName, out int tagValue))
            {
                // If it does, add the value to the sum
                expaSum += tagValue;;
                
            }
        }

        return expaSum;
    }
    private int calcBlue()
    {
        int Sum = 0;
        
        // Access the TagToValueMapping dictionary from the static class
        Dictionary<string, int> tagToValueMapping = MappingDictionary.BlueTagToValueMapping;
        if (_result.Predictions == null)
        {
            return Sum;
        }
        foreach (var prediction in _result.Predictions)
        {
            // Check if the tag name exists in the dictionary
            if (tagToValueMapping.TryGetValue(prediction.TagName, out int tagValue))
            {
                // If it does, add the value to the sum
                Sum += tagValue;;
                
            }
        }
        return Sum;    
    }
    
    private int calcGreen()
    {
        int circle = 0;
        int gear = 0;
        int stone = 0;
        int choice = 0;
        
        // Access the TagToValueMapping dictionary from the static class
        Dictionary<string, int> tagToValueMapping = MappingDictionary.BlueTagToValueMapping;
        if (_result.Predictions == null)
        {
            return 0;
        }
        foreach (var prediction in _result.Predictions)
        {
            // Check if the tag name exists in the dictionary                    
            if (prediction.TagName == "green_circle")
            {
                circle += 1;
            }
            else if (prediction.TagName == "green_gear")
            {
                gear += 1;
            }
            else if (prediction.TagName == "green_stone")
            {
                stone += 1;
            }
            else if (prediction.TagName == "green_choice")
            {
                choice += 1;
            }
            // If it does, add the value to the sum                
                        
        }
        int Sum = max(getGreemValue(circle + choice, gear, stone),getGreemValue(circle, gear + choice, stone), getGreemValue(circle, gear, stone + choice));
        return Sum;
    }

    private int getGreemValue(int circle, int gear, int stone){
        return (int)(Math.Pow(circle, 2) + Math.Pow(gear, 2) + Math.Pow(stone, 2) + (min(circle,gear,stone) * 7));        
    }

    private int min (int a, int b, int c){
        return Math.Min(Math.Min(a,b),c);
    }

    private int max (int a, int b, int c){
        return Math.Max(Math.Max(a,b),c);
    }
}
    
    

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
        evaluationData.Blue = 0;
        evaluationData.Yellow = 0;
        evaluationData.Violet = 0;
        evaluationData.Green = 0;
        evaluationData.Sum = 0;
        return evaluationData;
    }

    private int calcRed()
    {
        int redCount = 0;
        
        // Access the TagToValueMapping dictionary from the static class
        Dictionary<string, int> tagToValueMapping = MappingDictionary.WarTagToValueMapping;

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
}
    
    

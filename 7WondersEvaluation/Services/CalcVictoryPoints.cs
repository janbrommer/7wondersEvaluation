using System.Threading.Tasks.Dataflow;
using _7WondersEvaluation.Constants;

public class CalcVictoryPoints
{
    private ApiResult _result;
    public CalcVictoryPoints(ApiResult result)
    {
        // Constructor logic goes here
        _result = result;
    }

    public CalcVictoryPoints()
    {
        _result = new ApiResult();
    }

    public PlayerOutlay createPlayerOutlay(PlayersInGame playersInGame)
    {
        PlayerOutlay outlay = new PlayerOutlay
        {
            CountBlue = count("blue"),
            CountRed = count("red"),
            CountBrown = count("brown"),
            CountExpa = count("expa"),
            CountGild = count("gild"),
            CountGreen = count("green"),
            CountGrey = count("grey"),
            CountNegWarMarker = count("war_neg"),
            CountWarMarker = count("war") - count("war_neg"),
            CountYellow = count("yellow"),
            PlayersInGame = playersInGame,
            Gilds = getGild()

        };
        return outlay;
    }

    public Evaluation createEvaluationData(PlayersInGame playersInGame)
    {
        Evaluation evaluationData = new Evaluation
        {
            Red = calcRed(),
            Coins = calcCoin(),
            ExpansionStages = calcExpa(),
            Blue = calcBlue(),
            Yellow = calcYellow(),
            Violet = 0,
            Green = calcGreen(),
            PlayersInGame = playersInGame

        };
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
                redCount += tagValue; ;

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
                coinCount += tagValue; ;

            }
        }

        return coinCount / 3;

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
                expaSum += tagValue; ;

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
                Sum += tagValue; ;

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
            else if (prediction.TagName == "gild_green_choice")
            {
                choice += 1;
            }
            else if (prediction.TagName == "expa_green_choice")
            {
                choice += 1;
            }
            // If it does, add the value to the sum                

        }
        Console.WriteLine("circle: " + circle);
        Console.WriteLine("gear: " + gear);
        Console.WriteLine("stone: " + stone);
        Console.WriteLine("choice: " + choice);
        int Sum = max(getGreemValue(circle + choice, gear, stone), getGreemValue(circle, gear + choice, stone), getGreemValue(circle, gear, stone + choice));
        return Sum;
    }

    public int calcViolet(PlayersInGame pig, PlayersInGame pigLeft, PlayersInGame pigRight)
    {
        int sumViolet = 0;
        if (pigLeft == null || pigRight == null || pig == null)
        {
            throw new Exception("invalid data");
        }
        else if (pigLeft.PlayerOutlay == null || pigRight.PlayerOutlay == null || pig.PlayerOutlay == null)
        {
            throw new Exception("invalid data");
        }
        foreach (string gild in pig.PlayerOutlay.Gilds)
        {
            switch (gild)
            {
                case "gild_blue":
                    sumViolet += pigLeft.PlayerOutlay.CountBlue;
                    sumViolet += pigRight.PlayerOutlay.CountBlue;
                    break;

                case "gild_brown":
                    sumViolet += pigLeft.PlayerOutlay.CountBrown;
                    sumViolet += pigRight.PlayerOutlay.CountBrown;
                    break;

                case "gild_expa":
                    sumViolet += pigLeft.PlayerOutlay.CountExpa;
                    sumViolet += pigRight.PlayerOutlay.CountExpa;
                    sumViolet += pig.PlayerOutlay.CountExpa;
                    break;
                case "gild_expa_self":
                    //TODO: Check if all expas are build
                    sumViolet += 7;
                    break;
                case "gild_green":
                    sumViolet += pigLeft.PlayerOutlay.CountGreen;
                    sumViolet += pigRight.PlayerOutlay.CountGreen;
                    break;
                case "gild_grey":
                    sumViolet += pigLeft.PlayerOutlay.CountGrey * 2;
                    sumViolet += pigRight.PlayerOutlay.CountGrey * 2;
                    break;
                case "gild_mix":
                    sumViolet += pig.PlayerOutlay.CountGrey;
                    sumViolet += pig.PlayerOutlay.CountBrown;
                    sumViolet += pig.PlayerOutlay.CountGild;
                    break;
                case "gild_red":
                    sumViolet += pigLeft.PlayerOutlay.CountRed;
                    sumViolet += pigRight.PlayerOutlay.CountRed;
                    break;
                case "gild_war_neg":
                    sumViolet += pigLeft.PlayerOutlay.CountNegWarMarker;
                    sumViolet += pigRight.PlayerOutlay.CountNegWarMarker;
                    break;
                case "gild_yellow":
                    sumViolet += pigLeft.PlayerOutlay.CountYellow;
                    sumViolet += pigRight.PlayerOutlay.CountYellow;
                    break;
                default:
                    Console.WriteLine("Gild" + gild + "is unknown or will be handelt at an other place");
                    break;
            }
        }
        return sumViolet;
    }


    private int calcYellow()
    {
        int yellowCount = 0;
        if (_result?.Predictions == null)
        {
            return yellowCount;
        }
        foreach (var prediction in _result.Predictions)
        {
            // Check if the tag name exists in the dictionary                    
            if (prediction.TagName == "yellow_brown")
            {
                yellowCount += count("brown");
            }
            else if (prediction.TagName == "yellow_grey")
            {
                yellowCount += count("grey");
            }
            else if (prediction.TagName == "yellow_yellow")
            {
                yellowCount += count("yellow");
            }
            else if (prediction.TagName == "yellow_expa")
            {
                yellowCount += count("expa");
            }
        }
        return yellowCount;
    }

    private int count(String prefix)
    {
        int count = 0;
        if (_result?.Predictions != null)
        {
            foreach (var prediction in _result.Predictions)
            {
                // Check if the tag name exists in the dictionary                    
                if (prediction.TagName.StartsWith(prefix))
                {
                    count += 1;
                }
            }
        }
        return count;
    }

    private string[] getGild()
    {
        string[] result = new string[12];
        if (_result?.Predictions != null)
        {

            foreach (var prediction in _result.Predictions)
            {
                // Check if the tag name exists in the dictionary                    
                if (prediction.TagName.StartsWith("gild"))
                {
                    result.Append(prediction.TagName);
                }
            }
        }
        return result;
    }

    private int getGreemValue(int circle, int gear, int stone)
    {
        return (int)(Math.Pow(circle, 2) + Math.Pow(gear, 2) + Math.Pow(stone, 2) + (min(circle, gear, stone) * 7));
    }

    private int min(int a, int b, int c)
    {
        return Math.Min(Math.Min(a, b), c);
    }

    private int max(int a, int b, int c)
    {
        return Math.Max(Math.Max(a, b), c);
    }
}



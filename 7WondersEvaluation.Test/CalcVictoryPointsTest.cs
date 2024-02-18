using Xunit;
using Moq;
using _7WondersEvaluation.Constants;

namespace _7WondersEvaluation.Test;

public class CalcVictoryPointsTests
{
    private ApiResult mockResult;

    public CalcVictoryPointsTests()
    {

        // Create mock predictions
        var predictions = new List<Prediction>
        {
        };
        var mockPrediction = new Prediction
        {
            Probability = 0.8,
            TagName = "coin_1",
            BoundingBox = new BoundingBox
            {
                Left = 0,
                Top = 0,
                Width = 100,
                Height = 100
            }
        };
        var mockPrediction2 = new Prediction
        {
            Probability = 0.8,
            TagName = "coin_3",
            BoundingBox = new BoundingBox
            {
                Left = 0,
                Top = 0,
                Width = 100,
                Height = 100
            }
        };
        predictions.Add(mockPrediction2);
        mockResult = new ApiResult
        {
            Predictions = predictions,
        };
    }

    [Fact]
    public void createEvaluationDataTest()
    {

        // Arrange
        PlayersInGame mockPlayersInGame = new PlayersInGame { Game = new Game { GameName = "Test Game" }, Player = new Player { PlayerName = "Test Player" }, PositionInGame = 1 };
        var mockEval = new Evaluation
        {
            Coins = 1,
            PlayersInGame = mockPlayersInGame
        };
        var expectedResult = mockEval;

        // Act
        var calc = new CalcVictoryPoints(mockResult);
        Evaluation evaluation = calc.createEvaluationData(mockPlayersInGame);

        // Assert
        Assert.Equal(expectedResult.Coins, evaluation.Coins);
    }
}

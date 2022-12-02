using AdventOfCode.Enums;

namespace AdventOfCode.Challenges;

public class Day2 : DayBase
{
    
    private readonly string[] _inputs;

    public Day2()
    {
        _inputs = ReadFile("Day2.txt");
    }
    public override void Start()
    {
        Part1();
        Part2();
    }

    private void Part1()
    {
        long score = 0;
        foreach (var round in _inputs)
        {
            RockPaperScissorsChoice opponentChoice = ParseChoice(round[0]);
            RockPaperScissorsChoice ownChoice = ParseChoice(round[2]);

            RpsComparer comparer = new RpsComparer();

            int roundResult = comparer.Compare(opponentChoice, ownChoice);
            int choiceScore = CalculateChoiceScore(ownChoice);

            score += roundResult + choiceScore;
        }
        
        Console.WriteLine(score);
    }
    
    private void Part2()
    {
        long score = 0;
        foreach (string round in _inputs)
        {
            RockPaperScissorsChoice opponentChoice = ParseChoice(round[0]);
            RockPaperScissorsResult expectedResult = ParseExpectedResult(round[2]);
            
            
            int roundScore = expectedResult switch
            {
                RockPaperScissorsResult.Defeat => 0,
                RockPaperScissorsResult.Draw => 3,
                RockPaperScissorsResult.Win => 6
            };
            RockPaperScissorsChoice ownChoice = CalculateChoiceNeeded(opponentChoice, expectedResult);
            int choiceScore = CalculateChoiceScore(ownChoice);

            score += roundScore + choiceScore;
        }
        
        Console.WriteLine(score);
    }

    private static RockPaperScissorsChoice ParseChoice(char choice) =>
        choice switch
        {
            'A' => RockPaperScissorsChoice.Rock,
            'X' => RockPaperScissorsChoice.Rock,
            'B' => RockPaperScissorsChoice.Paper,
            'Y' => RockPaperScissorsChoice.Paper,
            'C' => RockPaperScissorsChoice.Scissors,
            'Z' => RockPaperScissorsChoice.Scissors,
            _ => throw new ArgumentOutOfRangeException(nameof(choice), choice, null)
        };

    private static RockPaperScissorsResult ParseExpectedResult(char choice) =>
        choice switch
        {
            'Y' => RockPaperScissorsResult.Draw,
            'X' => RockPaperScissorsResult.Defeat,
            'Z' => RockPaperScissorsResult.Win,
            _ => throw new ArgumentOutOfRangeException(nameof(choice), choice, null)
        };
    
    private static int CalculateChoiceScore(RockPaperScissorsChoice choice) =>
        choice switch
        {
            RockPaperScissorsChoice.Rock => 1,
            RockPaperScissorsChoice.Paper => 2,
            RockPaperScissorsChoice.Scissors => 3,
            _ => throw new ArgumentOutOfRangeException(nameof(choice), choice, null)
        };

    private static RockPaperScissorsChoice CalculateChoiceNeeded(RockPaperScissorsChoice opponent,
        RockPaperScissorsResult expectedResult)
    {
        return opponent switch
        {
            RockPaperScissorsChoice.Paper => expectedResult switch
            {
                RockPaperScissorsResult.Win => RockPaperScissorsChoice.Scissors,
                RockPaperScissorsResult.Draw => RockPaperScissorsChoice.Paper,
                RockPaperScissorsResult.Defeat => RockPaperScissorsChoice.Rock,
                _ => throw new ArgumentOutOfRangeException(nameof(expectedResult), expectedResult, null)
            },
            RockPaperScissorsChoice.Rock => expectedResult switch
            {
                RockPaperScissorsResult.Win => RockPaperScissorsChoice.Paper,
                RockPaperScissorsResult.Draw => RockPaperScissorsChoice.Rock,
                RockPaperScissorsResult.Defeat => RockPaperScissorsChoice.Scissors,
                _ => throw new ArgumentOutOfRangeException(nameof(expectedResult), expectedResult, null)
            },
            _ => expectedResult switch
            {
                RockPaperScissorsResult.Win => RockPaperScissorsChoice.Rock,
                RockPaperScissorsResult.Draw => RockPaperScissorsChoice.Scissors,
                RockPaperScissorsResult.Defeat => RockPaperScissorsChoice.Paper,
                _ => throw new ArgumentOutOfRangeException(nameof(expectedResult), expectedResult, null)
            }
        };
    }
}

internal class RpsComparer : IComparer<RockPaperScissorsChoice>
{
    public int Compare(RockPaperScissorsChoice opponent, RockPaperScissorsChoice own)
    {
        return opponent switch
        {
            RockPaperScissorsChoice.Paper => own switch
            {
                RockPaperScissorsChoice.Rock => 0,
                RockPaperScissorsChoice.Paper => 3,
                RockPaperScissorsChoice.Scissors => 6,
                _ => throw new ArgumentOutOfRangeException(nameof(own), own, null)
            },
            RockPaperScissorsChoice.Rock => own switch
            {
                RockPaperScissorsChoice.Scissors => 0,
                RockPaperScissorsChoice.Rock => 3,
                RockPaperScissorsChoice.Paper => 6,
                _ => throw new ArgumentOutOfRangeException(nameof(own), own, null)
            },
            _ => own switch
            {
                RockPaperScissorsChoice.Paper => 0,
                RockPaperScissorsChoice.Scissors => 3,
                RockPaperScissorsChoice.Rock => 6,
                _ => throw new ArgumentOutOfRangeException(nameof(own), own, null)
            }
        };
    }
}
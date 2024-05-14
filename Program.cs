using Xunit;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace PinValidator;

class Program
{
    static void Main(string[] args)
    {
        var tests = new List<string>{
            "1357",
            "246801",
            "12346",
            "123a",
            "1234a",
            "135a",
            "3456",
            "7654",
            "1135",
            "3013",
            "3013546"
        };

        List<IValidator> validators = new List<IValidator>
        {
            new LengthValidator(),
            new JustDigitsValidator(),
            new IsSequenceValidator(),
            new DuplicatesValidator()
        };

        foreach (var testString in tests)
        {
           PrintMessage(testString, RunValidators(testString, validators));
        }
    }

    static void PrintMessage(string currentTest, int result){
        Console.WriteLine($"string {currentTest} returned this validation message: {result}");
    }

    static int RunValidators(string currentTestPin, List<IValidator> validators)
    {
        var validatorsQueue = new Queue<IValidator>(validators);
        while (validatorsQueue.Count > 0)
        {
            var currentValidator = validatorsQueue.Dequeue();
            if (!currentValidator.IsValid(currentTestPin))
            {
                return currentValidator.returnNum;
            }
        }

        return currentTestPin.Length == 4 ? 0 : 1;
    }
}

public interface IValidator {
    int returnNum {get;}
    bool IsValid(string possiblePin);
}

public class LengthValidator : IValidator {
    public int returnNum { get { return 2;}}

    public bool IsValid(string possiblePin){
        return possiblePin.Length == 4 || possiblePin.Length == 6;
    }
}

public class JustDigitsValidator: IValidator {
    public int returnNum { get { return 3;}}

    public bool IsValid(string possiblePin){
        return Regex.IsMatch(possiblePin, @"^\d+$");
    }
}

public class IsSequenceValidator: IValidator {
    public int returnNum { get {return 4;}}


    public bool IsValid(string possiblePin){
        var isAscendingSequence = true;
        var isDescendingSequence = true;
        
        for (int i = 0; i < possiblePin.Length - 1; i++)
        {
            var left = int.Parse(possiblePin[i].ToString());
            var right = int.Parse(possiblePin[i+1].ToString());
            if (left + 1 != right)
            {
                isAscendingSequence = false;
            }
            if (left - 1 != right)
            {
                isDescendingSequence = false;
            }
        }
        return !isAscendingSequence && !isDescendingSequence;
    }
}

public class DuplicatesValidator: IValidator {
    public int returnNum { get {return 5;}}

    public bool IsValid(string possiblePin){
        var numberSet = new HashSet<char>(possiblePin.ToArray());
        return numberSet.Count == possiblePin.Length;
    }
}

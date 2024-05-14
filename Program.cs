using Xunit;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace PinValidator;

class Program
{
    static void Main(string[] args)
    {
        List<IValidator> validators = new List<IValidator>
        {
            new LengthValidator(),
            new JustDigitsValidator(),
        };
        var lengthValidator = new LengthValidator();
        Assert.False(lengthValidator.IsValid("13579"));
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

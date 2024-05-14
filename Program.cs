using Xunit;
namespace PinValidator;

class Program
{
    static void Main(string[] args)
    {
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
        return true;
    }
}

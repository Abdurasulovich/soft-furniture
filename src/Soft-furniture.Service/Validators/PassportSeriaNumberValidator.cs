namespace Soft_furniture.Service.Validators;

public class PassportSeriaNumberValidator
{
    public static (bool IsValid, string Message) IsTruePassportSeriaNumber(string passport)
    {
        if (passport.Length < 9) return (IsValid: false, "Passport can not be less than 9 character");

        bool IsAlphabets = false;
        byte digit = 0;
        if (char.IsUpper(passport[0]) && char.IsUpper(passport[1]))
            IsAlphabets = true;
        else return (IsValid: false, "Passport seria number is invalid!");

        for (int i = 2; i < passport.Length; i++)
        {
            if (char.IsDigit(passport[i]) == false)
                return (IsValid: false, "Passport seria number is invalid!");
            digit++;
        }
        if (IsAlphabets == false) return (IsValid: false, Message: "In Passport seria number must be least 2 alphabet!");
        if (digit < 7 || digit > 7) return (IsValid: false, Message: "In Passport seria number must be least 7 digit!");
        return (IsValid: true, "");
    }
}

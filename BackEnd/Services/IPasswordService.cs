namespace back_end.Services;

public interface IPasswordService
{
    public string ValidatePassword(string password);
    public bool IsPasswordCommon(string password);
}
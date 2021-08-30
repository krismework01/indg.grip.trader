namespace INDG.GRIP.Trader.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        ICurrentUser GetCurrentUser();

        ICurrentUser CreateUserByToken(string jwt);
    }
}
using System.Threading.Tasks;

namespace XFFirebaseAuthExample.Services
{
    public interface IFirebaseAuthenticator
    {
        Task<string> LoginWithEmailPassword(string email, string password);
    }
}

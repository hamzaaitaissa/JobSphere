namespace JobSphere.Services
{
    public interface IPasswordHasherService
    {
        (string Hash, byte[] Salt) HashPassword(string password);
        bool VerifyPassword(string passwordAttempt, string storedHash, byte[] storedSalt);
    }
}

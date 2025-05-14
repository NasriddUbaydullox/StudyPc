namespace HospitalManagment_V2.Helpers;

public interface IPasswordHasher
{
	string HashPassword(string password);

	bool VerifyHash(string password, string hash);
}

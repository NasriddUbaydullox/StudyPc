namespace HospitalManagment_V2.Services;

public interface IAuthService
{
	string GetToken(string username);
}

using HospitalManagment_V2.DataAccess;
using HospitalManagment_V2.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagment_V2.Repository;

public interface IUserRepository
{
	IQueryable<User> GetAll();

	Task<User> GetByIdAsync(int id);

	Task AddAsync(User user);

	Task UpdateAsync(User user);

	Task DeleteAsync(int id);

	Task SaveChangesAsync();

	Task<User?> GetByUsernameAsync(string username);

	IQueryable<User?> GetAllActive();
}
public class UserRepository : IUserRepository
{
	private readonly Context _context;

	public UserRepository(Context context)
	{
		_context = context;

	}

	public IQueryable<User> GetAll()
	{
		return _context.User
			.AsQueryable();
	}

	public async Task<User> GetByIdAsync(int id)
	{
		var doctor = await _context.User.FindAsync(id);

		return doctor;
	}

	public async Task AddAsync(User patient)
	{
		await _context.AddAsync(patient);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateAsync(User patient)
	{
		_context.Update(patient);
		await _context.SaveChangesAsync();
	}

	public async Task DeleteAsync(int id)
	{
		var patient = await _context.Users.FindAsync(id);
		if (patient != null)
		{
			_context.Remove(patient);
			await _context.SaveChangesAsync();
		}
	}

	public async Task SaveChangesAsync()
	{
		await _context.SaveChangesAsync();
	}


	public IQueryable<User?> GetAllActive()
	{
		return _context.User.Where(u => u.IsActive);
	}

	public async Task<User?> GetByUsernameAsync(string username)
	{
		return await _context.User
			.FirstOrDefaultAsync(u => u.Username == username && u.IsActive);
	}
}

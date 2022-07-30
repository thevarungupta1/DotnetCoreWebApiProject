using DotnetCoreWebApiProject.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetCoreWebApiProject.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _Context;

        public AuthRepository(DataContext context)
        {
            _Context = context;
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            ServiceResponse<int> response = new ServiceResponse<int>();
            
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            if(await UserExists(user.Username))
            {
                response.Success = false;
                response.Message = "User already exixts";
                return response;
            }

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _Context.Users.Add(user);
            await _Context.SaveChangesAsync();
         
            response.Data = user.Id;
            response.Message = "User registerd succesfully";

            return response;
        }

        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            var response = new ServiceResponse<string>();
            var user = await _Context.Users.FirstOrDefaultAsync(u => u.Username.ToLower().Equals(username.ToLower()));
            if(user == null)
            {
                response.Success = false;
                response.Message = "User not found";
            }
            else if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success=false;
                response.Message = "Wrong password";
            }
            else
            {
                response.Data = user.Id.ToString();
            }
            return response;
        }

        public async Task<bool> UserExists(string username)
        {
            if(await _Context.Users.AnyAsync(u => u.Username.ToLower()== username.ToLower()))
                return true;
            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }


    }
}

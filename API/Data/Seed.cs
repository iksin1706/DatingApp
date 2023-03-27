using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using API.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.TraceSource;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context){
            if(await context.Users.AnyAsync())return;

            var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");

            var opstions = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};

            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

            foreach (var user in users){
                using var hmac = new HMACSHA512();

                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
                user.PasswordSalt = hmac.Key;
                user.Photos[0].isMain=true;

                context.Users.Add(user);
            }

            await context.SaveChangesAsync();
        }
    }
}
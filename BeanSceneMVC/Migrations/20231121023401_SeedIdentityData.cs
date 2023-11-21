using BeanSceneMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeanSceneMVC.Migrations
{
    /// <inheritdoc />
    public partial class SeedIdentityData : Migration
    {
        private MigrationBuilder _migrationBuilder;
        //Role and user IDs (so you don't have to look at GUIDS)

        private Dictionary<string, string> _roleIds = new()
        {

            { "User","5aee536f-ddac-4552-b2b2-ed2e98bf2ea5"},
            { "Staff","cb0ca447-cc88-4f4c-81a9-309557a5b3a2"},
            { "Manager","7b3b07a0-f383-436f-a61f-80c6026e72ef"}
        };

        private Dictionary<string, string> _userIds = new()
        {

            { "seedManager","e190374d-6673-4ff4-bc43-88942c7755d2"},
             { "seedStaff","aa1b39eb-ef7d-4110-9087-319ad4b534e6"},
              { "seedUser","400bfbdf-00c4-4b70-b54e-56774c0d9653"},
               { "seedNoRoles","b8b3f882-39a1-4ba8-a535-3a78d9eb7ad0"}

        };

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            _migrationBuilder = migrationBuilder;
            //raw SQL is not recommended
            // migrationBuilder.Sql($"INSERT INTO AspNetRoles(col1,col2) VALUES (val1,val2);");

            //Add roles to the AspNetRoles table
            CreateRole(_roleIds["User"], "User");
            CreateRole(_roleIds["Staff"], "Staff");
            CreateRole(_roleIds["Manager"], "Manager");

            //Add roles to the AspNetUsers table
            //CreateUser(ID, username, password, email, phone, firstname, lastname)
            CreateUser(_userIds["seedUser"], "seedUser@test.com", "password", "seedUser@test.com", "0404 040 404", "Seed", "User",
                new string[] { _roleIds["User"] });
            CreateUser(_userIds["seedStaff"], "seedStaff@test.com", "password", "seedStaff@test.com", "0404 040 404", "Seed", "Staff",
                new string[] { _roleIds["Staff"] });
            CreateUser(_userIds["seedManager"], "seedManager@test.com", "password", "seedManager@test.com", "0404 040 404", "Seed", "Manager",
                new string[] { _roleIds["Staff"], _roleIds["Manager"] });
            CreateUser(_userIds["seedNoRoles"], "seedNoRoles@test.com", "password", "seedNoRoles@test.com", "0404 040 404", "Seed", "No Roles");


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //Make the migration builder available to other methods
            _migrationBuilder = migrationBuilder;

            //Delete roles

            //DeleteRole(_roleIds[""]);

            foreach (string roleId in _roleIds.Values)
            {
                DeleteRole(roleId);
            }

            foreach (string userId in _userIds.Values)
            {
                DeleteUser(userId);
            }


        }

        /// <summary>
        /// Create an identity role.
        /// </summary>
        /// <param name="id">The ID of the role</param>
        /// <param name="name">The name of the role</param>
        private void CreateRole(string id, string name)
        {
            //Todo:validation

            //Create an IdentityRole object to hold all the data
            IdentityRole role = new()
            {
                Id = id,
                Name = name

            };
            //Generate normalised name

            role.NormalizedName = name.ToUpperInvariant();

            //Generate the concurrency stamp (A random value that should change whenever a role is persisted to the store)

            role.ConcurrencyStamp = Guid.NewGuid().ToString();

            //Build query (based on the object)
            string[] fields = { nameof(role.Id), nameof(role.Name), nameof(role.NormalizedName), nameof(role.ConcurrencyStamp) };
            string[] data = { role.Id, role.Name, role.NormalizedName, role.ConcurrencyStamp };
            //Insert record into the database
            _migrationBuilder.InsertData("AspNetRoles", fields, data);
        }

        /// <summary>
        /// Create a user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>

        private void CreateUser(string id, string userName, string password, string email, string phone, string firstname, string lastname, string[]? rolesIds = null)
        {
            //Todo:validation

            //Create a user object to hold all the data
            ApplicationUser user = new()
            {
                Id = id,
                UserName = userName,
                Email = email,
                PhoneNumber = phone,
                FirstName = firstname,
                LastName = lastname
            };

            //Generate normalised username & email (to avoid duplicate users)

            user.NormalizedUserName = userName.ToUpperInvariant();
            user.NormalizedEmail = email.ToUpperInvariant();

            //Generate the concurrency stamp (A random value that must change whenever a role is persisted to the store)

            user.ConcurrencyStamp = Guid.NewGuid().ToString();

            //Generate the security stamp (A random value that must change whenever a user credentials change(password changed, login removed))

            user.SecurityStamp = Guid.NewGuid().ToString();


            //Generate password hash from the plaintext password
            PasswordHasher<ApplicationUser> passwordHasher = new();
            user.PasswordHash = passwordHasher.HashPassword(user, password);



            //Other data
            user.EmailConfirmed = true;
            user.PhoneNumberConfirmed = false;
            user.AccessFailedCount = 0;
            user.LockoutEnabled = true;
            user.LockoutEnd = null;
            user.TwoFactorEnabled = false;

            //Build query (based on the object)
            /*  string[] fields = { nameof(role.Id), nameof(role.Name), nameof(role.NormalizedName), nameof(role.ConcurrencyStamp) };
              string[] data = { role.Id, role.Name, role.NormalizedName, role.ConcurrencyStamp };*/


            string[] fields = {

                  nameof(user.Id),
                  nameof(user.UserName),
                  nameof(user.Email),
                  nameof(user.PhoneNumber),
                  nameof(user.FirstName),
                  nameof(user.LastName),
                  nameof(user.NormalizedUserName),
                  nameof(user.NormalizedEmail),
                  nameof(user.ConcurrencyStamp),
                  nameof(user.SecurityStamp),
                  nameof(user.PasswordHash),
                  nameof(user.EmailConfirmed),
                  nameof(user.PhoneNumberConfirmed),
                  nameof(user.AccessFailedCount),
                  nameof(user.LockoutEnabled),
                  nameof(user.LockoutEnd),
                  nameof(user.TwoFactorEnabled)


            };
            object[] data = {

                user.Id,
                user.UserName,
                user.Email,
                user.PhoneNumber,
                user.FirstName,
                user.LastName,
                user.NormalizedUserName,
                user.NormalizedEmail,
                user.ConcurrencyStamp,
                user.SecurityStamp,
                user.PasswordHash,
                user.EmailConfirmed,
                user.PhoneNumberConfirmed,
                user.AccessFailedCount,
                user.LockoutEnabled,
                user.LockoutEnd,
                user.TwoFactorEnabled
            };



            //Insert record into the database
            _migrationBuilder.InsertData("AspNetUsers", fields, data);

            if (rolesIds != null)
            {
                foreach (string roleId in rolesIds)
                {
                    AssignRoleToUser(user.Id, roleId);
                }
            }


        }

        private void AssignRoleToUser(string userId, string roleId)
        {
            //Build query (based on the object)
            string[] fields = { "UserId", "RoleId" };
            string[] data = { userId, roleId };
            //Insert record into the database
            _migrationBuilder.InsertData("AspNetUserRoles", fields, data);

        }

        /// <summary>
        /// Delete an identity role from the database
        /// </summary>
        /// <param name="id">The ID of the role to delete</param>
        /// 

        private void DeleteRole(string id)
        {
            //Delete record

            _migrationBuilder.DeleteData("AspNetRoles", nameof(IdentityRole.Id), id);

        }
        /// <summary>
        /// Delete an identity user from the database
        /// </summary>
        /// <param name="id">The ID of the user to delete</param>
        /// 

        private void DeleteUser(string id)
        {
            //Delete record

            _migrationBuilder.DeleteData("AspNetUsers", nameof(ApplicationUser.Id), id);

        }
    }
}

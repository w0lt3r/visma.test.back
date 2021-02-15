using System;

namespace Auth
{
    public class AppUser
    {
        public int Id { get; private set; }
        public AppUserRole Role { get; private set; }
        public string Token { get; private set; }
        public AppUser(int id, AppUserRole role, string token)
        {
            Id = id;
            Role = role;
            Token = token;
        }
    }
    public class AppUserRole
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public AppUserRole(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}

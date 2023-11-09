namespace Core.Models;

internal enum Roles
{
    guest,
    user,
    admin
}
internal class User
{
    static int _uniqueID = 1;
    Roles _role = Roles.guest;

    public int ID { get; } = _uniqueID;
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public Roles Role
    {
        get => _role;
        set
        {
            if (!Enum.IsDefined(typeof(Roles), value)) throw new NotImplementedException();
            this.Role = value;
        }
    }


}

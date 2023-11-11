using Core.Exceptions;

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
    string _username, _password;
    Roles _role = Roles.guest;

    public int ID { get; private set; } 
    public string Name { get; set; }
    public string Surname { get; set; }

    public string Username 
    {
        get => this._username;
        set
        {
            if (String.IsNullOrWhiteSpace(value) || value.Length < 3 || value.Length > 16) throw new InvalidUserameException();
            this._username = value;
        }
    }
    public string Password 
    {
        get => this._password;
        set
        {
            if (String.IsNullOrWhiteSpace(value) || value.Length < 6 || value.Length > 16) throw new InvalidPasswordException();
            
            byte flags = 0;
            // 001 1 isupper
            // 010 2 islower
            // 100 4 isdigit
            // 111 7 isready

            foreach (char item in value)
            {
                if (Char.IsUpper(item)) flags |= 1;
                else if (Char.IsLower(item)) flags |= 2;
                else if (Char.IsDigit(item)) flags |= 4;

                if (flags == 7)
                {
                    this._password = value;
                    return;
                }
            }

            string message = "Invalid password.";
            if ((flags & 1) == 0) message += " Should be at least one upper letter.";
            if ((flags & 2) == 0) message += " Should be at least one lower letter.";
            if ((flags & 4) == 0) message += " Should be at least one digit.";
            throw new InvalidPasswordException(message);
        }
    }
    public Roles Role
    {
        get => _role;
        set
        {
            if (!Enum.IsDefined(typeof(Roles), value)) throw new InvalidRoleException();
            this._role = value;
        }
    }

    public void UpdateID()
    {
        this.ID = User._uniqueID++;
    }

    public override string ToString()
    {
        return "[" + this.ID + "]: " + this.Role + " " + this.Username;
    }
}

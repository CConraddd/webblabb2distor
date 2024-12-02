namespace webblabb2distor.Core.Services;

public class UserService
{
    private readonly List<User> _users;

    public UserService()
    {
        _users = new List<User>
        {
            new User { id = 1, Username = "user1@gmail.com", Password = "password1" },
            new User { id = 2, Username = "user2@gmail.com", Password = "password2" },
            new User { id = 3, Username = "user3@gmail.com", Password = "password3" }
        };
    }
    //returns users by id
    public User GetUserById(int userId)
    {
        return _users.Find(u => u.id == userId);
    }
//returns users by username
    public User GetUserByUsername(string username)
    {
        return _users.Find(u => u.Username == username);
    }
//adds a new user
    public void addUser(User user)
    {
        int newId = _users.Max(u => u.id) + 1;
        var newUser = new User { id = newId, Username = user.Username, Password = user.Password };
        _users.Add(newUser);
    }
//validates user with users password.
    public bool validateUser(string userName, string password)
    {
        var user = GetUserByUsername(userName);
        return user != null && user.Password == password;
    }
}
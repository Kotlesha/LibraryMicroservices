using Shared.CleanArchitecture.Domain.Entities;

namespace User.Domain.Entities;

public sealed class User : AggregateRoot
{
    public string Name { get; private set; }
    public string Surname { get; private set; }
    public string Patronymic { get; private set; }
    public DateOnly BirthDate { get; private set; }
    public string Username { get; private set; }
    public string Email { get; private set; }

    private User(
        Guid id, 
        string name, 
        string surname, 
        string patronymic, 
        DateOnly birthDate, 
        string username, 
        string email) : base(id)
    {
        Name = name;
        Surname = surname;
        Patronymic = patronymic;
        BirthDate = birthDate;
        Username = username;
        Email = email;
    }

    public static User Create(
        string name,
        string surname,
        string patronymic,
        DateOnly birthDate,
        string username,
        string email)
    {
        var user = new User(
            Guid.NewGuid(), 
            name, 
            surname, 
            patronymic, 
            birthDate, 
            username, 
            email);

        user.Validate();

        return user;
    }

    public void Update(User user)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));

        Name = user.Name;
        Surname = user.Surname;
        Patronymic = user.Patronymic;
        BirthDate = user.BirthDate;
        Username = user.Username;
        Email = user.Email;
    }

    public override void Validate()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(Name, nameof(Name));
        ArgumentException.ThrowIfNullOrWhiteSpace(Surname, nameof(Surname));
        ArgumentException.ThrowIfNullOrWhiteSpace(Patronymic, nameof(Patronymic));
        ArgumentException.ThrowIfNullOrWhiteSpace(Username, nameof(Username));
        ArgumentException.ThrowIfNullOrWhiteSpace(Email, nameof(Email));
    }
}

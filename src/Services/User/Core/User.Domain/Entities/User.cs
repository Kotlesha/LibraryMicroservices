using Shared.CleanArchitecture.Domain.Entities;

namespace User.Domain.Entities;

public sealed class User : AggregateRoot
{
    public string Name { get; private set; }
    public string Surname { get; private set; }
    public string Patronymic { get; private set; }
    public DateOnly? BirthDate { get; private set; }
    public string Email { get; private set; }
    public Guid ApplicationUserId { get; private set; }

    private User(
        Guid id, 
        string name, 
        string surname, 
        string patronymic, 
        DateOnly? birthDate, 
        string email, 
        Guid applicationUserId) : base(id)
    {
        Name = name;
        Surname = surname;
        Patronymic = patronymic;
        BirthDate = birthDate;
        Email = email;
        ApplicationUserId = applicationUserId;
    }

    public static User Create(
        string name,
        string surname,
        string patronymic,
        DateOnly? birthDate,
        string email,
        Guid applicationUserId)
    {
        var user = new User(
            Guid.NewGuid(), 
            name, 
            surname, 
            patronymic, 
            birthDate, 
            email,
            applicationUserId);

        user.Validate();

        return user;
    }

    public void Update(User user)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));
        user.Validate();

        Name = user.Name;
        Surname = user.Surname;
        Patronymic = user.Patronymic;
        BirthDate = user.BirthDate;
        Email = user.Email;
    }

    protected override void Validate()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(Name, nameof(Name));
        ArgumentException.ThrowIfNullOrWhiteSpace(Surname, nameof(Surname));
        ArgumentException.ThrowIfNullOrWhiteSpace(Patronymic, nameof(Patronymic));
        ArgumentException.ThrowIfNullOrWhiteSpace(Email, nameof(Email));
    }
}

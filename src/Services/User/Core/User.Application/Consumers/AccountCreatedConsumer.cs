using MassTransit;
using Shared.CleanArchitecture.Domain.Repositories;
using Shared.Messaging.Messages.Account;
using User.Domain.Repositories;

namespace User.Application.Consumers;

using User = Domain.Entities.User;

public sealed class AccountCreatedConsumer(
    IUserRepository userRepository, IUnitOfWork unitOfWork)
    : IConsumer<AccountCreatedEvent>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Consume(ConsumeContext<AccountCreatedEvent> context)
    {
        var user = User.Create(
            context.Message.Name, 
            context.Message.Surname,
            context.Message.Patronymic,
            context.Message.BirthDate,
            context.Message.Email,
            context.Message.AccountId);

        _userRepository.AddUser(user);
        await _unitOfWork.SaveChangesAsync(context.CancellationToken);
    }
}

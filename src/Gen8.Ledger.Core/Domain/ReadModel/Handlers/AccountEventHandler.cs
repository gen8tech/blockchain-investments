using Gen8.Ledger.Core.ReadModel.Dtos;
using Gen8.Ledger.Core.ReadModel.Events;
using Gen8.Ledger.Core.Repositories;
using CQRSlite.Events;

namespace Gen8.Ledger.Core.ReadModel.Handlers
{
    public class AccountEventHandler : IEventHandler<AccountCreated>,
                                        IEventHandler<ParentAccountAssigned>,
                                        IEventHandler<AccountDeleted>
    {
        private readonly INoSqlRepository<AccountDto> _nosqlRepo;
        private readonly IRelationalRepository<AccountDto> _relationalRepo;
        public AccountEventHandler (INoSqlRepository<AccountDto> nosqlRepo, IRelationalRepository<AccountDto> relationalRepo)
        {
            _nosqlRepo = nosqlRepo;
            _relationalRepo = relationalRepo;

        }
        public void Handle(AccountCreated message)
        {
            AccountDto account = new AccountDto(message.Id, message.Title, message.Description,
                                message.Notes, message.Code, message.Type, message.CounterpartyType,
                                message.Security, message.ParentAccountId);
            _nosqlRepo.Create(account);
        }

        public void Handle(ParentAccountAssigned message)
        {
            AccountDto account = _nosqlRepo.FindByAggregateId(message.Id);
            account.ParentAccountId = message.ParentAccountId;

            _nosqlRepo.Update(account.UniqueId, account);
        }

        public void Handle(AccountDeleted message)
        {
            AccountDto account = _nosqlRepo.FindByAggregateId(message.Id);
            _nosqlRepo.Remove(account.UniqueId);
        }
    }
}

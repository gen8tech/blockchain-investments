using System;
using Gen8.Ledger.Core.Domain;
using Gen8.Ledger.Core.Infrastructure.Domain;

namespace Gen8.Ledger.Core.ReadModel.Dtos
{
    public class AccountDto : BaseEntity
    {
        public Guid AggregateId;
        public string Title;
        public string Description;
        public string Notes;
        public string Code;
        public AccountType Type;
        public CounterpartyType CounterpartyType;
        public Security Security;
        public string ParentAccountId;
        public AccountDto() {}
        public AccountDto(Guid id, string title, string description, string notes, string code,
                        AccountType type, CounterpartyType counterpartyType, Security security, string parentAccountId)
            {
                AggregateId = id;
                Title = title;
                Description = description;
                Notes = notes;
                Code = code;
                Type = type;
                CounterpartyType = counterpartyType;
                Security = security;
                ParentAccountId = parentAccountId;
            }
    }
}

using System;
using Gen8.Ledger.Core.Domain;

namespace Gen8.Ledger.Core.WriteModel.Commands
{
    public class CreateAccount : BaseCommand
	{
        public readonly string UserId;
        public readonly string Title;
        public readonly string Description;
        public readonly string Notes;
        public readonly string Code;
        public readonly AccountType Type;
        public readonly CounterpartyType CounterpartyType;
        public readonly Security Security;
        public readonly string ParentAccountId;
        public CreateAccount(Guid id, string userId, string title, string description, string notes, string code,
                             AccountType type, CounterpartyType counterpartyType, Security security, string parentAccountId)
        {
            Id = id;
            UserId = userId;
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

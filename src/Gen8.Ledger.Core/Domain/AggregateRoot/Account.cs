using System;
using Gen8.Ledger.Core.ReadModel.Events;
using CQRSlite.Domain;

namespace Gen8.Ledger.Core.Domain
{
    public class Account : AggregateRoot
    {
        private string _title;
        private string _description;
        private string _notes;
        private string _code;
        private AccountType _type;
        private CounterpartyType _counterpartyType;
        private Security _security;
        private string _parentAccountId;

        public void AddParentAccount(Guid id, string parentAccountId)
        {
            Id = id;
            _parentAccountId = parentAccountId;

            ApplyChange(new ParentAccountAssigned(id, parentAccountId));
        }

        public void DeleteAccount(Guid id, string userId)
        {
            Id = id;

            ApplyChange(new AccountDeleted(id, userId));
        }

        private Account() {}
        public Account(Guid id, string title, string description, string notes, string code, AccountType type,
                        CounterpartyType counterpartyType, Security security, string parentAccountId)
        {
            Id = id;
            _title = title;
            _description = description;
            _notes = notes;
            _code = code;
            _type = type;
            _counterpartyType = counterpartyType;
            _security = security;
            _parentAccountId = parentAccountId;

            ApplyChange(new AccountCreated(id, title, description, notes, code, type, counterpartyType, security, parentAccountId));
        }
    }
    public enum AccountType
    {
           Asset = 1,
           Liability = 2,
           Equity = 3,
           Revenue = 4,
           Expense = 5
    }
    public enum CounterpartyType
    {
        OwnProperty = 1,
        Bank = 2,
        Brokerage = 3,
        OtherService = 4
    }
}

using System;

namespace Gen8.Ledger.Core.WriteModel.Commands
{
    public class DeleteAccount : BaseCommand
	{
        public readonly string UserId;
        public DeleteAccount(Guid id, string userId)
        {
            Id = id;
            UserId = userId;
        }
	}
}

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Gen8.Ledger.Core.Infrastructure.Domain
{
    public abstract class BaseEntity
    {
        private string _objectId = string.Empty;
        [BsonId]
        public ObjectId ObjectId { get; set; }
        public string UniqueId
        {
            get
            {
                if (!this.ObjectId.ToString().Equals("000000000000000000000000"))
                    _objectId = this.ObjectId.ToString();

                return _objectId;
            }
            set
            {
                _objectId = value;
            }
        }
    }
}

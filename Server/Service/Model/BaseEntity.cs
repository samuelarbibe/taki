using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
    public class BaseEntity
    {
        private int _id;

        [DataMember]
        public int Id
        {
            get => _id;
            set => _id = value;
        }
    }
}
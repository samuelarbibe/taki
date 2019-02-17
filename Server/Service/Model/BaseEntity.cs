using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
    public class BaseEntity
    {
        [DataMember]
        public int Id { get; set; }
    }
}
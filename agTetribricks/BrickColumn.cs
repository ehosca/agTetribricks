using System.Collections.Generic;
using System.Runtime.Serialization;

namespace agTetribricks
{
    [DataContract]
    public class BrickColumn
    {
        [DataMember]
        internal List<Brick> Bricks { get; set; }

        public BrickColumn()
        {
            Bricks = new List<Brick>();
        }
    }
}
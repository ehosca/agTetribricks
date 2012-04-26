using System.Runtime.Serialization;
using System.Windows.Media;

namespace agTetribricks
{
    [DataContract]
    internal class Brick
    {
        public Brick()
        {
        }

        public Brick(int row, int column, Color color)
        {
            Row = row;
            Column = column;
            Color = color;
        }


        [DataMember]
        public int Row { get; set; }

        [DataMember]
        public int Column { get; set; }


        [DataMember]
        public Color Color { get; set; }
    }
}
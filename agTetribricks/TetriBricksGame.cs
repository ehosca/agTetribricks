using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace agTetribricks
{
    [DataContract]
    internal class TetriBricksGame //: ICloneable
    {
        private List<BrickColumn> _columns = new List<BrickColumn>();

        public List<BrickColumn> Columns
        {
            get { return _columns; }
            set { _columns = value; }
        }

        public List<Brick> Bricks
        {
            get
            {
                var bricks = new List<Brick>();
                foreach (BrickColumn bc in Columns)
                    bricks.AddRange(bc.Bricks);

                return bricks;
            }
        }

        public int Score { get; set; }

        public bool IsGameOver
        {
            get
            { return Columns.All(bc => !bc.Bricks.Any(b => GetAdjacentBricks(b).Count > 0)); }
        }

        public List<Brick> GetAdjacentBricks(Brick brick)
        {
            var foundBricks = new List<Brick>();
            List<Brick> bricksToSearch = Bricks;

            FindNeighbors(brick, foundBricks, bricksToSearch);

            return foundBricks;
        }

        private void FindNeighbors(Brick brick, List<Brick> foundBricks, List<Brick> bricksToSearch)
        {
            IEnumerable<Brick> neighbors = from b in bricksToSearch
                                           where b.Color == brick.Color
                                                 && (
                                                        ((Math.Abs(b.Row - brick.Row) == 1) &&
                                                         (b.Column == brick.Column))
                                                        ||
                                                        ((Math.Abs(brick.Column - b.Column) == 1) &&
                                                         (brick.Row == b.Row))
                                                    )
                                           select b;

            foreach (Brick b in neighbors.Where(b => !foundBricks.Contains(b)))
            {
                foundBricks.Add(b);
                FindNeighbors(b, foundBricks, bricksToSearch);
            }
        }

        public void RemoveBricks(List<Brick> bricksToRemove)
        {
            Score = (int) Math.Pow(bricksToRemove.Count, 2);

            //remove bricks 

            foreach (Brick b in bricksToRemove)
                for (int columnIndex = Columns.Count - 1; columnIndex >= 0; columnIndex--)
                    for (int brickIndex = Columns[columnIndex].Bricks.Count - 1; brickIndex >= 0; brickIndex--)
                    {
                        Brick brick = Columns[columnIndex].Bricks[brickIndex];
                        if (b.Row == brick.Row && b.Column == brick.Column && b.Color == brick.Color)
                        {
                            Columns[columnIndex].Bricks.RemoveAt(brickIndex);
                            if (Columns[columnIndex].Bricks.Count == 0)
                                Columns.RemoveAt(columnIndex);
                        }
                    }
            //adjust row/colum values
            foreach (BrickColumn bc in Columns)
                bc.Bricks.ForEach(delegate(Brick b)
                                      {
                                          b.Column = Columns.IndexOf(bc);
                                          b.Row = bc.Bricks.IndexOf(b);
                                      });
        }

        public object Clone()
        {
            var clone = new TetriBricksGame();
            foreach (BrickColumn bc in Columns)
            {
                var bcClone = new BrickColumn();
                foreach (Brick b in bc.Bricks)
                {
                    var bClone = new Brick(b.Row, b.Column, b.Color);
                    bcClone.Bricks.Add(bClone);
                }
                clone.Columns.Add(bcClone);
            }

            return clone;
        }

        public static bool operator ==(TetriBricksGame a, TetriBricksGame b)
        {
            return a.Bricks.Count == b.Bricks.Count;
        }

        public static bool operator !=(TetriBricksGame a, TetriBricksGame b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return Bricks.Count;
        }

        public override bool Equals(object obj)
        {
            if (obj is TetriBricksGame)
                return Bricks.Count == ((TetriBricksGame) obj).Bricks.Count;
            return false;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace agTetribricks
{
    public class TetribricksGameController
    {
        private static readonly Random _rnd = new Random();
        private readonly List<TetriBricksGame> _moveHistory = new List<TetriBricksGame>();
        private int _currentMove;


        public TetribricksGameController(int rows, int columns)
        {
            CreateNewGame(rows, columns);
        }

        public TetribricksGameController()
        {
            CreateNewGame();
        }

        public int Score
        {
            get { throw new NotImplementedException(); }
        }

        internal TetriBricksGame CurrentGame
        {
            get { return _moveHistory[_currentMove]; }
        }

        public void CreateNewGame()
        {
            CreateNewGame(15, 15);
        }

        public void CreateNewGame(int rows, int columns)
        {
            _moveHistory.Clear();

            var game = new TetriBricksGame();

            for (int i = 0; i < rows; i++)
            {
                var bc = new BrickColumn();
                //bc.Game = game;
                for (int j = 0; j < columns; j++)
                {
                    bc.Bricks.Add(new Brick(i, j, GetRandomColor()));
                }
                game.Columns.Add(bc);
            }
            _moveHistory.Add((TetriBricksGame) game.Clone());
            _currentMove = 0;
        }

        private Color GetRandomColor()
        {
            int color = _rnd.Next(4);

            switch (color)
            {
                case 0:
                    return Colors.Red;
                case 1:
                    return Colors.Blue;
                case 2:
                    return Colors.Green;
                default:
                    return Colors.Yellow;
            }
        }

        internal bool RemoveBrick(Brick brick)
        {
            // is there a future ?
            if (_moveHistory.Count - 1 > _currentMove)
            {
                _moveHistory.RemoveRange(_currentMove + 1, (_moveHistory.Count - 1 - _currentMove));
                _currentMove = _moveHistory.Count - 1;
            }

            List<Brick> bricks = CurrentGame.GetAdjacentBricks(brick);

            if (bricks.Count > 1)
            {
                var gameStep = (TetriBricksGame) CurrentGame.Clone();

                gameStep.RemoveBricks(bricks);
                gameStep.Score = gameStep.Score + CurrentGame.Score;

                _moveHistory.Add(gameStep);
                _currentMove = _moveHistory.Count - 1;

                return true;
            }
            else
                return false;
        }

        internal TetriBricksGame Previous()
        {
            _currentMove--;

            if (_currentMove < 0)
                _currentMove = 0;


            return CurrentGame;
        }

        internal TetriBricksGame Next()
        {
            _currentMove++;
            if (_currentMove > _moveHistory.Count - 1)
                _currentMove = _moveHistory.Count - 1;

            return CurrentGame;
        }
    }
}
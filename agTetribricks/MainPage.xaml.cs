using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using agTetribricks.TbScoreService;

namespace agTetribricks
{
    public partial class MainPage : UserControl
    {
        //http://localhost:8089/ScoreService.asmx
        //http://www.hosca.com/webservices/tetribricksscoreservice/tetribricksscoreservice.asmx


        private readonly ObservableCollection<TbScore> _highScoresCollection = new ObservableCollection<TbScore>();
        private readonly Random _rand = new Random();
        private int _currentPoints;
        private int _currentScore;
        private TetribricksGameController _gameController;

        private int _highScore;

        public MainPage()
        {
            InitializeComponent();
        }


        public ObservableCollection<TbScore> HighScores
        {
            get { return _highScoresCollection; }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            DisplayGame(_gameController.Previous());
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            DisplayGame(_gameController.Next());
        }

        private void btnReload_Click(object sender, RoutedEventArgs e)
        {
            panelSubmitHighScore.Visibility = Visibility.Collapsed;
            buttonSubmitScore.IsEnabled = true;
            NewGame(15);
            GetLatestHighScores();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            NewGame(15);
            GetLatestHighScores();
        }


        private void NewGame(int size)
        {
            txtStatus.Text = "";
            _gameController = new TetribricksGameController(size, size);

            DisplayGame(_gameController.CurrentGame);

            _currentScore = 0;
            _currentPoints = 0;
            UpdateScores();

        }


        private Color GetRandomColor()
        {
            int color = _rand.Next(4);

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

        private void UpdateScores()
        {
            Points.Text = _currentPoints > 0
                              ? string.Format("{0:##,###}", _currentPoints)
                              : string.Format("{0}", _currentPoints);
            Score.Text = _currentScore > 0
                             ? string.Format("{0:##,###}", _currentScore)
                             : string.Format("{0}", _currentScore);
        }

        private void RandomGame(int size)
        {
            gridPuzzle.RenderTransform = null;
            gridPuzzle.Children.Clear();

            for (int i = 0; i < size; i++)
            {
                var stackPanel = new StackPanel {Orientation = Orientation.Vertical};
                for (int j = 0; j < size; j++)
                {
                    stackPanel.Children.Add(new Rectangle
                                                {
                                                    Margin = new Thickness(2),
                                                    MinHeight = 20,
                                                    MinWidth = 20,
                                                    Opacity = 0.8,
                                                    Fill = new SolidColorBrush(GetRandomColor())
                                                });
                }
                gridPuzzle.Children.Add(stackPanel);
            }

            var rotate = new RotateTransform {Angle = (-180)};
            gridPuzzle.RenderTransformOrigin = new Point(0.5, 0.5);
            gridPuzzle.RenderTransform = rotate;
        }


        private void DisplayGame(TetriBricksGame game)
        {
            _currentScore = game.Score;

            gridPuzzle.RenderTransform = null;
            gridPuzzle.Children.Clear();

            game.Columns.ForEach(brickColumn =>
                                     {
                                         var stackPanel = new StackPanel {Orientation = Orientation.Vertical};
                                         brickColumn.Bricks.ForEach(brick =>
                                                                        {
                                                                            var lbl = new Rectangle
                                                                                          {
                                                                                              Margin = new Thickness(2),
                                                                                              MinHeight = 20,
                                                                                              MinWidth = 20,
                                                                                              Opacity = 0.8,
                                                                                              Tag = brick,
                                                                                              Fill = 
                                                                                                  new SolidColorBrush(
                                                                                                  brick.Color)
                                                                                          };
                                                                            lbl.MouseEnter += lbl_MouseEnter;
                                                                            lbl.MouseLeave += lbl_MouseLeave;
                                                                            lbl.MouseLeftButtonDown +=
                                                                                lbl_MouseLeftButtonDown;
                                                                            stackPanel.Children.Add(lbl);
                                                                        }
                                             );
                                         gridPuzzle.Children.Add(stackPanel);
                                     }
                );

            var rotate = new RotateTransform {Angle = (-180)};
            gridPuzzle.RenderTransformOrigin = new Point(0.5, 0.5);
            gridPuzzle.RenderTransform = rotate;

            UpdateScores();
        }

        private string GetColorName(Color color)
        {
            if (color.Equals(Colors.Yellow))
                return "yellow";
            if (color.Equals(Colors.Red))
                return "red";
            if (color.Equals(Colors.Blue))
                return "blue";
            if (color.Equals(Colors.Green))
                return "green";

            return "";
        }

        private void lbl_MouseLeave(object sender, MouseEventArgs e)
        {
            var brick = (Brick) ((Rectangle) sender).Tag;

            GetAllRectangles()
                .Where(r => _gameController.CurrentGame.GetAdjacentBricks(brick).Contains((Brick)r.Tag))
                .ToList()
                .ForEach(r => r.Opacity = 0.8);

            _currentPoints = 0;

            UpdateScores();
        }

        private void lbl_MouseEnter(object sender, MouseEventArgs e)
        {
            var brick = (Brick)((Rectangle)sender).Tag;

            List<Brick> foundBricks = _gameController.CurrentGame.GetAdjacentBricks(brick);

            if (foundBricks.Count > 1)
            {
                var rectangles = GetAllRectangles().Where(r => foundBricks.Contains((Brick)r.Tag)).ToList();
                rectangles.ToList().ForEach(r => r.Opacity = 1);

                _currentPoints = (int)Math.Pow(foundBricks.Count, 2);
            }
            else
                _currentPoints = 0;

            UpdateScores();
        }


        private List<Rectangle> GetAllRectangles()
        {
            var allLabels = new List<Rectangle>();
            foreach (StackPanel panel in gridPuzzle.Children)
                foreach (Rectangle lbl in panel.Children)
                    allLabels.Add(lbl);

            return allLabels;
        }



        private void lbl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var brick = (Brick) ((Rectangle) sender).Tag;

            if (_gameController.RemoveBrick(brick))
            {
                DisplayGame(_gameController.CurrentGame);

                if (_gameController.CurrentGame.IsGameOver)
                {
                    if (_currentScore > _highScore)
                    {
                        _highScore = _currentScore;
                        //this.panelSubmitHighScore.Visibility = Visibility.Visible;
                        panelSubmitHighScore.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        txtStatus.Text = "No more moves left!";
                    }
                }
            }
        }


        private void GetLatestHighScores()
        {
            lblHallOfFame.Text = "Loading Hall of Fame...";

            var ss = new ScoreServiceClient();
            //var ss = new ScoreServiceSoapClient();
            ss.GetLatestScoresCompleted += ss_GetLatestScoresCompleted;
            ss.GetLatestScoresAsync();
        }


        private void ss_GetLatestScoresCompleted(object sender, GetLatestScoresCompletedEventArgs e)
        {
            TbScore[] scores = e.Result.ToArray();
            var highScoreValue = scores[scores.Length - 1].ScoreValue;
            
            _highScore = highScoreValue.HasValue ? highScoreValue.Value : 0;
            _highScoresCollection.Clear();

            scores.ToList().ForEach(_highScoresCollection.Add);

            listViewHighScores.ItemsSource = HighScores;
            lblHallOfFame.Text = "Hall of Fame";
        }

        private void checkBoxNotify_Checked(object sender, RoutedEventArgs e)
        {
        }

        private void buttonSubmitScore_Click(object sender, RoutedEventArgs e)
        {
            ((Button) sender).IsEnabled = false;
            var score = new TbScore
                            {
                                ScoreValue = _highScore,
                                UserName = textUserName.Text,
                                ScoreDate = DateTime.Now
                            };
            if ((bool) checkBoxNotify.IsChecked)
                score.EmailAddress = textBoxEmail.Text;

            var ss = new ScoreServiceClient();
            //var ss = new ScoreServiceSoapClient();

            ss.SaveScoreCompleted += ss_SaveScoreCompleted;
            ss.SaveScoreAsync(score);
        }

        private void ss_SaveScoreCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                txtStatus.Text = "High Score sucessfully submitted to the Hall Of Fame !";
                GetLatestHighScores();
                panelSubmitHighScore.Visibility = Visibility.Collapsed;
                buttonSubmitScore.IsEnabled = true;
            }
            else
            {
                txtStatus.Text = e.Error.Message;
            }
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            Canvas.SetZIndex(panelHighScores, 2);
            //animShowSlideHighScores.Begin();
            //imageHighScore.Source = GetImageFromPath("images/right.png");
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            Canvas.SetZIndex(panelHighScores, 1);
            //animHideSlideHighScores.Begin();
            //imageHighScore.Source = GetImageFromPath("images/left.png");
        }

        private static BitmapImage GetImageFromPath(string imagePath)
        {
            return new BitmapImage(new Uri(imagePath, UriKind.Relative));
        }


        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            panelHighScores.Visibility = Visibility.Collapsed;
        }
    }
}
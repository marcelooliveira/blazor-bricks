using System.Collections.Generic;

namespace BlazorBricks.Core
{
    public class GameManager : IView
    {
        private static GameManager instance = null;
        private static BricksPresenter presenter = null;
        private static BoardViewModel currentBoard = null;

        private GameManager()
        {
            currentBoard = new BoardViewModel();
            currentBoard.Bricks = new BrickViewModel[] { };

            presenter = new BricksPresenter(this);
        }

        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameManager();
                }
                return instance;
            }
        }

        public BricksPresenter Presenter
        {
            get { return presenter; }
        }

        public BoardViewModel CurrentBoard
        {
            get { return currentBoard; }
        }

        public void InitializeBoard()
        {
            //presenter.InitializeBoard();
            //presenter.IsGameOver = false;
        }

        public void DisplayBoard(string arrayString, IBrick[,] brickArray, int width, int height)
        {
            currentBoard.Bricks = GetBricksArray(height, width, brickArray);
        }

        public void DisplayScore(int score, int hiScore, int lines, int level, BlazorBricks.Core.Shapes.IShape next)
        {
            currentBoard.Score = score;
            currentBoard.HiScore = hiScore;
            currentBoard.Lines = lines;
            currentBoard.Level = level;
            currentBoard.Next = GetBricksArray(next.ShapeArray.GetUpperBound(1) + 1, next.ShapeArray.GetUpperBound(0) + 1, next.ShapeArray);
        }

        private BrickViewModel[] GetBricksArray(int rowCount, int colCount, IBrick[,] array)
        {
            var bricksList = new List<BrickViewModel>();

            for (var row = 0; row < rowCount; row++)
            {
                for (var col = 0; col < colCount; col++)
                {
                    var b = array[col, row];
                    if (b != null)
                    {
                        bricksList.Add(new BrickViewModel()
                        {
                            Row = row,
                            Col = col,
                            Color = b.Color.ToString()
                        });
                    }
                    else
                    {
                        bricksList.Add(new BrickViewModel()
                        {
                            Row = row,
                            Col = col,
                            Color = "0"
                        });
                    }
                }
            }
            return bricksList.ToArray();
        }

        public void GameOver()
        {
            presenter.IsGameOver = true;
        }
    }

    public class BrickViewModel
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public string Color { get; set; }
    }

    public class BoardViewModel
    {
        public BoardViewModel()
        {
        }

        public BrickViewModel[] Bricks { get; set; }
        public int Score { get; set; }
        public int HiScore { get; set; }
        public int Lines { get; set; }
        public int Level { get; set; }
        public BrickViewModel[] Next { get; set; }
    }
}

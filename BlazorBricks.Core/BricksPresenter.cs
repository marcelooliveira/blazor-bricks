using System;
using System.Threading;
using System.Threading.Tasks;
using BlazorBricks.Core.Shapes;

namespace BlazorBricks.Core
{
    public class BricksPresenter : IPresenter
    {
        public event EventHandler Updated;

        private IView view;
        private BricksBoard BricksBoard;
        private TimeSpan accumulatedTimeSpan = TimeSpan.FromMilliseconds(0);
        private CancellationTokenSource cancellationTokenSource;
        private const int TICK_MS_INTERVAL = 30;
        private const int PROCESS_NEXT_MOVEMENT_MS_INTERVAL = 150;
        private Object thisLock = new Object();

        public bool IsGameOver { get; set; } = true;

        public BricksPresenter(IView view)
        {
            this.view = view;
            BricksBoard = new BricksBoard(this);
            BricksBoard.Updated += (obj, e) =>
            {
                Updated?.Invoke(this, e);
            };
        }

        public async Task StartTickLoop()
        {
            IsGameOver = false;
            cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;
            Task task = Task.Run(async () =>
               {
                   while (!IsGameOver)
                   {
                       bool processMove = await ExecuteTickLoop();
                       Tick(processMove);
                   }
               }, token);

            await task;
        }

        private async Task<bool> ExecuteTickLoop()
        {
            await Task.Delay(TICK_MS_INTERVAL);
            bool processMove = false;

            lock (thisLock)
            {
                processMove = BricksBoard.DownPressed;
                accumulatedTimeSpan = accumulatedTimeSpan.Add(TimeSpan.FromMilliseconds(TICK_MS_INTERVAL));
                if (accumulatedTimeSpan.TotalMilliseconds >= PROCESS_NEXT_MOVEMENT_MS_INTERVAL)
                {
                    processMove = true;
                    accumulatedTimeSpan = TimeSpan.FromMilliseconds(0);
                }
            }
            return processMove;
        }

        public IView View
        {
            get { return view; }
            set { view = value; }
        }

        public void UpdateBoardView(string ArrayString, IBrick[,] brickArray, int width, int height)
        {
            if (view == null)
                throw new ArgumentNullException("View");

            view.DisplayBoard(ArrayString, brickArray, width, height);
        }

        public void HighlightCompletedRow(int row)
        {
            if (view == null)
                throw new ArgumentNullException("View");
        }

        public void UpdateScoreView(int score, int hiScore, int lines, int level, IShape next)
        {
            if (view == null)
                throw new ArgumentNullException("View");

            view.DisplayScore(score, hiScore, lines, level, next);
        }

        public bool MoveLeft()
        {
            if (IsGameOver) return false;
            return BricksBoard.MoveLeft();
        }

        public bool MoveRight()
        {
            if (IsGameOver) return false;
            return BricksBoard.MoveRight();
        }
        
        public bool MoveDown()
        {
            if (IsGameOver) return false;
            return BricksBoard.MoveDown();
        }

        public bool Rotate90()
        {
            if (IsGameOver) return false;
            return BricksBoard.Rotate90();
        }

        public bool Rotate270()
        {
            if (IsGameOver) return false;
            return BricksBoard.Rotate270();
        }

        public void InitializeBoard()
        {
            BricksBoard.InitializeArray();
        }

        public void GameOver()
        {
            cancellationTokenSource.Cancel();
            view.GameOver();
        }

        public void Tick(bool processMove = false)
        {
            if (processMove)
            {
                BricksBoard.ProcessNextMove();
            }
            Updated?.Invoke(this, new EventArgs());
        }

        public int Width
        {
            get { return BricksBoard.Width; }
        }

        public int Height
        {
            get { return BricksBoard.Height; }
        }

        public int Level
        {
            get { return BricksBoard.Level; }
        }
    }

    public enum ShapeCode
    {
        J = 1,
        I,
        L,
        O,
        S,
        T,
        Z
    }
}

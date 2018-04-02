using System;
using System.Collections.Generic;
using System.Text;
using MVCBricks.Core.Shapes;

namespace MVCBricks.Core
{
    public class BricksPresenter : IPresenter
    {
        private IView view;
        private BricksBoard BricksBoard;

        public BricksPresenter(IView view)
        {
            this.view = view;
            BricksBoard = new BricksBoard(this);
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

            view.HighlightCompletedRow(row);
        }

        public void UpdateScoreView(int score, int hiScore, int lines, int level, IShape next)
        {
            if (view == null)
                throw new ArgumentNullException("View");

            view.DisplayScore(score, hiScore, lines, level, next);
        }

        public bool MoveLeft()
        {
            return BricksBoard.MoveLeft();
        }

        public bool MoveRight()
        {
            return BricksBoard.MoveRight();
        }

        public bool MoveDown()
        {
            return BricksBoard.MoveDown();
        }

        public bool Rotate90()
        {
            return BricksBoard.Rotate90();
        }

        public bool Rotate270()
        {
            return BricksBoard.Rotate270();
        }

        public void InitializeBoard()
        {
            BricksBoard.InitializeArray();
        }

        public void GameOver()
        {
            view.GameOver();
        }

        public void Tick()
        {
            BricksBoard.ProcessNextMove();
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

        public bool IsPlaying
        {
            get { return BricksBoard.IsPlaying; }
        }
    }

    public enum ShapeCodes
    {
        I = 1,
        J,
        L,
        O,
        S,
        T,
        Z
    }
}

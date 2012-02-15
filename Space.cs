using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace RandomWalker
{
    public class Space
    {
        public int DimX { get; set; }
        public int DimY { get; set; }

        public List<List<bool>> space;

        private bool CanPut(Point p)
        {
            if (p.X >= DimX)
                return false;

            if (p.Y >= DimY)
                return false;

            if (p.X < 0)
                return false;

            if (p.Y < 0)
                return false;

            if (space[p.X][p.Y])
                return false;

            return true;
        }

        private bool CanClear(Point p)
        {
            if (p.X >= DimX)
                return false;

            if (p.Y >= DimY)
                return false;

            if (p.X < 0)
                return false;

            if (p.Y < 0)
                return false;

            if (!space[p.X][p.Y])
                return false;

            return true;
        }

        public void Put(Point p)
        {
            space[p.X][p.Y] = true;
        }

        private void Clear(Point p)
        {
            space[p.X][p.Y] = false;
        }

        public bool CanMove(Point p, Direction d)
        {
            Point p2;
            switch (d)
            {
                case Direction.Left:
                    p2 = new Point(p.X - 1, p.Y);
                    return (CanClear(p) && CanPut(p2));
                case Direction.Right:
                    p2 = new Point(p.X + 1, p.Y);
                    return (CanClear(p) && CanPut(p2));
                case Direction.Up:
                    p2 = new Point(p.X, p.Y - 1);
                    return (CanClear(p) && CanPut(p2));
                case Direction.Down:
                    p2 = new Point(p.X, p.Y + 1);
                    return (CanClear(p) && CanPut(p2));
                default:
                    return false;
            }
        }

        public Point Move(Point p, Direction d)
        {
            if (!CanMove(p, d))
                return p;

            Point p2;

            switch (d)
            {
                case Direction.Left:
                    p2 = new Point(p.X - 1, p.Y);
                    Clear(p);
                    Put(p2);
                    return p2;
                case Direction.Right:
                    p2 = new Point(p.X + 1, p.Y);
                    Clear(p);
                    Put(p2);
                    return p2;
                case Direction.Up:
                    p2 = new Point(p.X, p.Y - 1);
                    Clear(p);
                    Put(p2);
                    return p2;
                case Direction.Down:
                    p2 = new Point(p.X, p.Y + 1);
                    Clear(p);
                    Put(p2);
                    return p2;
                default:
                    return p;
            }
        }

        public bool HasNeighbours(Point p)
        {
            return
              (
               (p.X != DimX - 1 && !CanPut(new Point(p.X + 1, p.Y))) ||
               (p.X != 0 && !CanPut(new Point(p.X - 1, p.Y))) ||
               (p.Y != DimY - 1 && !CanPut(new Point(p.X, p.Y + 1))) ||
               (p.Y != 0 && !CanPut(new Point(p.X, p.Y - 1)))
               );
        }

        public bool CanMove(Point p)
        {
            return CanMove(p, Direction.Left) || CanMove(p, Direction.Right) ||
                CanMove(p, Direction.Up) || CanMove(p, Direction.Down);
        }

        public Space(int width, int height)
        {
            DimX = width;
            DimY = height;

            space = new List<List<bool>>();

            for (int i = 0; i < DimY; i++) 
            {
                space.Add(new List<bool>());
                for (int j = 0; j < DimX; j++)
                {
                    space[i].Add(false);
                }
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < space.Count; i++)
            {
                for (int j = 0; j < space[i].Count; j++)
                {
                    if (space[i][j])
                        sb.Append("+");
                    else
                        sb.Append("-");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public void ToImage(string path)
        {
            using (Bitmap image = new Bitmap(DimX, DimY))
            {
                Graphics g = Graphics.FromImage((Image)image);
               
                Pen p = new Pen(Color.Black, 1);

                for (int i = 0; i < DimX; i++)
                {
                    for (int j = 0; j < DimY; j++)
                    {
                        if (space[i][j])
                            g.FillRectangle(p.Brush, i, j, 1, 1);
                    }
                }

                using (FileStream memStream = File.Create(path))
                {
                    image.Save(memStream, ImageFormat.Png);
                }
            }

        }
    }
}

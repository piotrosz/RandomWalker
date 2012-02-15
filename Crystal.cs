using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RandomWalker
{
    public class Crystal
    {
        private Space space;
        private CrystalSettings settings;

        public Crystal(CrystalSettings settings)
        {
            this.settings = settings;
            space = new Space(settings.SpaceWidth, settings.SpaceHeight);
        }

        public void Run()
        {
            Random random = RandomSeedGenerator.GetRandom();
            space.Put(settings.InitialPoint);

            for (int i = 0; i < settings.NumberOfWalkers; i++)
            {
                Console.Write("\r{0} of {1}", i, settings.NumberOfWalkers);
                Direction wall = (Direction)random.Next(0, 4);

                Point p = null;
                switch (wall)
                {
                    case Direction.Left:
                        p = new Point(0, random.Next(space.DimY - 1));
                        break;
                    case Direction.Up :
                        p = new Point(random.Next(space.DimX - 1), 0);
                        break;
                    case Direction.Down :
                        p = new Point(random.Next(space.DimX - 1), space.DimY - 1);
                        break;
                    case Direction.Right :
                        p = new Point(space.DimX - 1, random.Next(space.DimY - 1));
                        break;
                    default :
                        break;
                }

                space.Put(p);

                while(space.CanMove(p) && !space.HasNeighbours(p))
                {
                    p = space.Move(p, (Direction)random.Next(0, 4));
                }
            }

            space.ToImage(settings.OutputFileName);
        }
    }
}

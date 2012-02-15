using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RandomWalker
{
    public class CrystalSettings
    {
        public Point InitialPoint { get; set; }
        public int NumberOfWalkers { get; set; }
        public int SpaceWidth { get; set; }
        public int SpaceHeight { get; set; }
        public string OutputFileName { get; set; }
    }
}

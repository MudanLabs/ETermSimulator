using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETermSimulator
{
    public struct Point
    {
        public Point(Int16 row, Int16 column)
        {
            _Row = row;
            _Column = column;
        }
        private Int16 _Row;
        public Int16 Row { get { return _Row; } private set { _Row = value; } }

        private Int16 _Column;
        public Int16 Column { get { return _Column; } private set { _Column = value; } }
    }
}

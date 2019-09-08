using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Officer_Project
{
    class QueueManagement
    {
        public static int Customers { get; set; }
        public static Mutex _mutex = new Mutex();
        public static int _timeInside;
        public static Random _random = new Random();

        public QueueManagement(int customers, int timeInside)
        {
            Customers = customers;
            _timeInside = timeInside;
        }

    }
}

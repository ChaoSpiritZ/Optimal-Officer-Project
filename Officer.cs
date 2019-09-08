using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Officer_Project
{
    class Officer
    {
        private int _currentCustomerTimeInside;
        public int Timer { get; set; }
        private int _customersServed = 0;

        public Officer()
        {
            Timer = 0;
        }

        public void CallNextOne(Object i)
        {
            while (QueueManagement.Customers > 0)
            {
                QueueManagement._mutex.WaitOne();
                if (QueueManagement.Customers > 0)
                {
                    QueueManagement.Customers--;
                    QueueManagement._mutex.ReleaseMutex();
                    _currentCustomerTimeInside = QueueManagement._random.Next(QueueManagement._timeInside - 5, QueueManagement._timeInside + 5);
                    Thread.Sleep(_currentCustomerTimeInside);
                    Timer += _currentCustomerTimeInside;
                    _customersServed++;
                }
                else
                {
                    QueueManagement._mutex.ReleaseMutex();
                    break;
                }
            }
            // added {3} to check if they actually served everyone and didn't go under 0
            Console.WriteLine("Officer {0} Done!, worked {1} minutes, served {2}, {3} are now in the line",(int) i + 1, Timer, _customersServed, QueueManagement.Customers);
        }
    }
}

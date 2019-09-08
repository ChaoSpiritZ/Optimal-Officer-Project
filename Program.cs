using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Officer_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            int customers = 0;
            int timeInside = 0;
            int officers = 0;
            int totalWorkTime = 0;
            int averageWorkTime;
            double extraTime = 0;

            do
            {
                Console.Write("Please enter a valid number of customers: ");
            }
            while (int.TryParse(Console.ReadLine(), out customers) == false || customers <= 0);

            do
            {
                Console.Write("Please enter a valid time spent inside in minutes (actually miliseconds), must be more than 5: ");
            }
            while (int.TryParse(Console.ReadLine(), out timeInside) == false || timeInside <= 5);

            do
            {
                Console.Write("Please enter a valid number of officers: ");
            }
            while (int.TryParse(Console.ReadLine(), out officers) == false || officers <= 0);

            //Console.WriteLine("customers: " + customers);
            //Console.WriteLine("time inside: " + timeInside);
            //Console.WriteLine("officers: " + officers);

            QueueManagement Q = new QueueManagement(customers, timeInside);
            Officer[] theOffice = new Officer[officers];
            Thread[] threads = new Thread[officers];

            for(int i = 0;i < officers;i++)
            {
                theOffice[i] = new Officer();
                threads[i] = new Thread(new ParameterizedThreadStart(theOffice[i].CallNextOne));
                threads[i].Start(i);
            }

            for(int i = 0; i < officers; i++)
            {
                threads[i].Join();
                totalWorkTime += theOffice[i].Timer;
            }

            averageWorkTime = totalWorkTime / officers;
            Console.WriteLine("average work time: " + averageWorkTime + " minutes");

            extraTime = (averageWorkTime - 540) * officers;
            Console.WriteLine("extra time: " + extraTime + " minutes");

            if(extraTime > 0)
            {
                Console.WriteLine("you will need {0} more officers", Math.Ceiling(extraTime / 540));
            }
            else if(extraTime < -540)
            {
                Console.WriteLine("you will need {0} less officers", Math.Ceiling(extraTime / 540) * -1);
            }
            else
            {
                Console.WriteLine("you have the optimal number of officers");
            }
        }
    }
}

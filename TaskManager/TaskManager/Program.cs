using System;
using System.Diagnostics;
using System.Threading;

namespace TaskManager
{
    class Program
    {
        private static DateTime lastTime;
        private static TimeSpan lastTotalProcessorTime;

        private static DateTime curTime;
        private static TimeSpan curTotalProcessorTime;

        public static void ChangePriority(int PID,string Priority )
        {
            try
            {
                Process ProcessTochange = Process.GetProcessById(PID);
                switch (Priority)
                {
                    case "Normal":
                        ProcessTochange.PriorityClass = ProcessPriorityClass.Normal;
                        break;
                    case "High":
                        ProcessTochange.PriorityClass = ProcessPriorityClass.High;
                        Console.WriteLine("changed to " + ProcessTochange.PriorityClass);
                        break;
                    case "BelowNormal":
                        ProcessTochange.PriorityClass = ProcessPriorityClass.BelowNormal;
                        break;
                    case "RealTime":
                        ProcessTochange.PriorityClass = ProcessPriorityClass.RealTime;
                        break;
                    case "AboveNormal":
                        ProcessTochange.PriorityClass = ProcessPriorityClass.AboveNormal;
                        break;
                    default:
                        Console.WriteLine("Нет такого приоритета");
                        break;



                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public static void KillProcess(int PID)
        {
            try {
                Process ProcessToKill = Process.GetProcessById(PID);
                ProcessToKill.Kill();
                Console.WriteLine("Процесс остановлен");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static double CalculateCpuUsage(Process process)
        {
           
                lastTime=default(DateTime);
                lastTotalProcessorTime=default(TimeSpan);
                

                if (lastTime == default(DateTime))
                {
                    lastTime = DateTime.Now;
                    lastTotalProcessorTime = process.TotalProcessorTime;
                }

                Thread.Sleep(200);

                    curTime = DateTime.Now;
                    curTotalProcessorTime = process.TotalProcessorTime;
                    
                    double CPUUsage = (curTotalProcessorTime.TotalMilliseconds - lastTotalProcessorTime.TotalMilliseconds) /
                    curTime.Subtract(lastTime).TotalMilliseconds / Convert.ToDouble(Environment.ProcessorCount);
                    lastTime = curTime;
                    lastTotalProcessorTime = curTotalProcessorTime;
                    
                    
                    return CPUUsage*100;

        }
        public static void showAndCalcProcessInfo()
        {
            var RunningProcesses = Process.GetProcesses();
            
            foreach (var Process in RunningProcesses)
            {
                try
                {
                   
                    ProcessInfo CurrentProcess = new ProcessInfo { ProcessID = Process.Id, ProcessName = Process.ProcessName, CpuUsage = CalculateCpuUsage(Process) };
                    
                    Console.WriteLine(CurrentProcess);
                    Console.WriteLine();
                   

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            }
            


        }


        static void Main(string[] args)
        {
            Console.WriteLine("Диспетчер задач \n \n");
            while (! Console.KeyAvailable)
            {
                
               
                Console.WriteLine("1- отобразить имя и ID процесса, а также загрузку CPU для всех запущенных процессов.   ");
                Console.WriteLine("2- остановить какой-либо процесс ");
                Console.WriteLine("3- поменять приоритет процесса ");
                Console.WriteLine("4- выход");
                Console.WriteLine();
                var Selection = Console.ReadLine();
                switch (Selection)
                {
                    case "1":
                         showAndCalcProcessInfo();
                        
                        break;
                    case "2":
                        try {
                            Console.WriteLine("Введите ID процесса, который хотите остановить");
                            var pidTokill = Console.ReadLine();
                            KillProcess(int.Parse(pidTokill));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message); ;
                        }


                        break;
                    case "3":
                        try {
                            Console.WriteLine("Введите ID процесса ");
                            var PidToChange = Console.ReadLine();
                            Console.WriteLine("Введите приоритет:\n  Normal, High, BelowNormal, RealTime, AboveNormal");
                            var Priority = Console.ReadLine();
                            ChangePriority(Convert.ToInt32( PidToChange),Priority);
                        }
                        catch ( Exception ex) {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case "4":
                        Environment.Exit(0);
                        break;


                    default:
                        Console.WriteLine("Вы выбрали не существующую операцию");
                        Console.WriteLine();
                        Console.WriteLine();
                        break;
                }



            }

        }
    }
}

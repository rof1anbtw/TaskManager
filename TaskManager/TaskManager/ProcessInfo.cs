using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskManager
{
    class ProcessInfo
    {
        public int ProcessID { get; set; }
        public string ProcessName  { get; set; }
        public double CpuUsage { get; set; }

        public override string ToString()
        {
            try {
               
                return $" ID процесса: {this.ProcessID} \n Название процесса: {this.ProcessName} \n Используется CPU: {Math.Round(this.CpuUsage, 2)} %";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return $" ID процесса: {this.ProcessID} \n Название процесса: {this.ProcessName} \n Используется CPU: 0.00 %";
        }

    }
}

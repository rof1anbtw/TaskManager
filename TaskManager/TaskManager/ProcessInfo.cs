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
               
                return $" ID ��������: {this.ProcessID} \n �������� ��������: {this.ProcessName} \n ������������ CPU: {Math.Round(this.CpuUsage, 2)} %";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return $" ID ��������: {this.ProcessID} \n �������� ��������: {this.ProcessName} \n ������������ CPU: 0.00 %";
        }

    }
}

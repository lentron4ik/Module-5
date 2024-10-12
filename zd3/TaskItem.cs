using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zd3
{
    public class TaskItem
    {
        public string Description { get; set; }
        public bool IsCompleted { get; set; }

        public override string ToString()
        {
            return $"{Description} {(IsCompleted ? "(выполнено)" : "")}";
        }
    }
}

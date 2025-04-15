using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Projects.Response
{
    public class GetStatusTasksResponse
    {
        public int NotStarted { get; set; } = 0;     // Chưa làm
        public int InProgress { get; set; } = 0;      // Đang làm
        public int Completed { get; set; } = 0;     // Hoàn thành
        public int Overdue { get; set; } = 0;     // Quá hạn   
    }
}

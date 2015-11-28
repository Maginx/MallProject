using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracePatient
{
    class EndoscopeInformation
    {
        public int Id;
        public string WashBeginTime;
        public string WashEndTime;
        public string WashDate; 

        public EndoscopeInformation(int id, string washBeginTime, string washEndTime, string WashDate)
        {
            this.Id = id;
            this.WashBeginTime = washBeginTime;
            this.WashEndTime = washEndTime;
            this.WashDate = WashDate;
        }
    }
}

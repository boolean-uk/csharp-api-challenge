using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exercise.engine.Models
{
    public class RefillWarning
    {
        public string Message { get; set; }
        public DateTime Time { get; set; }
        public RefillWarning(string message) {
            Message = message;
        }
    }
}

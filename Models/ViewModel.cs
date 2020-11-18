using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Quiz.Models
{
    public class ViewModel
    {
        public IEnumerable<Paper> Papers { get; set; }
        public IEnumerable<Question> Questions { get; set; }
        public IEnumerable<Option> Options { get; set; }
        

    }

}

using System.Collections.Generic;

namespace Exam.DeliveriesManager
{
    public class Deliverer
    {
        //Constructor
        public Deliverer(string id, string name)
        {
            Id = id;
            Name = name;
        }

        //Properties
        public string Id { get; set; }

        public string Name { get; set; }
    }
}

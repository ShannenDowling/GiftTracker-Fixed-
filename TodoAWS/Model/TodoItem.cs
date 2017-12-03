using System;

namespace TodoAWSSimpleDB
{
	public class TodoItem
	{
        public string ID { get; set; }

        public string Name { get; set; }

        public string Link { get; set; }

        public double Price { get; set; }

        public bool Bought { get; set; }
    }
}

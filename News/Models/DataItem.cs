using System;
using System.Collections.Generic;
using System.Text;

namespace News.Models
{
   public class DataItem
    {
        public DataItem(int item)
        {
            Item = item;
            Group = item / 30;
        }

        public int Item { get; }

        public int Group { get; }

        public string Title { get; }

        public string Name => $"Item {Item}";
        
    }
}

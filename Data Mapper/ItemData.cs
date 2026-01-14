using System;
using System.Collections.Generic;
using System.Text;

namespace TestProjectAssignment.Data_Mapper
{
    public class ItemData
    {
        public required string ItemName { get; set; }
        public required int Quantity { get; set; }
        public required double ItemPrice { get; set; }

    }
}

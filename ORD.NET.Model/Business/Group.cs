using System;
using System.Collections.Generic;
using System.Text;

namespace ORD.NET.Model.Business
{
    public sealed class Group
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte[] GroupImage { get; set; }
    }
}

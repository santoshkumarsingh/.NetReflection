﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomSerializer
{
    [MyDataContract]
   public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
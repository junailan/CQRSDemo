﻿using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands.Books
{
    public class CreateBookCommand : CommandBase
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public int Pages { get; set; }
        public int Inventory { get; set; }
    }
}

﻿using System;

namespace Balto.Web.ViewModels
{
    public class ObjectiveGetView
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
    }
}

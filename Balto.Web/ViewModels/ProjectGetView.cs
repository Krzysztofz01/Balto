﻿using System.Collections.Generic;

namespace Balto.Web.ViewModels
{
    public class ProjectGetView
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public UserGetView Owner { get; set; }
        public IEnumerable<UserGetView> ReadWriteUsers { get; set; }
        public IEnumerable<ProjectTableGetView> Tabels { get; set; }
    }
}

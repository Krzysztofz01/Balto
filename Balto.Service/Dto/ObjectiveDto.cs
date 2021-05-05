﻿using System;

namespace Balto.Service.Dto
{
    public class ObjectiveDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Finished { get; set; }
        public bool Daily { get; set; }
        public long UserId { get; set; }
        public UserDto User { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
    }
}

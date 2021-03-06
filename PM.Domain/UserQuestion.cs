﻿using PM.Domain.Base;
using System;

namespace PM.Domain
{
    public class UserQuestion : IdBase<int>
    {
        public string Subject { get; set; }
        public string CreatorMessage { get; set; }
        public string UserCreatorId { get; set; }
        public ApplicationUser UserCreator { get; set; }
        public string UserResponderId { get; set; }
        public ApplicationUser UserResponder { get; set; }
        public string ResponderMessage { get; set; }
        public DateTime CreateDate { get; set; }
    }
}

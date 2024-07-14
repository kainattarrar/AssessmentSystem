﻿using AssessmentSystem.Service.Models.Enum;
using AssessmentSystem.Service.Shared.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentSystem.Service.Entities
{
    public class Email : EntityBase, IEntity
    {
        public EmailStatus Status {  get; set; }
    }
}

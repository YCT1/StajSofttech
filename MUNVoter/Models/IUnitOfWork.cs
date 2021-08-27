﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUNVoter.Models
{
    public interface IUnitOfWork
    {
        IMotionRepository Motions { get; }
        int Complete();
    }
}

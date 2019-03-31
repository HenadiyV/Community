﻿using Community.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Domain.Abstract
{
  public  interface IGenderRepository
    {
        IEnumerable<Genders> Gender { get; }
        int GetIdGender(string gender);
    }
}

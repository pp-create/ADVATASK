﻿using BLL.Respostery;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.InterFace
{
    public interface IEmployeRep :IGenaricRepository<Employees>
    {
        Task<IEnumerable<Employees>> GetAllWithDetailsAsync();
    }
}

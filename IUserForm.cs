﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    public interface IUserForm
    {
        void FillFields(int ID);
        void Delete(int ID);
    }
}

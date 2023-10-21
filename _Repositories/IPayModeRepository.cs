﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supermarket.Models;

namespace Supermarket._Repositories
{
    internal interface IPayModeRepository
    {
        void Add(PayModeModel payModeModel);

        void Edit(PayModeModel payModeModel);

        void Delete(PayModeModel payModeModel);

        IEnumerable<PayModeModel> GetAll();

        IEnumerable<PayModeModel> GetByValue(string value);
    }
}
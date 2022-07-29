﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuth.Data
{
    public interface IUnitOfWork : IDisposable
    {
        Task CompleteAsync();
    }
}

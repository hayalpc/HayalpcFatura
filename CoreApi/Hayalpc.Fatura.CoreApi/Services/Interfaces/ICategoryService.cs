﻿using Hayalpc.Fatura.Common.Dtos;
using Hayalpc.Library.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hayalpc.Fatura.CoreApi.Services.Interfaces
{
    public interface ICategoryService
    {
        IDataResult<List<CategoryDto>> Get();
    }
}

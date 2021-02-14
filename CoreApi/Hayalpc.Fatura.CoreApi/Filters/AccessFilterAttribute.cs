﻿using Microsoft.AspNetCore.Mvc;

namespace Hayalpc.Fatura.Panel.External.Filters
{
    public class AccessFilterAttribute : TypeFilterAttribute
    {
        public AccessFilterAttribute() : base(typeof(AccessFilter))
        {
        }
    }
}
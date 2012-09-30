using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NiCris.CoreServices.Interfaces;
using NiCris.DataAccess.Interfaces;

namespace NiCris.CoreServices.Services
{
    public class LookupService : ILookupService
    {
        public ILookupRepository LookupRepository { get; set; }  
    }
}

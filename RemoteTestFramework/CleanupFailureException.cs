using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace RemoteTestFramework
{
    class CleanupFailureException : UnitTestAssertException
    {
        public CleanupFailureException(string msg) : base(msg) { }
    }
}


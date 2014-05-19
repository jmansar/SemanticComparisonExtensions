using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Jmansar.SemanticComparisonExtensions.Diagnostics
{
    public class DefaultDiagnosticsWriter : IDiagnosticsWriter
    {
        public void WriteMessage(string message)
        {
            Debug.WriteLine(message);
        }
    }
}

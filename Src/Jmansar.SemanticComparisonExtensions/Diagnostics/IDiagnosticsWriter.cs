using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jmansar.SemanticComparisonExtensions.Diagnostics
{
    public interface IDiagnosticsWriter
    {
        void WriteMessage(string message);
    }
}

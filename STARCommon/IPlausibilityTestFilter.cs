using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STARCommon {
    public interface IPlausibilityTestFilter {
        bool[] Accept(IPlausibilityTest plausiCheck, PlausibilitySeverity severity);

        PlausibilitySeverity GetSeverity(int palusiCheckId);

        PlausibilitySeverity GetSeverity(IPlausibilityTest plausiCheck);
    }
}

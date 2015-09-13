using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STARCommon {
    public interface IPlausibilityTestSet {
        void AddCheck(IPlausibilityTest plausiTest, int position);

        void AddSet(IPlausibilityTestSet plausiSet, int position);

        int[] Check(IPlausibilityCheckContext context);
    }
}

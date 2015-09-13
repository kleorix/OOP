using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STARCommon {
    public interface IPlausibilityTest {

        int GetId();

        bool IsPlausible(IPlausibilityCheckContext context);
    }

    public enum PlausibilityContextTaxLiabilities {
        Changed,
        CurentP,
        AsociatedFederal,
    }

    public enum PlausibilityContextPersons {
        ChangedPerson,
        TaxLiabilityPerson1,
        TaxLiabilityPerson2,
        Partner,
    }

    public enum PlausibilitySeverity {
        Error = 1,
        Warning = 2,
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STARCommon {
    public interface IPlausibilityTestContext {
        ST_Steuerpflicht GetAssociatedTaxLiability(PlausibilityContextTaxLiabilities type, ST_Steuerpflicht taxLiability);

        ST_Person GetPerson(PlausibilityContextPersons type);

        ST_Person GetPersonForTaxLiability(PlausibilityContextPersons type, ST_Steuerpflicht taxLiability);

        ST_Steuerpflicht GetTaxLiability(PlausibilityContextTaxLiabilities type);
    }
}

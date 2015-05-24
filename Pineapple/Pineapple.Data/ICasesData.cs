using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pineapple.Model;

namespace Pineapple.Data
{
    public interface ICasesData
    {
        Cases GetCase(int caseId);

        List<Cases> LoadCases(Pagination pagination, CaseType caseType);

        List<CaseItem> LoadCaseItemsByCaseId(int caseId);

        List<CaseItem> LoadCaseItemsByCaseIds(List<int> caseIds);

        Cases SaveCase(Cases cases);

        List<CaseItem> SaveCaseItems(List<CaseItem> caseItems);

        bool DeleteCase(int caseId);

        bool DeleteCaseItem(int caseItemId);
    }
}

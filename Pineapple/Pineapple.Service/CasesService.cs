using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Pineapple.Business;
using Pineapple.Model;

namespace Pineapple.Service
{
    public class CasesService
    {
        [Dependency]
        public CasesManager CasesManager { protected get; set; }

        public Cases GetCase(int caseId)
        {
            return CasesManager.GetCase(caseId);
        }

        public List<Cases> LoadCases(Pagination pagination)
        {
            return CasesManager.LoadCases(pagination);
        }

        public List<Cases> LoadSimpleCases(Pagination pagination)
        {
            return CasesManager.LoadSimpleCases(pagination);
        }

        public Cases SaveCase(Cases cases)
        {
            return CasesManager.SaveCase(cases);
        }

        public bool DeleteCase(int caseId)
        {
            return CasesManager.DeleteCase(caseId);
        }

        public List<CaseItem> LoadCaseItemsByCaseId(int caseId)
        {
            return CasesManager.LoadCaseItemsByCaseId(caseId);
        }

        public CaseItem SaveCaseItem(CaseItem item)
        {
            return CasesManager.SaveCaseItem(item);
        }

        public bool DeleteCaseItem(int caseItemId)
        {
            return CasesManager.DeleteCaseItem(caseItemId);
        }
    }
}

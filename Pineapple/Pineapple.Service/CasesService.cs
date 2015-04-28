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

        public Cases SaveCase(Cases cases)
        {
            return CasesManager.SaveCase(cases);
        }

        public bool DeleteCase(int caseId)
        {
            return CasesManager.DeleteCase(caseId);
        }
    }
}

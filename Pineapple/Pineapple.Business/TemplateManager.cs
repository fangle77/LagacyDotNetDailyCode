using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Pineapple.Data;
using Pineapple.Model;

namespace Pineapple.Business
{
    public class TemplateManager
    {
        [Dependency]
        public ITemplateData TemplateData { protected get; set; }

        [Dependency]
        public IMappingData MappingData { protected get; set; }

        public Template SaveTemplate(Template template)
        {
            return TemplateData.SaveTemplate(template);
        }

        public Template GetTemplateById(int templateId)
        {
            return TemplateData.GetTemplateById(templateId);
        }

        public List<Template> LoadAllTemplate()
        {
            return TemplateData.LoadAllTemplate();
        }

        public CategoryTemplateMapping GetCategoryTemplateMappingByTemplateId(int templateId)
        {
            return (CategoryTemplateMapping)MappingData.GetMappingByValue(new CategoryTemplateMapping(), templateId);
        }

        public bool DeleteTemplate(int templateId)
        {
            bool templateDeleted = TemplateData.DeleteTemplate(templateId);
            if (templateDeleted)
            {
                MappingData.DeleteMappingByValue(new CategoryTemplateMapping(), templateId);
            }
            return templateDeleted;
        }
    }
}

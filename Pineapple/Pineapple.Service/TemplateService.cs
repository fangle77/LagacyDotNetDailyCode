using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Pineapple.Model;
using Pineapple.Business;
using Pineapple.View;

namespace Pineapple.Service
{
    public class TemplateService
    {
        [Dependency]
        public TemplateManager TemplateManager { protected get; set; }

        [Dependency]
        public CategoryManager CategoryManager { protected get; set; }

        public List<Template> LoadAllTemplates()
        {
            return TemplateManager.LoadAllTemplate();
        }
        
        public Template SaveTemplate(Template template)
        {
            return TemplateManager.SaveTemplate(template);
        }

        public bool DeleteTemplate(int templateId)
        {
            return TemplateManager.DeleteTemplate(templateId);
        }

        public TemplateView GetTemplateView(int templateId)
        {
            var view = new TemplateView();
            view.Template = TemplateManager.GetTemplateById(templateId);
            if (view.Template == null) return null;

            view.Categories = CategoryManager.LoadCategoriesByTemplateId(templateId);
            return view;
        }
    }
}

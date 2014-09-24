using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pineapple.Model;

namespace Pineapple.Data
{
    public interface ITemplateData
    {
        Template SaveTemplate(Template template);
        Template GetTemplateById(int templateId);
        List<Template> LoadAllTemplate();
        bool DeleteTemplate(int templateId);
    }
}

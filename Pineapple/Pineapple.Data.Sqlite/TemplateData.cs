using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Pineapple.Model;

namespace Pineapple.Data.Sqlite
{
    public class TemplateData : ITemplateData
    {
        public Template SaveTemplate(Template template)
        {
            using (var cnn = SqLiteBaseRepository.DbConnection())
            {
                if (template.TemplateId == null)
                {
                    template.TemplateId = cnn.Query<int>(template.GetSqliteInsertSql("TemplateId"), template).FirstOrDefault();
                }
                else
                {
                    cnn.Execute(template.GetUpdateSql("TemplateId"), template);
                }
                return template;
            }
        }

        public Template GetTemplateById(int templateId)
        {
            using (var cnn = SqLiteBaseRepository.DbReadOnlyConnection())
            {
                return cnn.Query<Template>(typeof(Template).GetSelectSql("TemplateId=@TemplateId"), new { TemplateId = templateId }).FirstOrDefault();
            }
        }

        public List<Template> LoadAllTemplate()
        {
            using (var cnn = SqLiteBaseRepository.DbReadOnlyConnection())
            {
                return cnn.Query<Template>(typeof(Template).GetSelectSql()).ToList();
            }
        }

        public bool DeleteTemplate(int templateId)
        {
            using (var cnn = SqLiteBaseRepository.DbReadOnlyConnection())
            {
                return cnn.Execute("delete from Template where TemplateId=@TemplateId", new { TemplateId = templateId }) > 0;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Pineapple.Model;

namespace Pineapple.Data.Sqlite
{
    public class AttachmentData : IAttachmentData
    {
        private readonly string KeyFiled = "AttachmentId";
        private readonly string[] InsertIgnore = { "AttachmentId", "IsImage" };
        private readonly string[] UpdateIgnore = { "AttachmentId", "IsImage" };
        private readonly string[] SelectIgnore = { "IsImage" };

        public Attachment SaveAttachment(Attachment attachment)
        {
            using (var cnn = SqLiteBaseRepository.DbConnection())
            {
                if (attachment.AttachmentId == null || attachment.AttachmentId <= 0)
                {
                    attachment.AttachmentId = cnn.Query<int>(attachment.GetSqliteInsertSql(InsertIgnore), attachment).FirstOrDefault();
                    return attachment;
                }
                else
                {
                    cnn.Execute(attachment.GetUpdateSql(KeyFiled, UpdateIgnore), attachment);
                    return attachment;
                }
            }
        }

        public bool DeleteAttachment(int attachmentId)
        {
            using (var cnn = SqLiteBaseRepository.DbConnection())
            {
                return cnn.Execute("Delete from Attachment where AttachmentId=@AttachmentId", new { AttachmentId = attachmentId }) > 0;
            }
        }

        public List<Attachment> LoadAllAttachment()
        {
            using (var cnn = SqLiteBaseRepository.DbConnection())
            {
                return cnn.Query<Attachment>(typeof(Attachment).GetSelectSql(null, SelectIgnore)).ToList<Attachment>();
            }
        }


        public Attachment GetAttachmentById(int attachmentId)
        {
            using (var cnn = SqLiteBaseRepository.DbConnection())
            {
                return cnn.Query<Attachment>(typeof(Attachment).GetSelectSql("AttachmentId=@AttachmentId", SelectIgnore), new { AttachmentId = attachmentId }).FirstOrDefault();
            }
        }

        public Attachment GetAttachmentByName(string originName)
        {
            using (var cnn = SqLiteBaseRepository.DbConnection())
            {
                return cnn.Query<Attachment>(typeof(Attachment).GetSelectSql("OriginName=@OriginName", SelectIgnore), new { OriginName = originName }).FirstOrDefault();
            }
        }
    }
}

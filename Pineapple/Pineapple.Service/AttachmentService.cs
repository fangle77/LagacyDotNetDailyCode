using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Pineapple.Model;
using Pineapple.Business;

namespace Pineapple.Service
{
    public class AttachmentService
    {
        [Dependency]
        public AttachmentManager AttachmentManager { protected get; set; }

        public Attachment SaveAttachment(Attachment attachment)
        {
            return AttachmentManager.SaveAttachment(attachment);
        }

        public Attachment SaveAttachment(int contentLength, string contentType, string fileName)
        {
            return AttachmentManager.SaveAttachment(contentLength, contentType, fileName);
        }

        public bool DeleteAttachment(int attachmentId)
        {
            return AttachmentManager.DeleteAttachment(attachmentId);
        }

        public List<Attachment> LoadAllAttachment()
        {
            return AttachmentManager.LoadAllAttachment();
        }

        public Attachment GetAttachmentById(int attachmentId)
        {
            return AttachmentManager.GetAttachmentById(attachmentId);
        }

        public Attachment GetAttachmentByName(string originName)
        {
            return AttachmentManager.GetAttachmentByName(originName);
        }
    }
}
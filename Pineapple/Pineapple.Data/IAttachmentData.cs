using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pineapple.Model;

namespace Pineapple.Data
{
    public interface IAttachmentData
    {
        Attachment SaveAttachment(Attachment attachment);
        Attachment GetAttachmentById(int attachmentId);
        Attachment GetAttachmentByName(string originName);
        bool DeleteAttachment(int attachmentId);
        List<Attachment> LoadAllAttachment();
        List<Attachment> LoadAttachmentByIds(List<int> attachmentIds);
    }
}
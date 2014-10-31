using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Practices.Unity;
using Pineapple.Data;
using Pineapple.Model;

namespace Pineapple.Business
{
    public class AttachmentManager
    {
        [Dependency]
        public IAttachmentData AttachmentData { protected get; set; }

        private readonly HashSet<string> ImageTypes = new HashSet<string>("jpg,jpeg,png,ico,gif,bmp".Split(','), StringComparer.OrdinalIgnoreCase);
        private readonly string ImagePath = "/contents/img";
        private readonly string OtherTypePath = "/contents/other";

        public Attachment SaveAttachment(Attachment attachment)
        {
            var existAttachment = GetAttachmentById(attachment.AttachmentId ?? 0);
            if (existAttachment != null)
            {
                attachment.Version = existAttachment.Version + 1;
                attachment.AttachmentId = existAttachment.AttachmentId;
                attachment.FileName = existAttachment.FileName;
            }
            else
            {
                attachment.Version = 0;
            }

            return AttachmentData.SaveAttachment(attachment);
        }

        public Attachment SaveAttachment(int contentLength, string contentType, string fileName)
        {
            var existAttachment = GetAttachmentByName(fileName);
            if (existAttachment != null)
            {
                existAttachment.Version++;
                existAttachment.Size = contentLength;
                existAttachment.ContentType = contentType;
                existAttachment.Type = GetExtention(fileName);
                return AttachmentData.SaveAttachment(existAttachment);
            }
            else
            {
                Attachment attachment = new Attachment();
                attachment.OriginName = fileName;
                attachment.Type = GetExtention(fileName);
                attachment.FileName = GenerateFileName();
                attachment.ContentType = contentType;
                attachment.Size = contentLength;
                attachment.Version = 0;
                if (!string.IsNullOrEmpty(attachment.Type))
                {
                    attachment.Alt = fileName.Substring(0, fileName.LastIndexOf('.'));
                }
                attachment.Path = IsImage(attachment.Type) ? ImagePath : OtherTypePath;
                return AttachmentData.SaveAttachment(attachment);
            }
        }

        public bool DeleteAttachment(int attachmentId)
        {
            var existAttachment = GetAttachmentById(attachmentId);
            if (existAttachment != null)
            {
                File.Delete(existAttachment.FilePath);
                return AttachmentData.DeleteAttachment(attachmentId);
            }
            return false;
        }

        public List<Attachment> LoadAllAttachment()
        {
            var list = AttachmentData.LoadAllAttachment();
            if (list != null && list.Count > 0)
            {
                list.ForEach(a => a.IsImage = IsImage(a.Type));
            }
            return list;
        }

        public Attachment GetAttachmentById(int attachmentId)
        {
            return AttachmentData.GetAttachmentById(attachmentId);
        }

        public Attachment GetAttachmentByName(string originName)
        {
            return AttachmentData.GetAttachmentByName(originName);
        }

        public string BuildFileName(string originName)
        {
            var existAttachment = GetAttachmentByName(originName);
            if (existAttachment != null)
            {
                return existAttachment.FileName;
            }
            else
            {
                return Guid.NewGuid().ToString().Replace("-", "").ToLower();
            }
        }

        private string GenerateFileName()
        {
            return Guid.NewGuid().ToString().Replace('-', '0').ToLower();
        }

        private string GetExtention(string fileName)
        {
            int dotIndex = fileName.LastIndexOf('.');
            if (dotIndex >= 0 && dotIndex < fileName.Length - 1)
            {
                return fileName.Substring(dotIndex + 1);
            }
            return null;
        }

        private bool IsImage(string type)
        {
            return ImageTypes.Contains(type);
        }
    }
}
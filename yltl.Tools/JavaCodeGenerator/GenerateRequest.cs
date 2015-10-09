using System.Collections.Generic;

namespace yltl.Tools.JavaCodeGenerator
{
    public class GenerateRequest
    {
        public string TablePrefix { get; set; }
        public List<string> InputLines { get; set; } 
    }
}
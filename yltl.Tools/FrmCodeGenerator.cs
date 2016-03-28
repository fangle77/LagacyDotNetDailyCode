using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace yltl.Tools
{
    public partial class FrmCodeGenerator : Form
    {
        public FrmCodeGenerator()
        {
            InitializeComponent();

            AllGenerators.Add("DomainGenerator", new DomainGenerator());
            AllGenerators.Add("DomainToViewProperty", new DomainToViewProperty());
            AllGenerators.Add("ObjectCopyProperty", new ObjectCopyProperty());
            AllGenerators.Add("ObjectCopyPropertyIfNotNull", new ObjectCopyPropertyIfNotNull());
            AllGenerators.Add("RepositoryGenerator", new RepositoryGenerator());
            AllGenerators.Add("ServiceGenerator", new ServiceGenerator());
            AllGenerators.Add("DbUnitXmlGenerator", new DbUnitXmlGenerator());
            AllGenerators.Add("ApiParameterGenerator", new ApiParameterGenerator());
            AllGenerators.Add("XmlElementAnotationGenerator", new XmlElementAnotationGenerator());
            AllGenerators.Add("JdbcRowMapperGenerator", new JdbcRowMapperGenerator());
            AllGenerators.Add("AssertObjEqureGenerator", new AssertObjEqureGenerator());
            AllGenerators.Add("MongoUpdateIfNotNull", new MongoUpdateIfNotNull());
            AllGenerators.Add("MongoUpdateInnerArrayIfNotNull", new MongoUpdateInnerArrayIfNotNull());
            AllGenerators.Add("ESMapping", new ESMapping());
            AllGenerators.Add("ESIndexData", new ESIndexData());
            AllGenerators.Add("ESSearchBuilder", new ESSearchBuilder());

            //AllGenerators.Add("ProductServiceEntityDomainConvert", new ProductServiceEntityDomainConvert());
            //AllGenerators.Add("ProductServiceRepository", new ProductServiceRepository());
            //AllGenerators.Add("ProductServiceService", new ProductServiceService());
            //AllGenerators.Add("ConvertEntityList", new ConvertEntityList());

            //AllGenerators.Add("TableProperty", new TableProperty());
            //AllGenerators.Add("TableToSaveSP", new TableToSaveSP());
            //AllGenerators.Add("ClassToString", new ClassToString());
            //AllGenerators.Add("CreateTable", new CreateTable());
            //AllGenerators.Add("SpToDataCode", new SpToDataCode());
        }

        readonly Dictionary<string, ICodeGenerator> AllGenerators = new Dictionary<string, ICodeGenerator>();

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            File.WriteAllText("tempinput", rtxtInput.Text);

            rtxtInput.Text = File.ReadAllText("tempinput");

            string name = cmbType.Text;
            if (AllGenerators.ContainsKey(name))
            {
                Generate(AllGenerators[name]);
            }
        }

        private void Generate(ICodeGenerator generator)
        {
            string input = rtxtInput.Text.Trim();
            if (string.IsNullOrEmpty(input))
            {
                rtxtInput.Text = generator.ExampleInput;
                return;
            }
            string output = generator.Generate(rtxtInput.Lines);
            rtxtOutput.Text = output;
        }

        private void FrmCodeGenerator_Load(object sender, EventArgs e)
        {
            List<string> list = new List<string>(AllGenerators.Count);
            list.AddRange(AllGenerators.Keys);
            list.Sort();
            cmbType.Items.AddRange(list.ConvertAll(s => (object)s).ToArray());
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            rtxtInput.Text = AllGenerators[cmbType.Text].ExampleInput;
        }
    }

    interface ICodeGenerator
    {
        string ExampleInput { get; }
        string Generate(string[] inputs);
    }

    #region C#
    class TableProperty : ICodeGenerator
    {
        public string ExampleInput
        {
            get
            {
                return @"OrderId INT NOT NULL,
	    [Service] VARCHAR(30) NOT NULL,
	    [Subject] VARCHAR(50) NOT NULL,";
            }
        }

        private static string GetType(string s)
        {
            if (s.IndexOf("bigint", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return "long";
            }
            if (s.IndexOf("int", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return "int";
            }
            else if (s.IndexOf("decimal", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return "decimal";
            }
            else if (s.IndexOf("datetime", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return "DateTime";
            }
            return "string";
        }
        public string Generate(string[] inputs)
        {
            var lines = inputs;
            var reg = new Regex(@"\w+");
            StringBuilder builder = new StringBuilder();
            foreach (string line in lines)
            {
                var matchs = reg.Matches(line);
                if (matchs.Count == 0) continue;
                builder.AppendFormat("public {1} {0} {{ get; set; }}", matchs[0].Value, GetType(matchs[1].Value));
                builder.AppendLine();
                builder.AppendLine();
            }
            return builder.ToString();
        }
    }

    class TableToSaveSP : ICodeGenerator
    {
        public string ExampleInput
        {
            get
            {
                return @"QWSaveAlipayPaymentRequest
AlipayPaymentRequest
        OrderId INT NOT NULL,
	    [Service] VARCHAR(30) NOT NULL,
	    [Subject] VARCHAR(50) NOT NULL,";
            }
        }

        public string Generate(string[] inputs)
        {
            var lines = inputs;
            string spName = lines[0].Trim();
            string table = lines[1].Trim();

            StringBuilder param = new StringBuilder();
            StringBuilder insertParam = new StringBuilder();
            StringBuilder insertValue = new StringBuilder();
            StringBuilder update = new StringBuilder();

            var reg = new Regex(@"[^\s]+");

            for (int i = 2; i < lines.Length; i++)
            {
                var matchs = reg.Matches(lines[i]);
                if (matchs.Count == 0) continue;
                string colName = matchs[0].Value.Trim().Trim(']').Trim('[').Trim();
                string type = matchs[1].Value.Trim().Trim(',').Trim();

                //bool isCreateOrUpdateDate = "UpdateDate".Equals(colName, StringComparison.OrdinalIgnoreCase) || "CreateDate".Equals(colName, StringComparison.OrdinalIgnoreCase);
                //bool isCreateBy = "CreateBy".Equals(colName, StringComparison.OrdinalIgnoreCase);
                //bool isUpdateBy = "UpdateBy".Equals(colName, StringComparison.OrdinalIgnoreCase);
                //if (isCreateBy) colName = "SaveBy";

                param.AppendFormat("\t{2}@{0} {1} = NULL\r\n", colName, type, GetComma(param));
                insertParam.AppendFormat("\t\t\t{1}{0}\r\n", colName, GetComma(insertParam));
                insertValue.AppendFormat("\t\t\t{1}@{0}\r\n", colName, GetComma(insertValue));
                update.AppendFormat("\t\t\t{1}{0} = @{0}\r\n", colName, GetComma(update));
            }

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = '{0}')
	BEGIN
		DROP  Procedure  dbo.{0}
	END

GO

/*
 * Designer:	Bowen
 * Description:	QS-80891 WC-2267:AliPay Integration
 * Created:		{1}
 * History:
 * =============================================================================
 * Author      DateTime        Alter Description
 * =============================================================================
 */

CREATE Procedure dbo.{0}", spName, DateTime.Now.ToString("MM/dd/yyyy"));
            sql.AppendLine();
            sql.AppendLine("(");
            sql.Append(param);
            sql.AppendLine(")");
            sql.AppendFormat(@"AS
BEGIN
	IF NOT EXISTS(SELECT 1 FROM dbo.[{0}] WITH(NOLOCK) WHERE OrderId = @OrderId)
	BEGIN 
		INSERT INTO [dbo].[{0}]
		(", table);
            sql.AppendLine();
            sql.Append(insertParam);
            sql.AppendLine("\t\t)");
            sql.AppendLine("\tVALUES(");
            sql.Append(insertValue);
            sql.AppendLine("\t\t)");
            sql.AppendLine("\tEND");
            sql.AppendFormat(@"    ELSE
    BEGIN
        UPDATE [dbo].[{0}] SET", table);
            sql.AppendLine();
            sql.Append(update);
            sql.AppendLine("\t\tWHERE 1=0");
            sql.AppendLine("\tEND");
            sql.Append(@"END
GO");
            return sql.ToString();
        }

        private static string GetComma(StringBuilder builder)
        {
            return builder.Length == 0 ? string.Empty : ",";
        }
    }

    class ClassToString : ICodeGenerator
    {
        public string ExampleInput
        {
            get
            {
                return @" public int OrderId { get; set; }

        public string Service { get; set; }

        public string Subject { get; set; }

        public string Currency { get; set; }";
            }
        }

        public string Generate(string[] inputs)
        {
            string content = string.Join("\r\n", inputs);
            var reg = new Regex(@"\w+\s+(\w+)\s+{");
            var matchs = reg.Matches(content);

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("StringBuilder builder = new StringBuilder();");
            builder.AppendLine("builder.Append(\"{\");");
            foreach (Match m in matchs)
            {
                if (m.Success && m.Groups.Count > 1)
                {
                    builder.AppendFormat("builder.AppendFormat(\"{0}:{{0}},\",{1});", m.Groups[1], m.Groups[1]);
                    builder.AppendLine();
                }
            }

            builder.AppendLine("builder.Append(\"}\");");
            builder.AppendLine("return builder.ToString();");

            return builder.ToString();
        }
    }

    class CreateTable : ICodeGenerator
    {
        public string ExampleInput
        {
            get
            {
                return @"Sign	Varchar(512)	Not null
SignType	Varchar(3)	Not null
OrderGmtCreate	Varchar(20)	";
            }
        }

        public string Generate(string[] inputs)
        {
            var lines = inputs;

            StringBuilder sb = new StringBuilder();
            foreach (string line in lines)
            {
                string[] cols = line.Replace("（", "(").Replace("）", ")").Split('\t');
                sb.Append("\t");
                int i = 0;
                foreach (var c in cols)
                {
                    sb.AppendFormat(i++ == 0 ? c.Trim() : c.Trim().ToUpper());
                    sb.Append(i == cols.Length ? string.Empty : " ");
                }
                sb.AppendLine(",");
            }
            return sb.ToString();
        }
    }

    class SpToDataCode : ICodeGenerator
    {
        public string ExampleInput
        {
            get
            {
                return @"alipayPaymentRequest
@OrderId INT = NULL
	,@Service VARCHAR(30) = NULL
	,@Subject VARCHAR(50) = NULL";
            }
        }

        public string Generate(string[] inputs)
        {
            var reg = new Regex(@"[^\s,@(=)]+");
            StringBuilder sb = new StringBuilder();
            string model = inputs[0].Trim();
            foreach (string line in inputs)
            {
                var matchs = reg.Matches(line);
                if (matchs.Count < 2) continue;
                string colName = matchs[0].Value.Trim("[] ".ToCharArray());
                sb.AppendFormat("database.AddInParameter(dbCommand, \"@{0}\", DbType.{1}, GetParameterValue({2}.{0}));"
                    , colName, GetType(matchs[1].Value), model);
                sb.AppendLine();
            }
            return sb.ToString();
        }

        private string GetType(string input)
        {
            input = input.ToUpper();
            switch (input)
            {
                case "INT": return "Int32";
                case "VARCHAR": return "String";
                case "DECIMAL": return "Decimal";
                case "BIGINT": return "Int64";
                case "DATETIME": return "Date";
                default: return "String";
            }
        }
    }

    class CodeGenerator
    {
        public static string Generate(string[] file)
        {
            string content = string.Join("\r\n", file);

            var reg = new Regex(@"<(\w+)>");

            var mths = reg.Matches(content);
            StringBuilder sb = new StringBuilder();
            foreach (Match m in mths)
            {
                if (m.Success && m.Groups.Count > 1)
                {
                    sb.AppendFormat("[XmlElement(\"{0}\")]", m.Groups[1]);
                    sb.AppendLine();
                    sb.AppendFormat("[AlipayParameter(\"{0}\")]", m.Groups[1]);
                    sb.AppendLine();
                    sb.AppendFormat("public string {0} {{ get; set; }}", BuildProperty(m.Groups[1].ToString()));
                    sb.AppendLine();
                    sb.AppendLine();
                }

                //        [XmlElement("gmt_last_modified_time")]
                //[AlipayParameter("gmt_last_modified_time")]
                //public string xx { get; set; }
            }

            return sb.ToString();
        }

        private static string BuildProperty(string name)
        {
            StringBuilder sb = new StringBuilder();
            string[] ss = name.Split('_');
            foreach (var s in ss)
            {
                sb.Append(s.Substring(0, 1).ToUpper());
                sb.Append(s.Substring(1));
            }
            return sb.ToString();
        }


        public static void GenerateCreateTable(string file)
        {
            var lines = File.ReadAllLines(file);

            StringBuilder sb = new StringBuilder();
            foreach (string line in lines)
            {
                string[] cols = line.Replace("（", "(").Replace("）", ")").Split('\t');
                sb.Append("\t");
                int i = 0;
                foreach (var c in cols)
                {
                    sb.AppendFormat(i++ == 0 ? c.Trim() : c.Trim().ToUpper());
                    sb.Append(" ");
                }
                sb.Remove(sb.Length - 1, 1).AppendLine(",");
            }

            File.WriteAllText("createtable.txt", sb.ToString());
        }
    }
    #endregion

    #region JAVA

    class CodeHelper
    {
        public static string UpperFirstChar(string input)
        {
            return input[0].ToString().ToUpper() + input.Substring(1);
        }

        public static string LowerFirstChar(string input)
        {
            return input[0].ToString().ToLower() + input.Substring(1);
        }

        public static string CamelString(string input, char split)
        {
            string[] ss = input.Split(split);
            string result = "";
            foreach (var s in ss)
            {
                result += UpperFirstChar(s);
            }
            return result;
        }

        public static string LowerFirstCamelString(string input, char split)
        {
            return LowerFirstChar(CamelString(input, split));
        }

        public static string ConvertToJavaType(string mysqlType)
        {
            mysqlType = mysqlType.ToLower();
            if (mysqlType.IndexOf("int") >= 0) return "Integer";
            if (mysqlType.IndexOf("char") >= 0) return "String";
            if (mysqlType.IndexOf("time") >= 0) return "Date";
            if (mysqlType.IndexOf("bit") >= 0) return "Integer";
            return mysqlType;
        }
    }

    class ObjectCopyProperty : ICodeGenerator
    {
        public static readonly Regex PropertyReg = new Regex(@"(private|public|package|protected)?(\s+)?[\w<>]+\s+(\w+);");

        public string ExampleInput
        {
            get
            {
                return @"oldPackage
    newPackage
    private int productId;

    private String name;

    private String roomName;

    private String type;
";
            }
        }

        public string Generate(string[] inputs)
        {
            if (inputs.Length <= 3) return "incorrect format";


            string newObj = inputs[1].Trim();
            string oldObj = inputs[0].Trim();
            List<string> properties = new List<string>();
            for (int i = 2; i < inputs.Length; i++)
            {
                string line = inputs[i].Trim();
                if (string.IsNullOrEmpty(line)) continue;

                var match = PropertyReg.Match(line);
                if (match.Success)
                {
                    properties.Add(match.Groups[3].Value);
                }
            }

            StringBuilder sb = new StringBuilder();
            foreach (var property in properties)
            {
                sb.AppendFormat("{0}.set{1}({2}.get{1}());", newObj, UpperFirstChar(property), oldObj);
                sb.AppendLine();
            }
            return sb.ToString();
        }

        private string UpperFirstChar(string input)
        {
            return input[0].ToString().ToUpper() + input.Substring(1);
        }
    }

    class ObjectCopyPropertyIfNotNull : ICodeGenerator
    {
        public static readonly Regex PropertyReg = new Regex(@"(private|public|package|protected)?(\s+)?[\w<>]+\s+(\w+);");

        public string ExampleInput
        {
            get
            {
                return @"oldPackage
    newPackage
    private int productId;

    private String name;

    private String roomName;

    private String type;
";
            }
        }

        public string Generate(string[] inputs)
        {
            if (inputs.Length <= 3) return "incorrect format";


            string newObj = inputs[1].Trim();
            string oldObj = inputs[0].Trim();
            List<string> properties = new List<string>();
            for (int i = 2; i < inputs.Length; i++)
            {
                string line = inputs[i].Trim();
                if (string.IsNullOrEmpty(line)) continue;

                var match = PropertyReg.Match(line);
                if (match.Success)
                {
                    properties.Add(match.Groups[3].Value);
                }
            }

            StringBuilder sb = new StringBuilder();
            foreach (var property in properties)
            {
                sb.AppendFormat("if ({2}.get{1}() != null) {0}.set{1}({2}.get{1}());", newObj, UpperFirstChar(property), oldObj);
                sb.AppendLine();
            }
            return sb.ToString();
        }

        private string UpperFirstChar(string input)
        {
            return input[0].ToString().ToUpper() + input.Substring(1);
        }
    }

    class MongoUpdateIfNotNull : ICodeGenerator
    {
        public static readonly Regex PropertyReg = new Regex(@"(private|public|package|protected)?(\s+)?[\w<>]+\s+(\w+);");

        public string ExampleInput
        {
            get
            {
                return @"oldPackage
    newPackage
    private int productId;

    private String name;

    private String roomName;

    private String type;
";
            }
        }

        public string Generate(string[] inputs)
        {
            if (inputs.Length <= 3) return "incorrect format";


            string newObj = inputs[1].Trim();
            string oldObj = inputs[0].Trim();
            List<string> properties = new List<string>();
            for (int i = 2; i < inputs.Length; i++)
            {
                string line = inputs[i].Trim();
                if (string.IsNullOrEmpty(line)) continue;

                var match = PropertyReg.Match(line);
                if (match.Success)
                {
                    properties.Add(match.Groups[3].Value);
                }
            }

            StringBuilder sb = new StringBuilder();
            newObj = string.IsNullOrEmpty(newObj) ? "" : newObj + ".";
            foreach (var property in properties)
            {
                sb.AppendFormat("if ({0}.get{1}() != null) update.set(\"{3}{2}\", {0}.get{1}());", oldObj, UpperFirstChar(property), property, newObj);
                sb.AppendLine();
            }
            return sb.ToString();
        }

        private string UpperFirstChar(string input)
        {
            return input[0].ToString().ToUpper() + input.Substring(1);
        }
    }

    class MongoUpdateInnerArrayIfNotNull : ICodeGenerator
    {
        public static readonly Regex PropertyReg = new Regex(@"(private|public|package|protected)?(\s+)?[\w<>]+\s+(\w+);");

        public string ExampleInput
        {
            get
            {
                return @"item
    items
    private int productId;

    private String name;

    private String roomName;

    private String type;
";
            }
        }

        public string Generate(string[] inputs)
        {
            if (inputs.Length <= 3) return "incorrect format";


            string newObj = inputs[1].Trim();
            string oldObj = inputs[0].Trim();
            List<string> properties = new List<string>();
            for (int i = 2; i < inputs.Length; i++)
            {
                string line = inputs[i].Trim();
                if (string.IsNullOrEmpty(line)) continue;

                var match = PropertyReg.Match(line);
                if (match.Success)
                {
                    properties.Add(match.Groups[3].Value);
                }
            }

            StringBuilder sb = new StringBuilder();
            foreach (var property in properties)
            {
                sb.AppendFormat("if ({0}.get{1}() != null) basicDBObject.append(\"$set\", new BasicDBObject(\"{3}.$.{2}\", {0}.get{1}()));", oldObj, UpperFirstChar(property), property, newObj);
                sb.AppendLine();
            }
            return sb.ToString();
        }

        private string UpperFirstChar(string input)
        {
            return input[0].ToString().ToUpper() + input.Substring(1);
        }
    }

    class ProductServiceEntityDomainConvert : ICodeGenerator
    {

        public string ExampleInput
        {
            get
            {
                return @"ProductServiceImage
ProductServiceImageEntity
    private Integer productId;

    private Integer version;

    private Integer serviceId;

    private String imageUrl;
";
            }
        }

        public string Generate(string[] inputs)
        {
            if (inputs.Length <= 3) return "incorrect format";

            string newObj = inputs[1].Trim();
            string oldObj = inputs[0].Trim();
            List<string> properties = new List<string>();
            for (int i = 2; i < inputs.Length; i++)
            {
                string line = inputs[i].Trim();
                if (string.IsNullOrEmpty(line)) continue;

                var match = ObjectCopyProperty.PropertyReg.Match(line);
                if (match.Success)
                {
                    properties.Add(match.Groups[2].Value);
                }
            }

            StringBuilder sb = new StringBuilder();
            foreach (var property in properties)
            {
                sb.AppendFormat("{0}.set{1}({2}.get{1}());", LowerFirstChar(newObj), UpperFirstChar(property), LowerFirstChar(oldObj));
                sb.AppendLine();
            }
            string buildMethod = string.Format(BuildMethodFormat, UpperFirstChar(newObj), LowerFirstChar(newObj)
                , UpperFirstChar(oldObj), LowerFirstChar(oldObj), sb);

            string convertMethod = string.Format(ListFormat, UpperFirstChar(newObj), LowerFirstChar(newObj)
                , UpperFirstChar(oldObj), LowerFirstChar(oldObj));

            return buildMethod + "\r\n\r\n" + convertMethod;
        }

        private string UpperFirstChar(string input)
        {
            return input[0].ToString().ToUpper() + input.Substring(1);
        }

        private string LowerFirstChar(string input)
        {
            return input[0].ToString().ToLower() + input.Substring(1);
        }

        private string ListFormat
        {
            get
            {
                return @"
 private List<{0}> build{0}List(List<{2}> {3}List, ProductProcessContext context) {{
        List<{0}> {1}List = new ArrayList<>();
        if (CollectionUtils.isEmpty({3}List)) return {1}List;
        for ({2} {3} : {3}List) {{
            {1}List.add(build{0}({3}, context));
        }}
        return {1}List;
    }}";

            }
        }

        private string BuildMethodFormat
        {
            get
            {
                return @"
private {0} build{0}({2} {3}, ProductProcessContext context) {{
        {0} {1} = new {0}();
        {1}.setVersion(context.getVersion());
        {4}
        {1}.setCreateBy(context.getUser());
        {1}.setUpdateBy(context.getUser());
        {1}.setCreateDate(new Date());
        {1}.setUpdateDate(new Date());
        return {1};
    }}";
            }
        }
    }

    class ProductServiceRepository : ICodeGenerator
    {
        public string ExampleInput
        {
            get { return "ProductOptionPackage"; }
        }

        public string Generate(string[] inputs)
        {
            string entityName = inputs[0].Trim();
            return string.Format(RepositoryFormat, CodeHelper.UpperFirstChar(entityName), CodeHelper.LowerFirstChar(entityName));
        }

        private string RepositoryFormat
        {
            get
            {
                return @"@Repository
public class {0}Repository extends CommonRepository {{
    public void saveSnapShot{0}({0}Entity {1}Entity) {{
        snapShotJpaAccess.save({1}Entity);
    }}

    public void deleteByProductId(Integer productId) {{
        String sql = ""delete from "" + {0}Entity.class.getName() + "" where productId=:productId"";
        Map<String, Object> params = new HashMap<>();
        params.put(""productId"", productId);
        jpaAccess.update(sql, params);
    }}
}}";
            }
        }
    }

    class ProductServiceService : ICodeGenerator
    {

        public string ExampleInput
        {
            get { return "ProductServiceImage"; }
        }

        public string Generate(string[] inputs)
        {
            string entity = inputs[0].Trim();
            return string.Format(ServiceFormat, CodeHelper.UpperFirstChar(entity), CodeHelper.LowerFirstChar(entity));
        }

        private string ServiceFormat
        {
            get
            {
                return @"@Service
public class {0}Service {{

    @Inject
    {0}Repository {1}Repository;

    @Transactional(value = DBConstants.PRODUCT_SNAPSHOT_JPA_TXN)
    public void createSnapShot{0}(List<{0}Entity> {1}EntityList) {{
        if (CollectionUtils.isEmpty({1}EntityList)) return;
        for ({0}Entity {1}Entity : {1}EntityList)
            {1}Repository.saveSnapShot{0}({1}Entity);
    }}

    @Transactional
    public void deleteAndCreate{0}ByProductId(Integer productId, List<{0}Entity> list, Date createDate, String createBy) {{
        {1}Repository.deleteByProductId(productId);
        if (!CollectionUtils.isEmpty(list)) {{
            for ({0}Entity {1}Entity : list) {{
                {0}Entity newProductImage = new {0}Entity();
                BeanUtils.copyProperties({1}Entity, newProductImage);
                newProductImage.setCreateDate(createDate);
                newProductImage.setCreateBy(createBy);
                {1}Repository.create(newProductImage);
            }}
        }}
    }}
}}";
            }
        }
    }

    class ConvertEntityList : ICodeGenerator
    {

        public string ExampleInput
        {
            get { return "ProductServiceImage\r\nProductOptionPackage"; }
        }

        public string Generate(string[] inputs)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string entity in inputs)
            {
                string ent = entity.Trim();
                if (string.IsNullOrEmpty(ent)) continue;
                sb.AppendFormat(ConvertFormat, CodeHelper.UpperFirstChar(entity), CodeHelper.LowerFirstChar(entity));
                sb.AppendLine();
                sb.AppendLine();
            }
            return sb.ToString();
        }

        private string ConvertFormat
        {
            get
            {
                return @"public List<{0}Entity> convertTo{0}EntityList(List<{0}Entity> {1}Entities) {{
        if (CollectionUtils.isEmpty({1}Entities)) return new ArrayList<>();
        List<{0}Entity> {1}EntityList = new ArrayList<>();
        for ({0}Entity {1}Entity : {1}Entities) {{
            {0}Entity newProductItem = new {0}Entity();
            BeanUtils.copyProperties({1}Entity, newProductItem);
            {1}EntityList.add(newProductItem);
        }}
        return {1}EntityList;
    }}";
            }
        }
    }

    class RepositoryGenerator : ICodeGenerator
    {

        public string ExampleInput
        {
            get { return "MaterialPackageItem\r\nMaterialPackage"; }
        }

        public string Generate(string[] inputs)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var input in inputs)
            {
                sb.AppendFormat(RepositoryFormat, CodeHelper.UpperFirstChar(input), CodeHelper.LowerFirstChar(input));
                sb.AppendLine();
                sb.AppendLine();
                sb.AppendLine();
            }
            return sb.ToString();
        }

        private string RepositoryFormat
        {
            get
            {
                return @"@Repository
public class {0}Repository {{
    @Inject
    private JPAAccess jpaAccess;

    public {0} get(int {1}Id) {{
        return jpaAccess.get({0}.class, {1}Id);
    }}

    public List<{0}> getByProductId(int productId) {{
        String sql = ""from "" + {0}.class.getName() + "" where productId=:productId"";
        Map<String, Object> params = new HashMap<>();
        params.put(""productId"", productId);
        return jpaAccess.find(sql, params);
    }}

    public List<{0}> getByIds(List<Integer> itemIds) {{
        String sql = ""from "" + {0}.class.getName() + "" where id in :itemIds"";
        Map<String, Object> params = new HashMap<>();
        params.put(""itemIds"", itemIds);
        return jpaAccess.find(sql, params);
    }}

    public void save({0} {1}) {{
        jpaAccess.save({1});
    }}

    public void update({0} {1}) {{
        jpaAccess.update({1});
    }}

    public void delete({0} {1}) {{
        jpaAccess.delete({1});
    }}
}}";
            }
        }
    }

    class ServiceGenerator : ICodeGenerator
    {
        public string ExampleInput
        {
            get { return "MaterialPackageItem\r\nMaterialPackage"; }
        }

        public string Generate(string[] inputs)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var input in inputs)
            {
                sb.AppendFormat(ServiceFormat, CodeHelper.UpperFirstChar(input), CodeHelper.LowerFirstChar(input));
                sb.AppendLine();
                sb.AppendLine();
                sb.AppendLine();
            }
            return sb.ToString();
        }

        private string ServiceFormat
        {
            get
            {
                return @"@Service
public class {0}Service {{
    @Inject
    private {0}Repository {1}Repository;

    public {0} get(int {1}Id) {{
        return {1}Repository.get({1}Id);
    }}
}}";
            }
        }
    }

    class DomainGenerator : ICodeGenerator
    {
        public string ExampleInput
        {
            get
            {
                return @"CREATE TABLE `wx_reservation` (  `reservation_id` int(11) NOT NULL AUTO_INCREMENT,  `shop_id` int(11) NOT NULL,  `image_frame_id` int(11) DEFAULT NULL COMMENT '案例ID,为NULL 代表预约的是店铺',  `open_id` varchar(50) NOT NULL COMMENT 'wx_user.open_id',  `user_name` varchar(50) NOT NULL COMMENT '预约姓名',  `phone` varchar(11) NOT NULL COMMENT '预约手机号码',  `user_logo_url` varchar(200) DEFAULT NULL COMMENT '冗余wx_user.user_logo_url',  `is_delete` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否删除：1.删除，默认值为0',  `reservation_time` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '预约时间',  `update_time` datetime DEFAULT NULL COMMENT '修改时间',  `update_by` varchar(30) DEFAULT NULL COMMENT '修改人',";
            }
        }

        public string Generate(string[] inputs)
        {
            StringBuilder builder = new StringBuilder();
            Regex tableReg = new Regex(@"create\s+table\s+[`]?(\w+)", RegexOptions.IgnoreCase);
            Regex colReg = new Regex(@"[`]?(\w+)[`]?\s+(\w+)[\(\s]+", RegexOptions.IgnoreCase);

            foreach (var input in inputs)
            {
                if (tableReg.IsMatch(input))
                {
                    string tableName = tableReg.Match(input).Groups[1].Value;
                    builder.AppendFormat("@Entity(name = \"{0}\")", tableName);
                    builder.AppendLine();
                    builder.AppendFormat("public class {0} {{", CodeHelper.CamelString(tableName, '_'));
                    builder.AppendLine();
                }
                else if (colReg.IsMatch(input))
                {
                    var match = colReg.Match(input);
                    string colName = match.Groups[1].Value;
                    string type = match.Groups[2].Value;
                    if (input.IndexOf("AUTO_INCREMENT", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        builder.AppendLine("\t@Id");
                        builder.AppendLine("\t@GeneratedValue");
                    }
                    builder.AppendLine("\t@Column(name = \"" + colName + "\")");
                    builder.AppendFormat("\tprivate {0} {1};", CodeHelper.ConvertToJavaType(type), CodeHelper.LowerFirstCamelString(colName, '_'));
                    builder.AppendLine();
                }
                builder.AppendLine();
            }
            builder.Append("}");
            return builder.ToString();
        }
    }

    class DomainToViewProperty : ICodeGenerator
    {
        public static readonly Regex PropertyReg = new Regex(@"(private|public|package|protected)?\s+\w+\s+(\w+);");

        public string ExampleInput
        {
            get
            {
                return @"
     @Id
    @GeneratedValue
    @Column(name = ""reservation_id"")
    private Integer reservationId;

    @Column(name = ""shop_id"")
    private Integer shopId;

    @Column(name = ""image_frame_id"")
    private Integer imageFrameId;

    @Column(name = ""open_id"")
    private String openId;
";
            }
        }

        public string Generate(string[] inputs)
        {
            if (inputs.Length <= 0) return "incorrect format";

            List<string> properties = new List<string>();
            for (int i = 0; i < inputs.Length; i++)
            {
                string line = inputs[i].Trim();
                if (string.IsNullOrEmpty(line)) continue;

                var match = PropertyReg.Match(line);
                if (match.Success)
                {
                    properties.Add(line);
                }
            }

            StringBuilder sb = new StringBuilder();

            foreach (var property in properties)
            {
                sb.AppendLine(property);
                sb.AppendLine();
            }
            return sb.ToString();
        }

        private string UpperFirstChar(string input)
        {
            return input[0].ToString().ToUpper() + input.Substring(1);
        }
    }

    class DbUnitXmlGenerator : ICodeGenerator
    {
        public string ExampleInput
        {
            get
            {
                return @"CREATE TABLE `wx_reservation` (  `reservation_id` int(11) NOT NULL AUTO_INCREMENT,  `shop_id` int(11) NOT NULL,  `image_frame_id` int(11) DEFAULT NULL COMMENT '案例ID,为NULL 代表预约的是店铺',  `open_id` varchar(50) NOT NULL COMMENT 'wx_user.open_id',  `user_name` varchar(50) NOT NULL COMMENT '预约姓名',  `phone` varchar(11) NOT NULL COMMENT '预约手机号码',  `user_logo_url` varchar(200) DEFAULT NULL COMMENT '冗余wx_user.user_logo_url',  `is_delete` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否删除：1.删除，默认值为0',  `reservation_time` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '预约时间',  `update_time` datetime DEFAULT NULL COMMENT '修改时间',  `update_by` varchar(30) DEFAULT NULL COMMENT '修改人',";
            }
        }

        public string Generate(string[] inputs)
        {
            StringBuilder builder = new StringBuilder();
            Regex tableReg = new Regex(@"create\s+table\s+[`]?(\w+)", RegexOptions.IgnoreCase);
            Regex colReg = new Regex(@"[`]?(\w+)[`]?\s+(\w+)[\(\s]+", RegexOptions.IgnoreCase);
            builder.Append("<");
            foreach (var input in inputs)
            {
                if (tableReg.IsMatch(input))
                {
                    string tableName = tableReg.Match(input).Groups[1].Value;
                    builder.Append(tableName).Append(" ");
                }
                else if (colReg.IsMatch(input))
                {
                    var match = colReg.Match(input);
                    string colName = match.Groups[1].Value;
                    string type = match.Groups[2].Value;
                    builder.AppendFormat("{0} = \"\" ", colName);
                }
            }
            builder.Append("/>");
            return builder.ToString();
        }
    }

    class ApiParameterGenerator : ICodeGenerator
    {
        public string ExampleInput
        {
            get
            {
                return @"   ""sourceEntry"": """",
                ""memo"": ""需要断桥隔热铝型材中空玻璃封窗，量大，贵店最近有优惠么？"",
                ""viewtime"": 0,
                ""rTotalAmount"": 0,
                ""categoryId"": 0,";
            }
        }
        public string Generate(string[] inputs)
        {
            if (inputs.Length == 0) return string.Empty;
            StringBuilder sb = new StringBuilder();
            foreach (var input in inputs)
            {
                string[] ss = input.Split(':');
                if (ss.Length < 2) continue;
                string name = ss[0].Replace("\"", "").Trim();
                string type = ss[1].IndexOf('"') >= 0 ? "String"
                    : (ss[1].IndexOf('.') > 0 ? "double"
                    : (ss[1].ToLower() == "true" || ss[1].ToLower() == "false" ? "boolean" : "int"));

                String fieldName = name;
                if (name.IndexOf('_') > 0)
                {
                    string[] nn = name.Split('_');
                    fieldName = nn[0];
                    for (int i = 1; i < nn.Length; i++)
                    {
                        fieldName += CodeHelper.UpperFirstChar(nn[i]);
                    }
                }
                sb.AppendFormat("@XmlElement(name = \"{0}\")", name);
                sb.AppendLine();
                sb.AppendFormat("private {0} {1};", type, fieldName);
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }

    class XmlElementAnotationGenerator : ICodeGenerator
    {
        public string ExampleInput
        {
            get
            {
                return @"private String name;

    private String nameInApp;

    private Integer itemType;";
            }
        }
        public string Generate(string[] inputs)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var input in inputs)
            {
                if (String.IsNullOrEmpty(input.Trim())) continue;
                var arr = input.Trim().TrimEnd(';').Trim().Split(' ');
                if (arr.Length < 1) continue;
                var name = arr[arr.Length - 1];
                if (input.IndexOf("List") >= 0)
                {
                    builder.AppendFormat("@XmlElementWrapper(name = \"{0}\")", name);
                    builder.AppendLine();
                }
                builder.AppendFormat("@XmlElement(name = \"{0}\")", name);
                builder.AppendLine();
                builder.AppendLine(input.Trim());
                builder.AppendLine();
            }
            return builder.ToString();
        }
    }

    class JdbcRowMapperGenerator : ICodeGenerator
    {
        public string ExampleInput
        {
            get
            {
                return @" entityName
  resultSet
    @Column(name = ""id"")
    private Integer id;

    @Column(name = ""title"")
    private String title;

    @Column(name = ""title_url"")
    private String titleUrl;";
            }
        }
        public string Generate(string[] inputs)
        {
            if (inputs.Length < 2) return string.Empty;
            string entityName = inputs[0].Trim();
            string resultSet = inputs[1].Trim();

            Regex colNameReg = new Regex(@"@Column\(name\s*=\s*""(\w+\d*\w+)""\)", RegexOptions.IgnoreCase);

            Regex whiteReg = new Regex(@"\s+");

            StringBuilder builder = new StringBuilder();
            bool column = false;
            string columnName = null;
            string filedName = null;
            string type = null;
            bool oneFinish = false;
            for (int i = 2; i < inputs.Length; i++)
            {
                string line = inputs[i].Trim(" ;".ToCharArray());
                if (!column && colNameReg.IsMatch(line))
                {
                    columnName = colNameReg.Match(line).Groups[1].Value;
                    column = true;
                }
                else if (column && !string.IsNullOrEmpty(line))
                {
                    string[] fields = whiteReg.Split(line);
                    filedName = fields[fields.Length - 1];
                    type = getType(fields[fields.Length - 2]);
                    column = false;
                }

                if (!string.IsNullOrEmpty(columnName) && !string.IsNullOrEmpty(filedName))
                {
                    builder.AppendFormat("{0}.set{1}({2}.{3}(\"{4}\"));", entityName, CodeHelper.UpperFirstChar(filedName)
                        , resultSet, type, columnName);
                    builder.AppendLine();
                    columnName = null;
                    filedName = null;
                    type = null;
                }
            }
            return builder.ToString();
        }

        private string getType(string type)
        {
            switch (type.ToLower())
            {
                case "date": return "getDate";
                case "integer":
                case "int": return "getInt";
                case "double": return "getDouble";
                case "boolean": return "getBoolean";
            }
            return "getString";
        }
    }


    class AssertObjEqureGenerator : ICodeGenerator
    {
        public static readonly Regex PropertyReg = new Regex(@"(private|public|package|protected)?(\s+)?[\w<>]+\s+(\w+);");

        public string ExampleInput
        {
            get
            {
                return @"oldPackage
    newPackage
    private int productId;

    private String name;

    private String roomName;

    private String type;
";
            }
        }

        public string Generate(string[] inputs)
        {
            if (inputs.Length <= 3) return "incorrect format";


            string newObj = inputs[1].Trim();
            string oldObj = inputs[0].Trim();
            List<string> properties = new List<string>();
            for (int i = 2; i < inputs.Length; i++)
            {
                string line = inputs[i].Trim();
                if (string.IsNullOrEmpty(line)) continue;

                var match = PropertyReg.Match(line);
                if (match.Success)
                {
                    properties.Add(match.Groups[3].Value);
                }
            }

            StringBuilder sb = new StringBuilder();
            foreach (var property in properties)
            {
                sb.AppendFormat("Assert.assertEquals({2}.get{1}(), {0}.get{1}());", newObj, UpperFirstChar(property), oldObj);
                sb.AppendLine();
            }
            return sb.ToString();
        }

        private string UpperFirstChar(string input)
        {
            return input[0].ToString().ToUpper() + input.Substring(1);
        }
    }

    class ESMapping : ICodeGenerator
    {
        public static readonly Regex PropertyReg = new Regex(@"(private|public|package|protected)?(\s+)?[\w<>]+\s+(\w+);");

        public string ExampleInput
        {
            get
            {
                return @"
    private int productId;

    private String name;

    private String roomName;

    private String type;
";
            }
        }

        public string Generate(string[] inputs)
        {
            if (inputs.Length <= 2) return "incorrect format";

            string collection = inputs[0];

            List<string> properties = new List<string>();
            for (int i = 1; i < inputs.Length; i++)
            {
                string line = inputs[i].Trim();
                if (string.IsNullOrEmpty(line)) continue;

                var match = PropertyReg.Match(line);
                if (match.Success)
                {
                    properties.Add(match.Groups[3].Value);
                }
            }

            StringBuilder sb = new StringBuilder();
            foreach (var property in properties)
            {
                sb.AppendFormat("{1}.add(new Field().name(\"{0}\").type(FieldType.String).index(FieldIndex.analyzed));", property, collection);
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }

    class ESIndexData : ICodeGenerator
    {
        public static readonly Regex PropertyReg = new Regex(@"(private|public|package|protected)?(\s+)?[\w<>]+\s+(\w+);");

        public string ExampleInput
        {
            get
            {
                return @"indexObject.getFields()
    item
    private int productId;

    private String name;

    private String roomName;

    private String type;
";
            }
        }

        public string Generate(string[] inputs)
        {
            if (inputs.Length <= 3) return "incorrect format";

            string collection = inputs[0].Trim();
            string dataObj = inputs[1].Trim();

            List<string> properties = new List<string>();
            for (int i = 1; i < inputs.Length; i++)
            {
                string line = inputs[i].Trim();
                if (string.IsNullOrEmpty(line)) continue;

                var match = PropertyReg.Match(line);
                if (match.Success)
                {
                    properties.Add(match.Groups[3].Value);
                }
            }

            StringBuilder sb = new StringBuilder();
            foreach (var property in properties)
            {
                sb.AppendFormat("{0}.add(new IndexField(\"{1}\", {2}.get{3}()));", collection, property, dataObj, UpperFirstChar(property));
                sb.AppendLine();
            }
            return sb.ToString();
        }

        private string UpperFirstChar(string input)
        {
            return input[0].ToString().ToUpper() + input.Substring(1);
        }
    }

    class ESSearchBuilder : ICodeGenerator
    {
        public string ExampleInput
        {
            get
            {
                return @"private List<Integer> Ids;

    private String nameInApp;

    private Integer itemType;";
            }
        }
        public string Generate(string[] inputs)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var input in inputs)
            {
                if (String.IsNullOrEmpty(input.Trim())) continue;
                if (!ESIndexData.PropertyReg.IsMatch(input)) continue;

                var arr = input.Trim().TrimEnd(';').Trim().Split(' ');
                if (arr.Length < 1) continue;
                var name = arr[arr.Length - 1];
                if (input.IndexOf("List") >= 0)
                {
                    builder.AppendFormat(@"if (!CollectionUtils.isEmpty(request.get{0}())) {{
            boolQueryBuilder.must(termsQuery(""{1}"", request.get{0}()));
}}", CodeHelper.UpperFirstChar(name), name);
                }
                else if (input.IndexOf("String") >= 0)
                {
                    builder.AppendFormat(@"if (StringUtils.hasText(request.get{0}())) {{
            boolQueryBuilder.must(wildcardQuery(""{1}"", wrapperAsterisk(request.get{0}())));
}}", CodeHelper.UpperFirstChar(name), name);
                }
                else
                {
                    builder.AppendFormat(@"if (request.get{0}() != null) {{
            boolQueryBuilder.must(termQuery(""{1}"", request.get{0}()));
}}", CodeHelper.UpperFirstChar(name), name);
                }
                builder.AppendLine();
            }
            return builder.ToString();
        }
    }
    #endregion
}

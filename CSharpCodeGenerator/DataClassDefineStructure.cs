namespace CSharpCodeGenerator
{
    public class DataClassDefineStructure
    {
        public class BookData
        {
            public List<SheetData> SheetDatas { get; set; } = new List<SheetData>();
        }

        public class SheetData
        {
            public List<DataClassData> DataClassDatas { get; set; } = new List<DataClassData>();
            public string? SheetName { get; set; }
            public List<string?> UsingNames { get; set; } = new List<string?>();
            public string? NamespaceName { get; set; }
        }

        public class DataClassData
        {
            public List<RowData> RowDatas { get; set; } = new List<RowData>();
            public string? ClassName { get; set; }
        }

        public class RowData
        {
            public string? DataType { get; set; }
            public string? Comment { get; set; }
            public string? VariableName { get; set; }
        }
    }
}

namespace CSharpCodeGenerator
{
    public class EnumDefineStructure
    {
        public class BookData
        {
            public List<SheetData> SheetDatas { get; set; } = new List<SheetData>();
        }

        public class SheetData
        {
            public List<EnumData> EnumDatas { get; set; } = new List<EnumData>();
            public string? SheetName { get; set; }
            public string? NamespaceName { get; set; }
        }

        public class EnumData
        {
            public List<RowData> RowDatas { get; set; } = new List<RowData>();
            public string? EnumName { get; set; }
        }

        public class RowData
        {
            public string? DataType { get; set; }
            public string? Comment { get; set; }
            public string? EnumElementName { get; set; }
            public object? Value { get; set; }
        }
    }
}

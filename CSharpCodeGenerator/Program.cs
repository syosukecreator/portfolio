using CSharpCodeGenerator;

if (!Directory.Exists("Generated"))
{
    Directory.CreateDirectory("Generated");
}

if (!Directory.Exists("Generated/Datas"))
{
    Directory.CreateDirectory("Generated/Datas");
}

if (!Directory.Exists("Generated/Enums"))
{
    Directory.CreateDirectory("Generated/Enums");
}

{
    var reader = new DataClassXlsxReader();
    var bookData = reader.GetBookData();

    foreach (var sheetData in bookData.SheetDatas)
    {
        Console.WriteLine(string.Format("SheetName: {0}, NamespaceName: {1}", sheetData.SheetName,
            sheetData.NamespaceName));

        foreach (var dataClassData in sheetData.DataClassDatas)
        {
            Console.WriteLine(string.Format("ClassName: {0}", dataClassData.ClassName));

            foreach (var rowData in dataClassData.RowDatas)
            {
                Console.WriteLine(string.Format("DataType: {0}, VariableName: {1}, Comment: {2}",
                    rowData.DataType, rowData.VariableName, rowData.Comment));
            }
        }
    }

    foreach (var sheetData in bookData.SheetDatas)
    {
        var text = new CsDataClass(sheetData).TransformText();
        File.WriteAllText(Path.Combine("Generated/Datas", sheetData.SheetName + "Data.cs"), text);
    }
}

{
    var reader = new EnumXlsxReader();
    var bookData = reader.GetBookData();

    foreach (var sheetData in bookData.SheetDatas)
    {
        Console.WriteLine(string.Format("SheetName: {0}, NamespaceName: {1}", sheetData.SheetName,
           sheetData.NamespaceName));

        foreach (var enumData in sheetData.EnumDatas)
        {
            Console.WriteLine(string.Format("EnumName: {0}", enumData.EnumName));

            foreach (var rowData in enumData.RowDatas)
            {
                Console.WriteLine(string.Format("EnumElementName:{0}, DataType:{1}, Value:{2}, Comment:{3}",
                    rowData.EnumElementName, rowData.DataType, rowData.Value, rowData.Comment));
            }
        }
    }

    foreach (var sheetData in bookData.SheetDatas)
    {
        var text = new CsEnum(sheetData).TransformText();
        File.WriteAllText(Path.Combine("Generated/Enums", sheetData.SheetName + "Enum.cs"), text);
    }
}

Console.WriteLine("Generate complete.");
Console.ReadKey();

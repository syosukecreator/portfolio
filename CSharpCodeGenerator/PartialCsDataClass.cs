using static CSharpCodeGenerator.DataClassDefineStructure;

namespace CSharpCodeGenerator
{
    public partial class CsDataClass
    {
        SheetData _sheetData;

        public CsDataClass(SheetData sheetData)
        {
            _sheetData = sheetData;
        }
    }
}

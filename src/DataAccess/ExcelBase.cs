using System;
using System.Linq;
using Aspose.Cells;


namespace DataAccess
{
    public abstract class ExcelBase
    {
        protected ExcelBase()
        {
            var license = new License();
            license.SetLicense("Aspose.Total.Product.Family.lic");
        }

        protected Workbook Workbook;
        protected Worksheet Worksheet;
        protected Cells Cells;
        protected Range Range;

        //protected NetOffice.ExcelApi.Application Application;
        //protected NetOffice.ExcelApi.Workbook Workbook;
        //protected NetOffice.ExcelApi.Worksheet Worksheet;
        //protected NetOffice.ExcelApi.Range Cells;
        //protected NetOffice.ExcelApi.Range Range;
        private string _outputPath;
        private string _inputFile;

        public string OutputPath
        {
            get { return _outputPath; }
            set
            {
                _outputPath = value;
                OutputPathChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public string InputFile
        {
            get { return _inputFile; }
            set
            {
                _inputFile = value;
                InputFileChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler OutputPathChanged;

        protected void SaveRangeToExcelFile(object[,] range)
        {
            OpenWorksheet();
            Worksheet.Cells.ImportTwoDimensionArray(range,0,0,false);
            Worksheet.Name = "Output";
            Workbook.Save(OutputPath);
            
        }

        protected void OpenWorksheet(string fileName ="")
        {
            Workbook = (string.IsNullOrWhiteSpace(fileName))
                ? new Workbook() 
                : new Workbook(fileName);
            Worksheet = Workbook.Worksheets.First();
        }

        public event EventHandler InputFileChanged;

        protected object[,] ReadRange()
        {
            object[,] valueArray;
            try
            {
                OpenWorksheet(InputFile);
                Range = Worksheet.Cells.MaxDisplayRange;
                valueArray = Worksheet.Cells.ExportArray(Range.FirstRow, Range.FirstColumn, Range.RowCount, Range.ColumnCount);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message,e.InnerException);
            }
            return valueArray;
        }
    }
}
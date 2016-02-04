using System.Linq;
using Core;
using DataAccess;
using Ninject;
using NUnit.Framework;
using Should;

namespace IntegrationTests
{
    public class ExcelTests : TestBase
    {
        private IReadExcelFiles _reader;

        [SetUp]
        public void Setup()
        {
            Bootstrap();
        }

        [Test, Category("BootStrap")]
        public void Should_Get_Excel_Reader_From_Bootstrap()
        {
            _reader = Kernel.Get<IReadExcelFiles>();
            _reader.ShouldNotBeNull();
            _reader.ShouldBeType<ReadExcelFiles>();
        }

        [Test, Category("BootStrap")]
        public void Should_Get_Mapper_From_Bootstrap()
        {
            var processor = Kernel.Get<IProcessor>();
            processor.ShouldNotBeNull();
            processor.ShouldBeType<Processor>();
        }

        [Test, Category("BootStrap")]
        public void Should_Get_Writer_From_Bootstrap()
        {
            var writer = Kernel.Get<IWriteExcelFiles>();
            writer.ShouldNotBeNull();
            writer.ShouldBeType<WriteExcelFiles>();
        }

        [Test, Category("Interop")]
        public void Should_Read_Columns()
        {
            var inputPath = @"C:\Development\GoDirect\ExcelCombinator\TestFiles\ColumnTest.xlsx";
            _reader = Kernel.Get<IReadExcelFiles>();
            _reader.InputFile = inputPath;
            var columns = _reader.ReadColumnNames().ToArray();
            columns.ShouldNotBeEmpty();
            columns.Count().ShouldEqual(3);
        }

        [Test, Category("Interop")]
        public void Should_Read_Full_WorkSheet()
        {
            var inputPath = @"C:\Development\GoDirect\ExcelCombinator\TestFiles\WorksheetTest.xlsx";
            _reader = Kernel.Get<IReadExcelFiles>();
            _reader.InputFile = inputPath;
            var result = _reader.ReadWorkSheet();
            result.Count.ShouldEqual(4);
            result[2].First().ShouldEqual("Row2Col1");
        }

        [Test, Category("Integration")]
        public void Input_Path_Change_Should_Fire()
        {
            var eventFired = false;
            _reader = Kernel.Get<IReadExcelFiles>();
            _reader.InputFileChanged += (sender, args) => eventFired = true;
            _reader.InputFile = "Something New";
            eventFired.ShouldBeTrue();
        }

        [Test, Category("Integration")]
        public void Output_Path_Change_Should_Fire()
        {
            var eventFired = false;
            var writer = Kernel.Get<IWriteExcelFiles>();
            writer.OutputPathChanged += (sender, args) => eventFired = true;
            writer.OutputPath = "Changed";
            eventFired.ShouldBeTrue();
        }

        [Test, Category("Integration")]
        public void Should_Write_An_Excel_File_From_A_List()
        {
            Bootstrap();
            var writer = Kernel.Get<IWriteExcelFiles>();
            var outputPath = @"C:\Development\GoDirect\ExcelCombinator\TestFiles\TestOutput.xlsx";
            writer.OutputPath = outputPath;
            GenerateSampleOutputList();
            writer.WriteToExcelFile(SampleOutputList);
            FileAssert.Exists(outputPath);
        }

    }
}
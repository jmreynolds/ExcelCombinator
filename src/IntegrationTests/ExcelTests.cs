using System.IO;
using System.Linq;
using System.Threading;
using Core;
using DataAccess;
using Ninject;
using NUnit.Framework;
using Should;

namespace IntegrationTests
{
    [TestFixture]
    public class ExcelTests : TestBase
    {
        private IReadExcelFiles _reader;

        [SetUp]
        public void Setup()
        {
            Thread.Sleep(5000);
            Bootstrap();
        }
        [TearDown]
        public void TearDown()
        {
            Thread.Sleep(5000);
            if(File.Exists($@"{TestPath}_Output.xlsx"))
                File.Delete($@"{TestPath}New PC list 1 12 17_Output.xlsx");
            if(File.Exists($@"{TestPath}TestOutput.xlsx"))
                File.Delete($@"{TestPath}TestOutput.xlsx");
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
            processor.ShouldBeType<ProcessMunicipalItems>();
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
            var inputPath = $@"{TestPath}ColumnTest.xlsx";
            _reader = Kernel.Get<IReadExcelFiles>();
            _reader.InputFile = inputPath;
            var columns = _reader.ReadColumnNames().ToArray();
            columns.ShouldNotBeEmpty();
            columns.Length.ShouldEqual(3);
        }

        [Test, Category("Interop")]
        public void Should_Read_Full_WorkSheet()
        {
            var inputPath = $@"{TestPath}New PC list 1 12 17.xlsx";
            _reader = Kernel.Get<IReadExcelFiles>();
            _reader.InputFile = inputPath;
            var result = _reader.ReadWorkSheet().ToList();
            result.Count.ShouldEqual(355);

            result.ShouldNotBeNull();
            var item = result.Where(x => x.Key == 1)
                .Select(x => x.Value)
                .FirstOrDefault()
                ?.FirstOrDefault(x => x.ColumnName == "Violation Date");
            item.ShouldNotBeNull();
            item?.Value.ShouldStartWith(@"11/20/2016");
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

        [Test, Category("Interop")]
        public void RowCountChanged_Event_Should_Fire()
        {
            var reader = Kernel.Get<IReadExcelFiles>();
            var inputPath = $@"{TestPath}New PC list 1 12 17.xlsx";
            reader.InputFile = inputPath;
            var rowsReadEventFired = false;
            var rowsCountEventFired = false;
            reader.RowsReadChanged += (sender, args) => rowsReadEventFired = true;
            reader.RowCountChanged += (sender, args) => rowsCountEventFired = true;
            reader.ReadWorkSheet();
            rowsReadEventFired.ShouldBeTrue();
            rowsCountEventFired.ShouldBeTrue();
            reader.RowsRead.ShouldEqual(355);
        }

        [Test, Category("Interop")]
        public void FullStackTest()
        {
            var reader = Kernel.Get<IReadExcelFiles>();
            var processor = Kernel.Get<IProcessMunicipalItems>();
            var writer = Kernel.Get<IWriteExcelFiles>();
            var inputPath = $@"{TestPath}New PC list 1 12 17.xlsx";
            var outputPath = $@"{TestPath}New PC list 1 12 17_Output.xlsx";
            var citationEventFired = false;
            var rowsToWriteEventFired = false;
            var inputFilterEventFired = false;
            var outputFileEventFired = false;
            var rowsWrittenEventFired = false;
            var rowCountEventFired = false;
            var rowsReadEventFired = false;


            reader.InputFileChanged += (sender, args) => inputFilterEventFired = true;
            reader.RowCountChanged += (sender, args) => rowCountEventFired = true;
            reader.RowsReadChanged += (sender, args) => rowsReadEventFired = true;
            processor.CitationsCountChanged += (sender, args) => citationEventFired = true;
            processor.RowsToWriteChanged += (sender, args) => rowsToWriteEventFired = true;
            writer.OutputPathChanged += (sender, args) => outputFileEventFired = true;
            writer.RowsWrittenChanged += (sender, args) => rowsWrittenEventFired = true;
            reader.InputFile = inputPath;
            writer.OutputPath = outputPath;
            var worksheet = reader.ReadWorkSheet();
            var forfitureInputs = processor.MapToCashBondForfitureInput(worksheet).ToList();
            var forfitureOutputs = processor.MapToCashBondForfitureOutput(forfitureInputs).ToList();
            writer.WriteToExcelFile(forfitureOutputs);
            

            citationEventFired.ShouldBeTrue();
            rowsToWriteEventFired.ShouldBeTrue();
            inputFilterEventFired.ShouldBeTrue();
            outputFileEventFired.ShouldBeTrue();
            rowsWrittenEventFired.ShouldBeTrue();
            rowCountEventFired.ShouldBeTrue();
            rowsReadEventFired.ShouldBeTrue();
            reader.RowCount.ShouldEqual((reader.RowsRead + 1), $"Rows Read: {reader.RowsRead + 1 } don't match Rows Available: {reader.RowCount}");
            reader.RowsRead.ShouldEqual(processor.Citations, $"Rows Read: {reader.RowsRead} don't match Citations Printed: {processor.Citations}");
            processor.RowsToWrite.ShouldEqual(forfitureOutputs.Count, "Rows don't match output stream");
            writer.RowsWritten.ShouldEqual(processor.RowsToWrite, "Rows out don't add up");
        }


        [Test, Category("Interop")]
        public void Should_Write_An_Excel_File_From_A_List()
        {
            var writer = Kernel.Get<IWriteExcelFiles>();
            var outputPath = $@"{TestPath}TestOutput.xlsx";
            writer.OutputPath = outputPath;
            GenerateSampleOutputList();
            writer.WriteToExcelFile(SampleOutputList);
            FileAssert.Exists(outputPath);
        }

        [Test, Category("Interop")]
        public void Reader_Should_Get_RowCount()
        {
            var inputPath = $@"{TestPath}New PC list 1 12 17.xlsx";
            _reader = Kernel.Get<IReadExcelFiles>();
            _reader.InputFile = inputPath;
            _reader.ReadWorkSheet();
            _reader.RowsRead.ShouldEqual(355);
        }
    }
}
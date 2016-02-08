using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Models;
using DataAccess;
using Moq;
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
            var result = _reader.ReadWorkSheet().ToList();
            result.Count.ShouldEqual(4);
            result[2].Value.First().Value.ShouldEqual("Row2Col1");
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
        public void RowCountChanged_Event_Should_Fire()
        {
            var reader = Kernel.Get<IReadExcelFiles>();
            var inputPath = @"C:\Development\GoDirect\ExcelCombinator\TestFiles\WorksheetTest.xlsx";
            reader.InputFile = inputPath;
            var rowsReadEventFired = false;
            var rowsCountEventFired = false;
            reader.RowsReadChanged += (sender, args) => rowsReadEventFired = true;
            reader.RowCountChanged += (sender, args) => rowsCountEventFired = true;
            reader.ReadWorkSheet();
            rowsReadEventFired.ShouldBeTrue();
            rowsCountEventFired.ShouldBeTrue();
            reader.RowsRead.ShouldEqual(4);
        }

        [Test, Category("Integration")]
        public void FullStackTest()
        {
            var reader = Kernel.Get<IReadExcelFiles>();
            var processor = Kernel.Get<IProcessor>();
            var writer = Kernel.Get<IWriteExcelFiles>();
            var inputPath = @"C:\Development\GoDirect\ExcelCombinator\TestFiles\bf notice Jan 30-Feb 2, 2016.xlsx";
            var outputPath = @"C:\Development\GoDirect\ExcelCombinator\TestFiles\SampleData_Output.xlsx";
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
            var forfitureInputs = processor.MapToCashBondForfitureInput(worksheet);
            var forfitureOutputs = processor.MapToCashBondForfitureOutput(forfitureInputs).ToList();
            writer.WriteToExcelFile(forfitureOutputs);


            citationEventFired.ShouldBeTrue();
            rowsToWriteEventFired.ShouldBeTrue();
            inputFilterEventFired.ShouldBeTrue();
            outputFileEventFired.ShouldBeTrue();
            rowsWrittenEventFired.ShouldBeTrue();
            rowCountEventFired.ShouldBeTrue();
            rowsReadEventFired.ShouldBeTrue();
            reader.RowCount.ShouldEqual(processor.Citations);
            processor.RowsToWrite.ShouldEqual(forfitureOutputs.Count());
            writer.RowsWritten.ShouldEqual(processor.RowsToWrite);
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

        [Test, Category("Integration")]
        public void Reader_Should_Get_RowCount()
        {
            var inputPath = @"C:\Development\GoDirect\ExcelCombinator\TestFiles\WorksheetTest.xlsx";
            _reader = Kernel.Get<IReadExcelFiles>();
            _reader.InputFile = inputPath;
            _reader.ReadWorkSheet();
            _reader.RowsRead.ShouldEqual(4);
        }
    }
}
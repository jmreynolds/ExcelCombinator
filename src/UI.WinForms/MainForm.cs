using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Core;
using Core.Exceptions;
using Core.Models;

// ReSharper disable LocalizableElement

namespace UI.WinForms
{
    public partial class MainForm : Form
    {
        private readonly IProcessor _processor;
        private readonly ILog _logger;
        private readonly IReadExcelFiles _reader;
        private readonly IWriteExcelFiles _writer;

        public MainForm(IReadExcelFiles reader, 
            IWriteExcelFiles writer, 
            IProcessor processor,
            ILog logger)
        {
            _reader = reader;
            _writer = writer;
            _processor = processor;
            _logger = logger;

            RegisterEvents();

            InitializeComponent();
        }

        private void RegisterEvents()
        {
            _reader.InputFileChanged += OnInputFileChanged;
            _reader.RowsReadChanged += (sender, args) => prgBarReading.Value = _reader.RowsRead;
            _reader.RowCountChanged += (sender, args) => prgBarReading.Maximum = _reader.RowCount-1;
            _processor.RowsToWriteChanged += (sender, args) => prgBarWriting.Maximum = _processor.RowsToWrite;
            _writer.RowsWrittenChanged += (sender, args) => prgBarWriting.Value = _writer.RowsWritten;
        }

        private void OnInputFileChanged(object sender, EventArgs args)
        {
            tbInput.Text = _reader.InputFile;
            _writer.OutputPath = _reader.InputFile.Replace(".xl", "_output.xl");
            tbOutput.Text = _writer.OutputPath;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            lblComplete.Visible = false;
            var result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                var inputPath = openFileDialog1.FileName;
                _reader.InputFile = inputPath;
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            prgBarReading.Visible = true;
            prgBarWriting.Visible = true;

            if (_reader.InputFile == string.Empty)
            {
                lblComplete.Text = "Please select an input path.";
                lblComplete.Visible = true;
                btnBrowse.Focus();
                return;
            }
            if (!File.Exists(_reader.InputFile))
            {
                lblComplete.Text = "Can't locate your file - please try again.";
                lblComplete.Visible = true;
                btnBrowse.Focus();
                return;
            }

            try
            {
                var worksheet = _reader.ReadWorkSheet();
                var forfitureInput = _processor.MapDynamicInput(worksheet);
                var forfitureOutput = _processor.MapDynamicInputToDynamicOutput(forfitureInput);
                var foo = (IEnumerable<CashBondForfitureOutput>) forfitureOutput;
                if (foo != null) _writer.WriteToExcelFile(foo);
            }
            catch (InvalidColumnException ex)
            {
                _logger.Log(ex);
                MessageBox.Show($"The format of the spreadsheet is invalid. " + Environment.NewLine +
                                $"Column '{ex.InvalidColumnName}' is not normally present." + Environment.NewLine +
                                $"Support Has Been Notified." + Environment.NewLine +
                                $"Please Drop the data in DropBox for verification" + Environment.NewLine +
                                $"and let the client know that it needs to be double checked.");
                return;
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                MessageBox.Show("There was a problem processing the file. " + Environment.NewLine +
                                "Please drop it in DropBox and contact support.");


            }
            lblComplete.Text = "Complete";
            lblComplete.Visible = true;
        }
    }
}
using System;
using System.IO;
using System.Windows.Forms;
using Core;

namespace UI.WinForms
{
    public partial class MainForm : Form
    {
        private readonly IProcessor _processor;
        private readonly IReadExcelFiles _reader;
        private readonly IWriteExcelFiles _writer;

        public MainForm(IReadExcelFiles reader, IWriteExcelFiles writer, IProcessor processor)
        {
            _reader = reader;
            _writer = writer;
            _processor = processor;

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

            var worksheet = _reader.ReadWorkSheet();
            var forfitureInput = _processor.MapToCashBondForfitureInput(worksheet);
            var forfitureOutput = _processor.MapToCashBondForfitureOutput(forfitureInput);
            _writer.WriteToExcelFile(forfitureOutput);
            lblComplete.Text = "Complete";
            lblComplete.Visible = true;
        }
    }
}
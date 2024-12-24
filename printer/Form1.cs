using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace printer
{
    public partial class Form1 : Form
    {
        private SaToPrint _printer;
        private PrintData _printData; // Biến lưu dữ liệu từ JSON

        public Form1()
        {
            InitializeComponent();
            _printer = new SaToPrint();

            // Thêm danh sách máy in vào ComboBox
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                cbx_PrintList.Items.Add(printer);
            }
        }

        // Đọc dữ liệu từ file JSON được chọn qua nút Browser
        private void LoadDataFromJson(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string jsonData = File.ReadAllText(filePath);
                    _printData = JsonConvert.DeserializeObject<PrintData>(jsonData);
                    int NumberVincode = _printData.ListVincode?.Count ?? 0;
                }
                else
                {
                    MessageBox.Show("File JSON không tồn tại.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đọc file JSON: {ex.Message}");
            }
        }

        // Lấy giá trị để in từ đối tượng PrintData
        private List<string> GetValuePrint()
        {
            var printValues = new List<string>();

            if (_printData != null)
            {
                printValues.Add(_printData.PartNumber ?? ""); // PartNumber
                printValues.Add(_printData.Name ?? ""); // PartName
                printValues.Add(_printData.Customer ?? ""); // Customer
                printValues.Add(_printData.CustomerCode ?? ""); // CustomerCode
                printValues.Add(_printData.QuantityBox ?? ""); // QuantityBox
                printValues.Add(_printData.PoNo ?? ""); // PoNo
                printValues.Add(_printData.Inspector ?? ""); // Inspector
                printValues.Add(_printData.ProductionDate ?? ""); // ProductionDate

                //Thay vì lấy từ _printData.QRcodeID, ta sẽ lấy từ đường dẫn file JSON
                string qrCodeID = Path.GetFileNameWithoutExtension(textBoxFilePath.Text);
                printValues.Add(qrCodeID); // QRCodeID lấy từ đường dẫn file JSON
                //printValues.Add(_printData.QRcodeID ?? ""); // ProductionDate
                printValues.Add(_printData.AccountNumber ?? ""); // Account Number
                printValues.Add(@_printData.Product ?? ""); // Hình Product
                printValues.Add(@_printData.QRcode ?? ""); // Hình QRcode
                printValues.AddRange(_printData.ListVincode); // Thêm tất cả các mã VinCode



            }

            return printValues;
        }

        // Nút chọn file JSON
        private void buttonBrowse_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                    openFileDialog.Title = "Chọn file JSON";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = openFileDialog.FileName;
                        LoadDataFromJson(filePath);

                        // Hiển thị đường dẫn file lên TextBox
                        textBoxFilePath.Text = filePath;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở dialog: {ex.Message}");
            }
        }

        // Nút in
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem người dùng đã chọn máy in chưa
                if (string.IsNullOrEmpty(cbx_PrintList.SelectedItem?.ToString()))
                {
                    MessageBox.Show("Vui lòng chọn máy in trước khi in.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Dừng thực hiện nếu chưa chọn máy in
                }
                if (_printData != null)
                {
                    int NumberVincode = _printData.ListVincode?.Count ?? 0;
                    var valuesToPrint = GetValuePrint();

                    if (valuesToPrint.Count > 0)
                    {
                        _printer.PrintLabel(valuesToPrint, cbx_PrintList.SelectedItem?.ToString() ?? "", NumberVincode);

                        // Xóa file JSON sau khi in thành công

                        string filePath = textBoxFilePath.Text; // Lấy đường dẫn từ TextBox
                        if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                        {
                            File.Delete(filePath);
                            MessageBox.Show("In thành công và file JSON đã bị xóa!");
                        }
                        else
                        {
                            MessageBox.Show("Không thể tìm thấy file JSON để xóa.");
                        }
                        // Xóa nội dung trong TextBox
                        textBoxFilePath.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Không có dữ liệu để in.");
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn file JSON trước khi in.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi in: {ex.Message}");
            }
        }

        private void cbx_PrintList_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void Preview_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (_printData != null)
                {
                    // Lấy dữ liệu để xem trước
                    var previewData = new List<string>
            {
                _printData.PartNumber,
                _printData.Name,
                _printData.Customer,
                _printData.CustomerCode,
                _printData.QuantityBox,
                _printData.PoNo,
                _printData.Inspector,
                _printData.ProductionDate,
                Path.GetFileNameWithoutExtension(textBoxFilePath.Text),
                _printData.AccountNumber,
                _printData.Product,
                _printData.QRcode
            };

                    // Lấy danh sách Vincode
                    var vincodeData = _printData.ListVincode ?? new List<string>();

                    // Mở Form2 và truyền dữ liệu
                    Form2 previewForm = new Form2(this);
                    previewForm.SetPreviewData(previewData, vincodeData);
                    previewForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn file JSON trước khi xem trước.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xem trước dữ liệu: {ex.Message}");
            }
        }

        private void Delete_button_Click(object sender, EventArgs e)
        {
            try
            {
                // Xóa file JSON khi nhấn delete

                string filePath = textBoxFilePath.Text; // Lấy đường dẫn từ TextBox
                if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                {
                    File.Delete(filePath);
                    MessageBox.Show("File JSON đã bị xóa!");
                }
                else
                {
                    MessageBox.Show("Không thể tìm thấy file JSON để xóa.");
                }
                // Xóa nội dung trong TextBox
                textBoxFilePath.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi in: {ex.Message}");
            }

        }
        public void PerformButton1Click()
        {
            button1.PerformClick(); // Thực hiện hành động click trên button1
        }

        public void PerformDeleteButtonClick()
        {
            Delete_button.PerformClick(); // Thực hiện hành động click trên Delete_button
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }
    }
}

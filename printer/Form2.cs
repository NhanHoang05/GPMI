using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SATO.MLV5.Common.Printer;
using static SATO.MLV5.CommonConst;

namespace printer
{
    public partial class Form2 : Form
    {  
        public Form2()
        {
            InitializeComponent();
        }

        private Form1 _form1; // Tham chiếu đến Form1

        public Form2(Form1 form1)
        {
            InitializeComponent();
            _form1 = form1; // Gán tham chiếu từ Form1 vào biến _form1
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        

        // Phương thức để nhận dữ liệu và hiển thị trong DataGridView
        public void SetPreviewData(List<string> previewData, List<string> vincodeData)
        {
            // Điền dữ liệu vào DataGridView1
            DataTable table1 = new DataTable();
            table1.Columns.Add("Supplier", typeof(string));
            table1.Columns.Add("GPMI", typeof(string));

            // Danh sách tên chi tiết
            List<string> detailNames = new List<string>
                 {
                        "PartNumber",
                        "PartName",
                        "Customer",
                        "CustomerID",
                        "QuantityBox",
                        "Po No",
                        "Inspector",
                        "Production Date",
                        "QRcode ID",
                        "Account Number"

                  };

            // Tạo dữ liệu từ danh sách previewData
            for (int i = 0; i < previewData.Count; i++)
            {
                if (i < detailNames.Count ) // Đảm bảo không vượt quá số lượng tên chi tiết
                {
                    table1.Rows.Add(detailNames[i], previewData[i]);
                }

            }

            dataGridView1.DataSource = table1;
            // Cấu hình DataGridView để loại bỏ khoảng trắng
            dataGridView1.RowHeadersVisible = false; // Ẩn cột headers trống bên trái
            dataGridView1.AllowUserToAddRows = false; // Tắt hàng trống cuối cùng
            dataGridView1.ScrollBars = ScrollBars.None; // Tắt thanh cuộn
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Cột co giãn vừa khít
            dataGridView1.DefaultCellStyle.Padding = new Padding(0); // Xóa padding
            dataGridView1.RowTemplate.Height = 25; // Điều chỉnh chiều cao hàng
                                                   // Căn giữa nội dung của cột đầu tiên
            dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            // Ẩn cột chọn cho DataGridView1
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            // Điền dữ liệu vào DataGridView2
            DataTable table2 = new DataTable();
            table2.Columns.Add("List Vincode", typeof(int));
            table2.Columns.Add("Info", typeof(string));

            for (int i = 0; i < vincodeData.Count; i++)
            {
                table2.Rows.Add(i + 1, vincodeData[i]);
            }

            dataGridView2.DataSource = table2;
            // Hiển thị hình ảnh trong pictureBox3 và pictureBox4 (Product và QRcode)
            try
            {
                // Lấy đường dẫn của hình Product và QRcode
                string productImagePath = previewData.LastOrDefault(); // Đây có thể là đường dẫn đến hình Product
                string qrCodeImagePath = previewData[previewData.Count - 2]; // Đây có thể là đường dẫn đến hình QRcode

                if (!string.IsNullOrEmpty(productImagePath) && File.Exists(productImagePath))
                {
                    pictureBox3.Image = Image.FromFile(productImagePath); // Hiển thị hình Product trong pictureBox3
                }
                else
                {
                    pictureBox3.Image = null; // Nếu không tìm thấy hình Product, không hiển thị
                }

                if (!string.IsNullOrEmpty(qrCodeImagePath) && File.Exists(qrCodeImagePath))
                {
                    pictureBox4.Image = Image.FromFile(qrCodeImagePath); // Hiển thị hình QRcode trong pictureBox4
                }
                else
                {
                    pictureBox4.Image = null; // Nếu không tìm thấy hình QRcode, không hiển thị
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải hình ảnh: {ex.Message}");
            }
            // Ẩn cột chọn cho DataGridView1
            dataGridView2.RowHeadersVisible = false;
        }

        private void Cancel_button_Click(object sender, EventArgs e)
        {
            // Kích hoạt nút Delete_button trong Form1 thông qua phương thức công khai
            _form1.PerformDeleteButtonClick();

            // Đóng Form2
            this.Close();
        }

        private void Print_button_Click_1(object sender, EventArgs e)
        {
            // Kích hoạt nút button1 trong Form1 thông qua phương thức công khai
            _form1.PerformButton1Click();

            // Đóng Form2
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
 
        }
    }
}

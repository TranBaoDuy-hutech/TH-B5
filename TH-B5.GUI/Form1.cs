using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TH_B5.BUS;
using TH_B5.DAL.Entitys;

namespace TH_B5.GUI
{
    public partial class Form1 : Form
    {
        private readonly StudentService studentService = new StudentService();
        private readonly FacultyService facultyService = new FacultyService();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            setGridViewStyle(dgvStudent);
            var listFacultys = facultyService.GetAll();
            var listStudents = studentService.GetAll();
            FillFalcultyCombobox(listFacultys);
            BindGrid(listStudents);
        }
        private void FillFalcultyCombobox(List<Faculty> listFacultys)
        {
            listFacultys.Insert(0, new Faculty());
            this.cmbFaculty.DataSource = listFacultys;
            this.cmbFaculty.DisplayMember = "FacultyName";
            this.cmbFaculty.ValueMember = "FacultyID";
        }
        private void BindGrid(List<Student> listStudent)
        {
            dgvStudent.Rows.Clear();
            foreach (var item in listStudent)
            {
                int index = dgvStudent.Rows.Add();
                dgvStudent.Rows[index].Cells[0].Value = item.StudentID;
                dgvStudent.Rows[index].Cells[1].Value = item.FullName;
                if (item.Faculty != null)
                    dgvStudent.Rows[index].Cells[2].Value =
                    item.Faculty.FacultyName;
                dgvStudent.Rows[index].Cells[3].Value = item.AverageScore + "";
            //    if (item.MajorID != null)
               dgvStudent.Rows[index].Cells[4].Value = item.MajorID + "";
              ShowAvatar(item.Avartar);
            }
        }
        private void ShowAvatar(string ImageName)
        {
            if (string.IsNullOrEmpty(ImageName))
            {
                picAvatar.Image = null;
            }
            else
            {
                string parentDirectory =
                Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
                string imagePath = Path.Combine(parentDirectory, "Images", ImageName);
                picAvatar.Image = Image.FromFile(imagePath);
                picAvatar.Refresh();
            }
        }
    
        public void setGridViewStyle(DataGridView dgview)
        {
            dgview.BorderStyle = BorderStyle.None;
            dgview.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgview.CellBorderStyle =
            DataGridViewCellBorderStyle.SingleHorizontal;
            dgview.BackgroundColor = Color.White;
            dgview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            using (var context = new StudentModel())
            {
                // Tạo đối tượng sinh viên mới
                Student newStudent = new Student
                {
                    StudentID = int.Parse(txtStudentID.Text),
                    FullName = txtFullName.Text,
                    AverageScore = decimal.Parse(txtAverageScore.Text),
                    FacultyID = (int)cmbFaculty.SelectedValue
                };

                studentService.AddStudent(newStudent);
                BindGrid(studentService.GetAll());
            }
        }
        private void dgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvStudent_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            using (var context = new StudentModel())
                if (e.RowIndex >= 0)
                {
                    // Lấy thông tin sinh viên từ hàng được chọn
                    int studentID = (int)dgvStudent.Rows[e.RowIndex].Cells[0].Value;
                    Student selectedStudent = context.Student.Find(studentID);

                    if (selectedStudent != null)
                    {
                        // Hiển thị thông tin của sinh viên vào các TextBox
                        txtStudentID.Text = selectedStudent.StudentID.ToString(); // Hiển thị mã sinh viên
                        txtFullName.Text = selectedStudent.FullName;
                        txtAverageScore.Text = selectedStudent.AverageScore.ToString();
                        cmbFaculty.SelectedValue = selectedStudent.FacultyID; // Chọn khoa tương ứng
                    }
                }
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            if (dgvStudent.SelectedRows.Count > 0)
            {
                var selectedRow = dgvStudent.SelectedRows[0];
                var studentId = (int)selectedRow.Cells[0].Value;

                var updatedStudent = new Student
                {
                    StudentID = studentId,
                    FullName = txtFullName.Text,
                    FacultyID = (int)cmbFaculty.SelectedValue,
                    AverageScore = decimal.Parse(txtAverageScore.Text),
                   
                };

                studentService.UpdateStudent(updatedStudent);
                BindGrid(studentService.GetAll());
            }
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            if (dgvStudent.SelectedRows.Count > 0)
            {
                var selectedRow = dgvStudent.SelectedRows[0];
                var studentId = (int)selectedRow.Cells[0].Value;

                studentService.DeleteStudent(studentId);
                BindGrid(studentService.GetAll());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           /* Form2 F = new Form2();
            this.Hide();
            F.ShowDialog();
            this.Show();
           */
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            var listStudents = new List<Student>();
            if (this.checkBox1.Checked)
                listStudents = studentService.GetAllHasNoMajor();
            else
                listStudents = studentService.GetAll();
            BindGrid(listStudents);
        }

        private void picAvatar_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif"; // Lọc các định dạng hình ảnh
                openFileDialog.Title = "Chọn hình ảnh";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Gọi hàm ShowAvatar với đường dẫn hình ảnh đã chọn
                    ShowAvatar(openFileDialog.FileName);
                }
            }
        }

    }
}



using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace manprac
{
    public partial class SettingsForm : Form
    {

        private String dbFileName = "RentDB";
        private string ConnString = "Data Source = RentDB; Version=3";

        public SettingsForm()
        {
            InitializeComponent();
        }

        public void CreateDB()
        {
            if (!File.Exists(dbFileName))
            {
                SQLiteConnection.CreateFile(dbFileName);
            }

            SQLiteConnection conn = new SQLiteConnection(ConnString);
            try
            {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = conn;

                cmd.CommandText = "CREATE TABLE IF NOT EXISTS Months (ID_Months INTEGER PRIMARY KEY AUTOINCREMENT, Name Text)";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "CREATE TABLE IF NOT EXISTS Renters (ID_Renters INTEGER PRIMARY KEY AUTOINCREMENT, Name Text)";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "CREATE TABLE IF NOT EXISTS Offices (ID_Office INTEGER PRIMARY KEY AUTOINCREMENT, ID_Renters INTEGER, Contract TEXT,  ID_Month INTEGER, Amount_Rent FLOAT, " +
                    "VAT FLOAT, Date_Payment timestamp, Note Text,  FOREIGN KEY (ID_Renters) REFERENCES Renters(ID_Renters),  FOREIGN KEY (ID_Month) REFERENCES Months(ID_Months))";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "CREATE TABLE IF NOT EXISTS ApartamentStatus (ID_Apartament_Status INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT)";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "CREATE TABLE IF NOT EXISTS Apartaments (ID_Apartament INTEGER PRIMARY KEY AUTOINCREMENT, ID_Renters INTEGER, Contract TEXT, ID_Month INTEGER, Amount_Rent FLOAT," +
                    " VAT FLOAT, Date_Payment timestamp, Apartament_Status INTEGER, Note TEXT, Amount_Payment FLOAT, FOREIGN KEY (ID_Renters) REFERENCES Renters(ID_Renters), " +
                    "FOREIGN KEY (ID_Month) REFERENCES Months(ID_Month), FOREIGN KEY (Apartament_Status) REFERENCES ApartamentStatus(ID_Apartament_Status))";
                cmd.ExecuteNonQuery();

                SQLiteCommand InsertMonths = new SQLiteCommand("INSERT INTO  [Months] (ID_Months, Name) " +
                    "VALUES (1, 'Январь'), (2, 'Февраль'), (3, 'Март'), (4, 'Апрель'), (5, 'Май'), (6, 'Июнь')," +
                    " (7, 'Июль'), (8, 'Август'), (9, 'Сентябрь'), (10, 'Октябрь'), (11, 'Ноябрь'), (12, 'Декабрь')", conn);
                InsertMonths.ExecuteNonQuery();

                SQLiteCommand InsertStatusFlats = new SQLiteCommand("INSERT INTO [ApartamentStatus] (ID_Apartament_Status, Name) " +
                "VALUES (1 , 'Нежилое'), (2, 'Жилое')", conn);
                InsertStatusFlats.ExecuteNonQuery();

            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Произошла ошибка при создании базы данных" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                conn.Close();
            }
        }
        private void createDB_Button_Click(object sender, EventArgs e)
        {
            if (File.Exists(dbFileName))
            {
                MessageBox.Show("База данных уже существует в проекте.", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                CreateDB();
                MessageBox.Show("База данных успешно создана.", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void clearRentersButton_Click(object sender, EventArgs e)
        {
            MainForm main = new MainForm();

            List<int> deleteRecordList = new List<int>();
            string textMessage = "Будут удалены арендаторы, которые отсутствуют в таблицах квартир и офисов." +
                " Чтобы удалить всех арендаторов необходимо предварительно удалить записи в квартирах и офисах. Продолжить?";
            if (MessageBox.Show(textMessage, "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SQLiteConnection conn = new SQLiteConnection(ConnString);
                conn.Open();

                SQLiteCommand readNullRenters = new SQLiteCommand("SELECT Renters.ID_Renters AS 'FreeRenters'  FROM Renters" +
                    " LEFT JOIN Offices " +
                    " ON Renters.ID_Renters = Offices.ID_Renters  LEFT JOIN Apartaments ON Renters.ID_Renters = Apartaments.ID_Renters " +
                    "WHERE Offices.ID_Renters IS NULL AND Apartaments.ID_Renters IS NULL", conn);

                try
                {
                    SQLiteDataReader reader = readNullRenters.ExecuteReader();
                    while (reader.Read())
                    {
                        deleteRecordList.Add(Convert.ToInt32(reader["FreeRenters"]));
                    }
                    reader.Close();


                    if (deleteRecordList.Count == 0)
                    {
                        MessageBox.Show("Нет записей подходящих под удаление.", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    for (int i = 0; i < deleteRecordList.Count; i++)
                    {
                        SQLiteCommand deleteRenters = new SQLiteCommand("Delete FROM [Renters] WHERE ID_Renters = @ID_Renters", conn);
                        deleteRenters.Parameters.AddWithValue("@ID_Renters", deleteRecordList.ElementAt(i));
                        deleteRenters.ExecuteNonQuery();
                    }
                    MessageBox.Show("Записи успешно удалены.", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void clearOfficesButton_Click(object sender, EventArgs e)
        {
            MainForm main = new MainForm();

            if (MessageBox.Show("Вы уверены что хотите удалить все записи из таблицы \"Офисы\"?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SQLiteConnection conn = new SQLiteConnection(ConnString);
                conn.Open();

                SQLiteCommand deleteAllRecors = new SQLiteCommand("DELETE FROM Offices", conn);
                try
                {
                    deleteAllRecors.ExecuteNonQuery();
                    MessageBox.Show("Данные из таблицы успешно удалены.", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void clearFlatsButton_Click(object sender, EventArgs e)
        {
            MainForm main = new MainForm();
            if (MessageBox.Show("Вы уверены что хотите удалить все записи из таблицы \"Квартиры\"?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SQLiteConnection conn = new SQLiteConnection(ConnString);
                conn.Open();

                SQLiteCommand deleteAllRecors = new SQLiteCommand("DELETE FROM Apartaments", conn);
                try
                {
                    deleteAllRecors.ExecuteNonQuery();
                    MessageBox.Show("Данные из таблицы успешно удалены.", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void savaDB_Button_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = dbFileName;
            try
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    //string pathNewDB = @"C:\Users\Алексей\Downloads\SaveDBHere";
                    string pathNewDB = Path.GetDirectoryName(dialog.FileName);
                    using (var location = new SQLiteConnection(ConnString))
                    using (var destination = new SQLiteConnection(string.Format(@"Data Source={0}\RentDB; Version=3;", pathNewDB)))
                    {
                        location.Open();
                        destination.Open();
                        location.BackupDatabase(destination, "main", "main", -1, null, 0);
                        location.Close();
                        destination.Close();
                    }
                    MessageBox.Show("Резервная копия базы данных успешно создана.", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void loadDB_Button_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            var fileName = @"\RentDB";
            var localProgramPath = Application.StartupPath;
            try
            {
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    if (openFile.SafeFileName == dbFileName)
                    {
                        File.Delete(dbFileName);
                        string getFilePath = Path.GetFullPath(openFile.FileName);
                        File.Copy(getFilePath, localProgramPath + fileName);
                        MessageBox.Show("Загрузка БД из копии успешно завершена. ", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("Выбраный файл не является резервной копией БД.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка. " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RecreateDB_Button_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("При пересоздании базы данных будут утеряны все записи. Продолжить? ", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (File.Exists(dbFileName))
                {
                   
                    try
                    {
                        File.Delete(dbFileName);
                        CreateDB();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Произошла ошибка. " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    MessageBox.Show("База данных успешно пересоздана.", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }



        }

        private void helpLabel_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath;
            MessageBox.Show("В случае, если по каким либо причинам загрузить копию БД программно не удаётся  " +
                "можно выполнить загрузку вручную. \r Для этого необходимо переместить резервную копию БД в папку с программой  " +
                "по пути \r" + path, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}

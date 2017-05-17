using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_Lab6
{
    public partial class MainForm : Form
    {
        private PostgresDatabase _db = new PostgresDatabase();
        private List<string> _ids = new List<string>();
        private bool _is_table_updating = false;
        private List<List<string>> table = null;
        private string _sql = "";

        public MainForm()
        {
            InitializeComponent();
            GetTablesList();
        }

        public void GetTablesList()
        {
            if (_is_table_updating)
            {
                return;
            }
            _is_table_updating = true;

            _sql =    "SELECT table_name " +
                            "FROM information_schema.tables " +
                            "WHERE table_schema = 'public' " +
                                    "AND table_type = 'BASE TABLE' " +
                            "ORDER BY table_name;";

            List<List<string>> tables = _db.QueryList(_sql);
            guiTables.Items.Clear();
            for (int row = 1; row < tables.Count; row++)
            {
                for (int col = 0; col < tables[row].Count; col++)
                {
                    guiTables.Items.Add(tables[row][col]);
                }
            }

            guiTables.SelectedIndex = 0;

            _is_table_updating = false;
        }

        /// <summary>
        /// If sender == null and e == null then old values from this.tabel will be used.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guiQueryTable_Click(object sender, EventArgs e)
        {
            if (_is_table_updating)
            {
                return;
            }
            _is_table_updating = true;

            guiUpdateTable.Enabled = true;
            guiSearchGroupBox.Enabled = true;
            guiFilterGroupBox.Enabled = true;

            // if not called manually
            if (!(sender == null && e == null))
            {
                _sql = string.Format("SELECT * FROM {0};", guiTables.SelectedItem.ToString());
                table = _db.QueryList(_sql);
            }

            guiDGV.Rows.Clear();
            guiDGV.Columns.Clear();

            if (table.Count == 0)
            {
                return;
            }
            // adding columns
            guiSearchColumn.Items.Clear();
            guiFilterColumn.Items.Clear();
            guiSearhPattern.Clear();

            for (int col_index = 0; col_index < table[0].Count; col_index++)
            {   // in first row there always column headers
                guiDGV.Columns.Add(table[0][col_index], table[0][col_index]);

                if (table[0][col_index].ToLower() == "id")
                {
                    guiDGV.Columns[col_index].Visible = false;
                }
                else
                {
                    guiSearchColumn.Items.Add(table[0][col_index]);
                    guiFilterColumn.Items.Add(table[0][col_index]);
                }
            }
            // managing search elements
            guiSearchColumn.SelectedIndex = 0;
            guiFilterColumn.SelectedIndex = 0;
            guiFilterDirection.SelectedIndex = 0;

            // filling table
            _ids.Clear();
            for (int row = 1; row < table.Count; row++)
            {
                guiDGV.Rows.Add();
                for (int col = 0; col < table[row].Count; col++)
                {
                    if (table[row][col] == "True" || table[row][col] == "False")
                    {
                        guiDGV.Rows[row - 1].Cells[col] = new DataGridViewCheckBoxCell(false);
                        guiDGV.Rows[row - 1].Cells[col].Value = table[row][col] == "True";
                    }
                    else
                    {
                        guiDGV.Rows[row - 1].Cells[col].Value = table[row][col];
                    }

                    if (table[0][col] == "id")
                    {
                        _ids.Add(table[row][col]);
                    }
                }
            }

            _is_table_updating = false;
        }

        private void guiUpdateTable_Click(object sender, EventArgs e)
        {
            if (_is_table_updating)
            {
                return;
            }
            _is_table_updating = true;

            for (int row = 0; row < guiDGV.Rows.Count-1; row++)
            {
                // if record is new
                if (guiDGV.Rows[row].Cells["id"].Value == null)
                {
                    InsertRow(row);
                    continue;
                }

                _sql = string.Format("UPDATE {0} SET ", guiTables.SelectedItem.ToString());

                for (int col = 0; col < guiDGV.Rows[row].Cells.Count; col++)
                {
                    // dont touch id
                    if (guiDGV.Columns[col].HeaderText == "id" || guiDGV.Rows[row].Cells[col].Value == null)
                    {
                        continue;
                    }

                    if (guiDGV.Rows[row].Cells[col] is DataGridViewCheckBoxCell)
                    {
                        _sql += string.Format("{0} = {1}",
                                guiDGV.Rows[row].Cells[col].OwningColumn.HeaderText,
                                (bool)guiDGV.Rows[row].Cells[col].Value ? "TRUE" : "FALSE");
                    }
                    else
                    {
                        double number = 0;
                        if (double.TryParse(guiDGV.Rows[row].Cells[col].Value.ToString(), out number))
                        {
                            _sql += string.Format("{0} = {1}",
                                guiDGV.Rows[row].Cells[col].OwningColumn.HeaderText,
                                guiDGV.Rows[row].Cells[col].Value.ToString());
                        }
                        else
                        {
                            _sql += string.Format("{0} = '{1}'",
                                guiDGV.Rows[row].Cells[col].OwningColumn.HeaderText,
                                guiDGV.Rows[row].Cells[col].Value.ToString());
                        }
                    }
                    // adding space and ,
                    _sql += ", ";
                }

                _sql += string.Format("WHERE id = {0};", guiDGV.Rows[row].Cells["id"].Value.ToString());
                _sql = _sql.Replace(", WHERE", " WHERE");

                // trying to update
                try
                {
                    _db.Exec(_sql);
                }
                catch (Exception ex)
                {
                    guiDGV.ClearSelection();
                    guiDGV.Rows[row].Selected = true;
                    Logger.Error("Cannot update selected cortege.\n" + ex.Message);
                    break;
                }
            }

            _is_table_updating = false;
        }

        private void InsertRow(int row)
        {
            if (row < 0)
            {
                throw new IndexOutOfRangeException();
            }

            _sql = string.Format("INSERT INTO {0} (", guiTables.SelectedItem.ToString());

            for (int col = 0; col < guiDGV.Rows[row].Cells.Count; col++)
            {
                // dont touch id
                if (guiDGV.Columns[col].HeaderText != "id")
                {
                    _sql += string.Format("{0}, ", guiDGV.Columns[col].HeaderText);
                }
            }

            _sql += ") VALUES (";
            _sql = _sql.Replace(", )", ")");

            for (int col = 0; col < guiDGV.Rows[row].Cells.Count; col++)
            {
                // dont touch id
                if (guiDGV.Columns[col].HeaderText != "id")
                {
                    if (guiDGV.Rows[row].Cells[col] is DataGridViewCheckBoxCell)
                    {
                        _sql += (bool)guiDGV.Rows[row].Cells[col].Value ? "TRUE" : "FALSE";
                    }
                    else
                    {
                        double number = 0;
                        if (double.TryParse(guiDGV.Rows[row].Cells[col].Value.ToString(), out number))
                        {
                            _sql += guiDGV.Rows[row].Cells[col].Value.ToString();
                        }
                        else
                        {
                            _sql += string.Format("'{0}'", guiDGV.Rows[row].Cells[col].Value.ToString());
                        }
                    }
                    // adding space and ,
                    _sql += ", ";
                }
            }

            _sql += ");";
            _sql = _sql.Replace(", )", ")");

            // trying to insert
            try
            {
                _db.Exec(_sql);
            }
            catch (Exception ex)
            {
                guiDGV.ClearSelection();
                guiDGV.Rows[row].Selected = true;
                Logger.Error("Cannot insert selected cortege.\n" + ex.Message);
            }
        }

        private void guiDGV_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (_is_table_updating)
            {
                return;
            }
            _is_table_updating = true;

            try
            {
                _db.Exec(string.Format("DELETE FROM {0} WHERE id = {1};",
                guiTables.SelectedItem.ToString(),
                _ids[e.RowIndex]));
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to remove the row.\n" + ex.Message);
            };

            _is_table_updating = false;
        }

        private void guiSearch_Click(object sender, EventArgs e)
        {
            if (_is_table_updating)
            {
                return;
            }
            _is_table_updating = true;

            string pattern = _db.SecureString(guiSearhPattern.Text);
            if (pattern == "")
            {
                _is_table_updating = false;
                return;
            }

            _sql = "";
            double number = 0;
            
            if (pattern.ToLower() == "true" || pattern.ToLower() == "false")
            {
                _sql = string.Format("SELECT * FROM {0} WHERE {1} = {2};",
                guiTables.SelectedItem.ToString(),
                guiSearchColumn.SelectedItem.ToString(),
                pattern.ToUpper());
            }
            else if (double.TryParse(pattern, out number))
            {
                _sql = string.Format("SELECT * FROM {0} WHERE {1} = {2};",
                guiTables.SelectedItem.ToString(),
                guiSearchColumn.SelectedItem.ToString(),
                pattern);
                
            }
            else
            {
                _sql = string.Format("SELECT * FROM {0} WHERE {1} ~ '.*{2}.*';",
                guiTables.SelectedItem.ToString(),
                guiSearchColumn.SelectedItem.ToString(),
                pattern);
            }

            // trying to select
            try
            {
                table = _db.QueryList(_sql);

                _is_table_updating = false;
                guiQueryTable_Click(null, null);
                _is_table_updating = true;
            }
            catch
            {
                Logger.Info("No items found.");
            }
            finally
            {
                _is_table_updating = false;
            }
        }

        private void guiFilter_Click(object sender, EventArgs e)
        {
            if (_is_table_updating)
            {
                return;
            }
            _is_table_updating = true;

            string order_sql = _sql;
            if (order_sql.IndexOf("SELECT") < 0)
            {
                order_sql = string.Format("SELECT * FROM {0};", guiTables.SelectedItem.ToString());
            }
            // 
            string direction = guiFilterDirection.SelectedItem.ToString() == "ascending" ? "ASC" : "DESC";
            order_sql += string.Format(" ORDER BY {0} {1};", guiFilterColumn.SelectedItem.ToString(), direction);
            order_sql = order_sql.Replace("; ORDER", " ORDER");

            // trying to select
            try
            {
                table = _db.QueryList(order_sql);

                _is_table_updating = false;
                guiQueryTable_Click(null, null);
                _is_table_updating = true;
            }
            catch
            {
                Logger.Info("No items found.");
            }
            finally
            {
                _is_table_updating = false;
            }
        }
    }
}

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
        private bool _is_table_updating = false;
        private BindingSource _binding_source = null;
        private string _sql = "";

        public MainForm()
        {
            InitializeComponent();
            _binding_source = new BindingSource();
            guiDGV.DataSource = _binding_source;
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
            guiSortGroupBox.Enabled = true;
            guiMathGroupBox.Enabled = true;
            guiFilterGroupBox.Enabled = true;

            // if not called manually
            if (!(sender == null && e == null))
            {
                _sql = string.Format("SELECT * FROM {0};", guiTables.SelectedItem.ToString());
                _binding_source.DataSource = _db.QueryDataTable(_sql);
            }

            //if (((DataTable)_binding_source.DataSource).Rows.Count == 0)
            //{
            //    return;
            //}
            // adding columns
            guiSearchColumn.Items.Clear();
            guiSortColumn.Items.Clear();
            guiMathColumn.Items.Clear();
            guiFilterColumn.Items.Clear();
            guiSearhPattern.Clear();

            foreach (DataColumn column in ((DataTable)_binding_source.DataSource).Columns)
            {   // in first row there always column headers
                if (column.ColumnName.ToLower() == "id")
                {
                    guiDGV.Rows[0].Cells["id"].OwningColumn.Visible = false;
                }
                else
                {
                    guiSearchColumn.Items.Add(column.ColumnName.ToLower());
                    guiSortColumn.Items.Add(column.ColumnName.ToLower());
                    guiMathColumn.Items.Add(column.ColumnName.ToLower());
                    guiFilterColumn.Items.Add(column.ColumnName.ToLower());
                }
            }
            // managing search elements
            guiSearchColumn.SelectedIndex = 0;
            guiSortColumn.SelectedIndex = 0;
            guiSortDirection.SelectedIndex = 0;
            guiMathAlgo.SelectedIndex = 0;
            guiMathColumn.SelectedIndex = 0;
            guiFilterColumn.SelectedIndex = 0;
            guiFilterPred.SelectedIndex = 2;

            _is_table_updating = false;
        }

        private void guiUpdateTable_Click(object sender, EventArgs e)
        {
            if (_is_table_updating)
            {
                return;
            }
            _is_table_updating = true;

            try
            {
                _db.UpdateData((DataTable)_binding_source.DataSource);
            }
            catch (Exception ex)
            {
                Logger.Error("Cannot update data in table.");
            }
            finally
            {
                _is_table_updating = false;
            }
            
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
                _binding_source.DataSource = _db.QueryDataTable(_sql);

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

        private void guiSort_Click(object sender, EventArgs e)
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
            string direction = guiSortDirection.SelectedItem.ToString() == "ascending" ? "ASC" : "DESC";
            order_sql += string.Format(" ORDER BY {0} {1};", guiSortColumn.SelectedItem.ToString(), direction);
            order_sql = order_sql.Replace("; ORDER", " ORDER");

            // trying to select
            try
            {
                _binding_source.DataSource = _db.QueryDataTable(order_sql);

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

        private void guiMath_Click(object sender, EventArgs e)
        {
            string function = "";

            switch (guiMathAlgo.SelectedItem.ToString().ToLower())
            {
                case "min":
                    function = "MIN";
                    break;
                case "max":
                    function = "MAX";
                    break;
                case "average":
                    function = "AVG";
                    break;
                case "sum":
                    function = "min";
                    break;
                default:
                    break;
            }

            _sql = string.Format("SELECT {0} ({1}) FROM {2};",
                function,
                guiMathColumn.SelectedItem.ToString(),
                guiTables.SelectedItem.ToString());

            // trying to select
            try
            {
                _binding_source.DataSource = _db.QueryDataTable(_sql);
            }
            catch
            {
                Logger.Info("Cannot evaluate algorithm found.");
            }
            finally
            {
                _is_table_updating = false;
            }
        }

        private void guiFilter_Click(object sender, EventArgs e)
        {
            _sql = string.Format("SELECT * FROM {0} WHERE {1} {2} {3};",
                guiTables.SelectedItem.ToString(),
                guiFilterColumn.SelectedItem.ToString(),
                guiFilterPred.SelectedItem.ToString(),
                guiFilterValue.Value.ToString());

            // trying to select
            try
            {
                _binding_source.DataSource = _db.QueryDataTable(_sql);
            }
            catch
            {
                Logger.Info("Cannot filter.");
            }
            finally
            {
                _is_table_updating = false;
            }
        }
    }
}

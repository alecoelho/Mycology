using CrystalDecisions.CrystalReports.Engine;
using Micologia.Modelo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Micologia
{
    public partial class frmRelatorio : Form
    {
        public frmRelatorio()
        {
            InitializeComponent();
        }

        

      public frmRelatorio(IList<vwMICOLOGIA_PEDIDOEXAME> List, ReportClass Rel)
        {
            InitializeComponent();

            DataTable x = DataTableFromIEnumerable(List);

            Rel.SetDataSource(x);
            crystalReportViewer1.ReportSource = Rel;
        }
        public frmRelatorio(DataTable Dados, ReportClass Rel)
        {
            InitializeComponent();

            Rel.SetDataSource(Dados);
            crystalReportViewer1.ReportSource = Rel;
        }
        private DataTable DataTableFromIEnumerable(IEnumerable ien)
        {
            DataTable dt = new DataTable();
            foreach (object obj in ien)
            {
                Type t = obj.GetType();
                PropertyInfo[] pis = t.GetProperties();
                if (dt.Columns.Count == 0)
                {
                    foreach (PropertyInfo pi in pis)
                    {
                        Type pt = pi.PropertyType;
                        if (pt.IsGenericType && pt.GetGenericTypeDefinition() == typeof(Nullable<>))
                            pt = Nullable.GetUnderlyingType(pt);
                        dt.Columns.Add(pi.Name, pt);
                    }
                }
                DataRow dr = dt.NewRow();
                foreach (PropertyInfo pi in pis)
                {
                    object value = pi.GetValue(obj, null);
                    dr[pi.Name] = value;
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

     
    }
}

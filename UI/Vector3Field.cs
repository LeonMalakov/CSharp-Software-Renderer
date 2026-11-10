using System;
using System.Windows.Forms;
using KG_Task4.Structures;

namespace KG_Task4.UI
{
    internal class Vector3Field
    {
        private TextBox _x;
        private TextBox _y;
        private TextBox _z;

        private Action<Vector3> _valueChanged;

        public static void Create(Control parent, int yOffset, string header, Vector3 currentValue, Action<Vector3> valueChanged)
        {
            Vector3Field field = new Vector3Field();

            field._valueChanged = valueChanged;

            field._x = CreateFloatField(30, 20);
            field._y = CreateFloatField(80, 20);
            field._z = CreateFloatField(130, 20);

            field._x.TextChanged += field.ValueChanged;
            field._y.TextChanged += field.ValueChanged;
            field._z.TextChanged += field.ValueChanged;

            field._x.Text = currentValue.X.ToString();
            field._y.Text = currentValue.Y.ToString();
            field._z.Text = currentValue.Z.ToString();

            GroupBox gb = new GroupBox();
            gb.Text = header;
            gb.Controls.Add(field._x);
            gb.Controls.Add(field._y);
            gb.Controls.Add(field._z);

            gb.Width = parent.Width;
            gb.Height = 55;

            gb.Location = new System.Drawing.Point(0, yOffset);

            parent.Controls.Add(gb);
        }

        private void ValueChanged(object sender, EventArgs e)
        {
            Vector3 v = new Vector3();

            if (_x.Text == string.Empty) _x.Text = 0.ToString();
            if (_y.Text == string.Empty) _y.Text = 0.ToString();
            if (_z.Text == string.Empty) _z.Text = 0.ToString();

            if (!Utilities.FloatTryParse(_x.Text, out v.X)) return;

            if (!Utilities.FloatTryParse(_y.Text, out v.Y)) return;

            if (!Utilities.FloatTryParse(_z.Text, out v.Z)) return;

            _valueChanged?.Invoke(v);
        }

        private static TextBox CreateFloatField(int xOffset = 0, int yOffset = 0)
        {
            TextBox tb = new TextBox();
            tb.Size = new System.Drawing.Size(40, 20);
            tb.Location = new System.Drawing.Point(xOffset, yOffset);

            return tb;
        }
    }
}

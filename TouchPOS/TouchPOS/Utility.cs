using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace TouchPOS
{
    class Utility
    {
        public static void fitFormToScreen(Form form, int h, int w)
        {

            //scale the form to the current screen resolution
            form.Height = (int)((float)form.Height * ((float)Screen.PrimaryScreen.Bounds.Size.Height / (float)h));
            form.Width = (int)((float)form.Width * ((float)Screen.PrimaryScreen.Bounds.Size.Width / (float)w));

            //here font is scaled like width
            form.Font = new Font(form.Font.FontFamily, form.Font.Size * ((float)Screen.PrimaryScreen.Bounds.Size.Width / (float)w));

            foreach (Control item in form.Controls)
            {
                fitControlsToScreen(item, h, w);
            }

        }

        static void fitControlsToScreen(Control cntrl, int h, int w)
        {
            if (Screen.PrimaryScreen.Bounds.Size.Height != h)
            {

                cntrl.Height = (int)((float)cntrl.Height * ((float)Screen.PrimaryScreen.Bounds.Size.Height / (float)h));
                cntrl.Top = (int)((float)cntrl.Top * ((float)Screen.PrimaryScreen.Bounds.Size.Height / (float)h));

            }
            if (Screen.PrimaryScreen.Bounds.Size.Width != w)
            {

                cntrl.Width = (int)((float)cntrl.Width * ((float)Screen.PrimaryScreen.Bounds.Size.Width / (float)w));
                cntrl.Left = (int)((float)cntrl.Left * ((float)Screen.PrimaryScreen.Bounds.Size.Width / (float)w));

                cntrl.Font = new Font(cntrl.Font.FontFamily, cntrl.Font.Size * ((float)Screen.PrimaryScreen.Bounds.Size.Width / (float)w));

            }

            foreach (Control item in cntrl.Controls)
            {
                fitControlsToScreen(item, h, w);
            }
        }

        public static List<Control> findControls(Control c)
        {
            List<Control> list = new List<Control>();
            foreach (Control control in c.Controls)
            {
                list.Add(control);
                if (c.HasChildren) 
                {
                    foreach (Control control1 in control.Controls) {
                        list.Add(control1);
                        if (control1.HasChildren) 
                        {
                            foreach (Control control2 in control1.Controls) 
                            {
                                list.Add(control2);
                            }
                        }
                    }
                }
            }
            return list;
        }

        public static void relocate(Form fo, int ox, int oy)
        {
            List<Control> list = new List<Control>();
            list = findControls(fo);
            for (int i = 0; i < list.Count; i++)
                reposition(list[i], ox, oy);
        }
        public static void reposition(Control c, int ox, int oy)
        {
            int x, y;
            x = ((c.Location.X * Screen.PrimaryScreen.Bounds.Width) / ox);
            y = ((c.Location.Y * Screen.PrimaryScreen.Bounds.Height) / oy);
            c.Location = new Point(x, y);
            x = ((c.Width * Screen.PrimaryScreen.Bounds.Width) / ox);
            y = ((c.Height * Screen.PrimaryScreen.Bounds.Height) / oy);
            c.Width = x;
            c.Height = y;
            if (c is Label || c is Button)
                resizeText(c, ox, oy);
        }
        public static void resizeText(Control l, int ox, int oy)
        {
            float s;
            float txtsize = l.Font.Size;
            s = ((txtsize * Screen.PrimaryScreen.Bounds.Width) / ox) + 1;
            l.Font = new Font(l.Font.Name, s, l.Font.Style);
        }
        public static void repositionForm(Form f, int ox, int oy)
        {
            int x, y;
            x = (f.Location.X * Screen.PrimaryScreen.Bounds.Width) / ox;
            y = (f.Location.Y * Screen.PrimaryScreen.Bounds.Width) / oy;
            f.Location = new Point(x, y);
        }

    }
}

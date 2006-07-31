using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;

namespace EVEMon.Schedule
{
    public partial class CalendarControl : UserControl
    {
        public CalendarControl()
        {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.Opaque, true);
            SetStyle(ControlStyles.ResizeRedraw, true);

            m_date = DateTime.Now;
        }

        private DayOfWeek m_firstDayOfWeek;
        private CalendarType m_calendarType = CalendarType.Month;

        public CalendarType CalendarType
        {
            get { return m_calendarType; }
            set
            {
                if (m_calendarType != value)
                {
                    m_calendarType = value;
                    this.Invalidate();
                }
            }
        }

        private DateTime m_date;

        public DateTime Date
        {
            get { return m_date; }
            set
            {
                if (m_date != value)
                {
                    m_date = value;
                    this.Invalidate();
                }
            }
        }
        
        private void CalendarControl_Load(object sender, EventArgs e)
        {
            m_firstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            using (Brush b = new LinearGradientBrush(
                this.ClientRectangle, Color.LightBlue, Color.DarkBlue, 90.0F))
            {
                e.Graphics.FillRectangle(b, e.ClipRectangle);
            }

            switch (m_calendarType)
            {
                default:
                case CalendarType.Month:
                    PaintMonthCalendar(e);
                    HighlightDay(e);
                    HighlightToday(e);
                    break;
            }
        }

        private const double CELL_ASPECT_RATIO = 7.0d / 10.0d;
        private const int MAX_ROWS = 6;
        private const int HEADER_HEIGHT = 20;
        private const int DAY_HEADER_HEIGHT = 20;

        private Point m_calTopLeft = new Point(0, 0);
        private Size m_cellSize = new Size(5, 5);

        private void CalculateCellMetrics()
        {
            double maxCellWidth = Math.Floor(Convert.ToDouble(this.ClientSize.Width) / 8.0f);
            double maxCellHeight = Math.Floor(Convert.ToDouble(this.ClientSize.Height - HEADER_HEIGHT - DAY_HEADER_HEIGHT) / Convert.ToDouble(MAX_ROWS+1));

            double heightWithMaxWidth = Math.Floor(maxCellWidth * CELL_ASPECT_RATIO);

            int effectiveHeight = Convert.ToInt32(heightWithMaxWidth);
            int effectiveWidth = Convert.ToInt32(maxCellWidth);
            if (heightWithMaxWidth > maxCellHeight)
            {
                double widthWithMaxHeight = Math.Floor(maxCellHeight / CELL_ASPECT_RATIO);
                effectiveHeight = Convert.ToInt32(maxCellHeight);
                effectiveWidth = Convert.ToInt32(widthWithMaxHeight);
            }

            int calWidth = effectiveWidth * 7;
            int calHeight = effectiveHeight * MAX_ROWS;

            m_calTopLeft = new Point((this.ClientSize.Width / 2) - (calWidth / 2),
                                (this.ClientSize.Height / 2) - ((calHeight + HEADER_HEIGHT + DAY_HEADER_HEIGHT) / 2));
            m_cellSize = new Size(effectiveWidth, effectiveHeight);

        }

        private void PaintMonthCalendar(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            CalculateCellMetrics();

            DateTime mdt = new DateTime(m_date.Year, m_date.Month, 1);
            string ymDesc = mdt.ToString("y");

            DayOfWeek firstDayDow = mdt.DayOfWeek;
            DayOfWeek cDow = m_firstDayOfWeek;
            using (Font boldf = new Font(this.Font, FontStyle.Bold))
            {
                Rectangle headerRect = new Rectangle(m_calTopLeft.X, m_calTopLeft.Y,
                    m_cellSize.Width * 7, HEADER_HEIGHT);
                using (Brush hb = new SolidBrush(Color.FromArgb(47, 77, 132)))
                {
                    g.FillRectangle(hb, headerRect);
                }
                g.DrawRectangle(Pens.Black, headerRect);
                TextRenderer.DrawText(g, ymDesc, boldf,
                    new Rectangle(headerRect.Left + 1, headerRect.Top + 1, headerRect.Width - 2, headerRect.Height - 2),
                    Color.White, Color.Transparent,
                    TextFormatFlags.EndEllipsis | TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                using (Brush db = new SolidBrush(Color.FromArgb(106, 149, 228)))
                {
                    for (int x = 0; x < 7; x++)
                    {
                        Rectangle cellRect = new Rectangle(m_calTopLeft.X + (m_cellSize.Width * x),
                            m_calTopLeft.Y + HEADER_HEIGHT, m_cellSize.Width, DAY_HEADER_HEIGHT);
                        g.FillRectangle(db, cellRect);
                        g.DrawRectangle(Pens.Black, cellRect);

                        string dayName = CultureInfo.CurrentCulture.DateTimeFormat.DayNames[(int)cDow];
                        TextRenderer.DrawText(g, dayName, boldf,
                            new Rectangle(cellRect.Left + 1, cellRect.Top + 1, cellRect.Width - 2, cellRect.Height - 2),
                            Color.Black, Color.Transparent,
                            TextFormatFlags.EndEllipsis | TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                        cDow = (DayOfWeek)(((int)cDow + 1) % 7);
                    }
                }
            }

            bool daysRunning = false;
            using (Brush validDayBrush = new LinearGradientBrush(
                new Rectangle(m_calTopLeft.X, m_calTopLeft.Y + HEADER_HEIGHT + DAY_HEADER_HEIGHT,
                    m_cellSize.Width * 7, m_cellSize.Height * MAX_ROWS),
                    Color.FromArgb(244, 244, 244), Color.FromArgb(203, 220, 228), LinearGradientMode.Vertical))
            {
                for (int y = 0; y < MAX_ROWS; y++)
                {
                    cDow = m_firstDayOfWeek;
                    using (Brush invalidDayBrush = new LinearGradientBrush(
                        new Rectangle(m_calTopLeft.X, m_calTopLeft.Y + HEADER_HEIGHT + DAY_HEADER_HEIGHT + (y * m_cellSize.Height),
                            m_cellSize.Width * 7, m_cellSize.Height),
                            Color.FromArgb(169, 169, 169), Color.FromArgb(140, 140, 140), LinearGradientMode.Vertical))
                    {
                        for (int x = 0; x < 7; x++)
                        {
                            bool isValidDay = false;
                            int dayNum = 0;
                            if (!daysRunning)
                            {
                                if (cDow == firstDayDow)
                                    daysRunning = true;
                            }
                            if (daysRunning)
                            {
                                if (mdt.Month == m_date.Month)
                                {
                                    isValidDay = true;
                                    dayNum = mdt.Day;
                                    mdt = mdt + TimeSpan.FromDays(1);
                                }
                            }

                            Rectangle cellRect = new Rectangle(m_calTopLeft.X + (m_cellSize.Width * x),
                                m_calTopLeft.Y + HEADER_HEIGHT + DAY_HEADER_HEIGHT + (m_cellSize.Height * y), m_cellSize.Width, m_cellSize.Height);
                            if (isValidDay)
                                g.FillRectangle(validDayBrush, cellRect);
                            else
                                g.FillRectangle(invalidDayBrush, cellRect);
                            g.DrawRectangle(Pens.Black, cellRect);

                            if (isValidDay)
                            {
                                TextRenderer.DrawText(g, dayNum.ToString(), this.Font,
                                    new Point(cellRect.Left + 2, cellRect.Top + 2), Color.Black, Color.Transparent,
                                    TextFormatFlags.NoPadding | TextFormatFlags.NoClipping);
                            }

                            cDow = (DayOfWeek)(((int)cDow + 1) % 7);
                        }
                    }
                }
            }
        }

        private void HighlightDay(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            CalculateCellMetrics();
            DateTime mdt = new DateTime(m_date.Year, m_date.Month, 1);

            int box_number = m_date.Day + (((int)mdt.DayOfWeek + 6) % 7) - 1;
            int x_co = box_number % 7;
            int y_co = (int)Math.Floor(box_number / 7.0);
            Rectangle cellRect = new Rectangle(m_calTopLeft.X + (m_cellSize.Width * x_co),
                m_calTopLeft.Y + HEADER_HEIGHT + DAY_HEADER_HEIGHT + (m_cellSize.Height * y_co), m_cellSize.Width, m_cellSize.Height);
            g.DrawRectangle(Pens.DeepSkyBlue, cellRect);
        }

        private void HighlightToday(PaintEventArgs e)
        {
            DateTime today = DateTime.Now;
            DateTime mdt = new DateTime(m_date.Year, m_date.Month, 1);
            if (today.Month == mdt.Month && today.Year == mdt.Year)
            {
                Graphics g = e.Graphics;

                CalculateCellMetrics();

                int box_number = today.Day + (((int)mdt.DayOfWeek + 6) % 7) - 1;
                int x_co = box_number % 7;
                int y_co = (int)Math.Floor(box_number / 7.0);
                Rectangle cellRect = new Rectangle(m_calTopLeft.X + (m_cellSize.Width * x_co),
                    m_calTopLeft.Y + HEADER_HEIGHT + DAY_HEADER_HEIGHT + (m_cellSize.Height * y_co), m_cellSize.Width, m_cellSize.Height);
                g.DrawRectangle(Pens.Violet, cellRect);
            }
        }
    }

    public enum CalendarType
    {
        Month
    }
}

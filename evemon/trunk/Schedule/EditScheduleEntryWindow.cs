using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using EVEMon.Common.Schedule;

namespace EVEMon.Schedule
{
    public partial class EditScheduleEntryWindow : EVEMon.Common.EVEMonForm
    {
        public EditScheduleEntryWindow()
        {
            InitializeComponent();
        }

        private ScheduleEntry m_scheduleEntry = null;

        public ScheduleEntry ScheduleEntry
        {
            get { return m_scheduleEntry; }
            set { m_scheduleEntry = value; UpdateFromEntry(); }
        }

        private void UpdateFromEntry()
        {
            tbTitle.Text = String.Empty;
            SetTypeFlags(ScheduleEntryOptions.None);
            rbOneTime.Checked = true;
            rbRecurring.Checked = false;
            SetOneTimeStartDate(DateTime.Now);
            tbOneTimeStartTime.Text = DateTime.Today.ToShortTimeString();
            DateTime dtto = DateTime.Today + TimeSpan.FromDays(1) - TimeSpan.FromMinutes(1);
            SetOneTimeEndDate(dtto);
            tbOneTimeEndTime.Text = dtto.ToShortTimeString();
            SetRecurringDateFrom(DateTime.MinValue);
            SetRecurringDateTo(DateTime.MaxValue);
            cbRecurringFrequency.SelectedIndex = 0;
            nudRecurDayOfMonth.Value = 1;
            cbRecurOnOverflow.SelectedIndex = 0;
            tbRecurringTimeFrom.Text = DateTime.Today.ToShortTimeString();
            tbRecurringTimeTo.Text = dtto.ToShortTimeString();
            if (m_scheduleEntry == null)
                return;

            tbTitle.Text = m_scheduleEntry.Title;
            SetTypeFlags(m_scheduleEntry.ScheduleEntryOptions);
            if (m_scheduleEntry is SimpleScheduleEntry)
            {
                SimpleScheduleEntry sse = (SimpleScheduleEntry)m_scheduleEntry;
                rbOneTime.Checked = true;
                rbRecurring.Checked = false;
                SetOneTimeStartDate(sse.StartDateTime);
                tbOneTimeStartTime.Text = sse.StartDateTime.ToShortTimeString();
                SetOneTimeEndDate(sse.EndDateTime);
                tbOneTimeEndTime.Text = sse.EndDateTime.ToShortTimeString();
            }
            else if (m_scheduleEntry is RecurringScheduleEntry)
            {
                RecurringScheduleEntry rse = (RecurringScheduleEntry)m_scheduleEntry;
                rbOneTime.Checked = false;
                rbRecurring.Checked = true;
                SetRecurringDateFrom(rse.RecurStart);
                SetRecurringDateTo(rse.RecurEnd);
                SetRecurringFrequencyDropdown(rse.RecurFrequency, rse.RecurDayOfWeek);
                nudRecurDayOfMonth.Value = rse.RecurDayOfMonth;
                SetRecurringOverflowDropdown(rse.OverflowResolution);
                DateTime tstart = DateTime.Today + TimeSpan.FromSeconds(rse.StartSecond);
                DateTime tend = DateTime.Today + TimeSpan.FromSeconds(rse.EndSecond);
                tbRecurringTimeFrom.Text = tstart.ToShortTimeString();
                tbRecurringTimeTo.Text = tend.ToShortTimeString();
            }

            ValidateData();
        }

        private void SetRecurringOverflowDropdown(MonthlyOverflowResolution monthlyOverflowResolution)
        {
            switch (monthlyOverflowResolution)
            {
                default:
                case MonthlyOverflowResolution.Drop:
                    cbRecurOnOverflow.SelectedIndex = 0;
                    break;
                case MonthlyOverflowResolution.OverlapForward:
                    cbRecurOnOverflow.SelectedIndex = 1;
                    break;
                case MonthlyOverflowResolution.ClipBack:
                    cbRecurOnOverflow.SelectedIndex = 2;
                    break;
            }
        }

        private MonthlyOverflowResolution GetRecurringOverflowDropdown()
        {
            switch (cbRecurOnOverflow.SelectedIndex)
            {
                default:
                case 0:
                    return MonthlyOverflowResolution.Drop;
                case 1:
                    return MonthlyOverflowResolution.OverlapForward;
                case 2:
                    return MonthlyOverflowResolution.ClipBack;
            }
        }

        private void SetRecurringFrequencyDropdown(RecurFrequency recurFrequency, DayOfWeek recurDow)
        {
            switch (recurFrequency)
            {
                default:
                case RecurFrequency.Daily:
                    cbRecurringFrequency.SelectedIndex = 0;
                    break;
                case RecurFrequency.Weekdays:
                    cbRecurringFrequency.SelectedIndex = 1;
                    break;
                case RecurFrequency.Weekends:
                    cbRecurringFrequency.SelectedIndex = 2;
                    break;
                case RecurFrequency.Weekly:
                    switch (recurDow)
                    {
                        default:
                        case DayOfWeek.Monday:
                            cbRecurringFrequency.SelectedIndex = 3;
                            break;
                        case DayOfWeek.Tuesday:
                            cbRecurringFrequency.SelectedIndex = 4;
                            break;
                        case DayOfWeek.Wednesday:
                            cbRecurringFrequency.SelectedIndex = 5;
                            break;
                        case DayOfWeek.Thursday:
                            cbRecurringFrequency.SelectedIndex = 6;
                            break;
                        case DayOfWeek.Friday:
                            cbRecurringFrequency.SelectedIndex = 7;
                            break;
                        case DayOfWeek.Saturday:
                            cbRecurringFrequency.SelectedIndex = 8;
                            break;
                        case DayOfWeek.Sunday:
                            cbRecurringFrequency.SelectedIndex = 9;
                            break;
                    }
                    break;
                case RecurFrequency.Monthly:
                    cbRecurringFrequency.SelectedIndex = 10;
                    break;
            }
        }

        private RecurFrequency GetRecurringFrequencyDropdown(ref DayOfWeek dow)
        {
            switch (cbRecurringFrequency.SelectedIndex)
            {
                default:
                case 0:
                    return RecurFrequency.Daily;
                case 1:
                    return RecurFrequency.Weekdays;
                case 2:
                    return RecurFrequency.Weekends;
                case 3:
                    dow = DayOfWeek.Monday;
                    return RecurFrequency.Weekly;
                case 4:
                    dow = DayOfWeek.Tuesday;
                    return RecurFrequency.Weekly;
                case 5:
                    dow = DayOfWeek.Wednesday;
                    return RecurFrequency.Weekly;
                case 6:
                    dow = DayOfWeek.Thursday;
                    return RecurFrequency.Weekly;
                case 7:
                    dow = DayOfWeek.Friday;
                    return RecurFrequency.Weekly;
                case 8:
                    dow = DayOfWeek.Saturday;
                    return RecurFrequency.Weekly;
                case 9:
                    dow = DayOfWeek.Sunday;
                    return RecurFrequency.Weekly;
                case 10:
                    return RecurFrequency.Monthly;
            }
        }

        private void SetTypeFlags(ScheduleEntryOptions scheduleEntryOptions)
        {
            cbBlocking.Checked = ((scheduleEntryOptions & ScheduleEntryOptions.Blocking) != 0);
            cbSilent.Checked = ((scheduleEntryOptions & ScheduleEntryOptions.Quiet) != 0);
        }

        private ScheduleEntryOptions GetTypeFlags()
        {
            ScheduleEntryOptions result = ScheduleEntryOptions.None;
            if (cbBlocking.Checked)
                result |= ScheduleEntryOptions.Blocking;
            if (cbSilent.Checked)
                result |= ScheduleEntryOptions.Quiet;
            return result;
        }

        private DateTime m_recurringDateFrom = DateTime.MinValue;
        private DateTime m_recurringDateTo = DateTime.MaxValue;

        private void SetRecurringDateTo(DateTime dateTime)
        {
            if (dateTime == DateTime.MinValue || dateTime == DateTime.MaxValue)
            {
                tbRecurringEndDate.Text = "(Forever)";
                m_recurringDateTo = DateTime.MaxValue;
            }
            else
            {
                tbRecurringEndDate.Text = dateTime.ToLongDateString();
                m_recurringDateTo = StripToDate(dateTime);
            }
        }

        private void SetRecurringDateFrom(DateTime dateTime)
        {
            if (dateTime == DateTime.MinValue || dateTime == DateTime.MaxValue)
            {
                tbRecurringStartDate.Text = "(Forever)";
                m_recurringDateFrom = DateTime.MinValue;
            }
            else
            {
                tbRecurringStartDate.Text = dateTime.ToLongDateString();
                m_recurringDateFrom = StripToDate(dateTime);
            }
        }

        private DateTime m_oneTimeStartDate = DateTime.Now;
        private DateTime m_oneTimeEndDate = DateTime.Now;

        private DateTime StripToDate(DateTime dateTime)
        {
            return dateTime -
                TimeSpan.FromHours(dateTime.Hour) -
                TimeSpan.FromMinutes(dateTime.Minute) -
                TimeSpan.FromSeconds(dateTime.Second) -
                TimeSpan.FromMilliseconds(dateTime.Millisecond);
        }

        private void SetOneTimeStartDate(DateTime dateTime)
        {
            tbOneTimeStartDate.Text = dateTime.ToLongDateString();
            m_oneTimeStartDate = StripToDate(dateTime);
        }

        private void SetOneTimeEndDate(DateTime dateTime)
        {
            tbOneTimeEndDate.Text = dateTime.ToLongDateString();
            m_oneTimeEndDate = StripToDate(dateTime);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void rbOneTime_CheckedChanged(object sender, EventArgs e)
        {
            pnlOneTime.Enabled = rbOneTime.Checked;
        }

        private void rbRecurring_CheckedChanged(object sender, EventArgs e)
        {
            pnlRecurring.Enabled = rbRecurring.Checked;
        }

        private void cbRecurringFrequency_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlRecurMonthly.Enabled = (cbRecurringFrequency.SelectedIndex == 10);
        }

        private void btnRecurringNoStartDate_Click(object sender, EventArgs e)
        {
            SetRecurringDateFrom(DateTime.MinValue);
        }

        private void btnRecurringNoEndDate_Click(object sender, EventArgs e)
        {
            SetRecurringDateTo(DateTime.MaxValue);
        }

        //TODO: set these in validatedata
        private int m_oneTimeStartTime = 0;
        private int m_oneTimeEndTime = 0;
        private int m_recurringStartTime = 0;
        private int m_recurringEndTime = 0;

        private bool TryParseTime(string text, out int seconds)
        {
            DateTime res;
            if (!DateTime.TryParse("2000/01/01 " + text, out res))
            {
                seconds = 0;
                return false;
            }
            DateTime b = new DateTime(2000, 1, 1);
            TimeSpan diff = res - b;
            seconds = Convert.ToInt32(diff.TotalSeconds);
            return true;
        }

        private void ValidateData()
        {
            bool valid = true;
            if (String.IsNullOrEmpty(tbTitle.Text) || String.IsNullOrEmpty(tbTitle.Text.Trim()))
            {
                valid = false;
            }
            else
            {
                if (rbOneTime.Checked)
                {
                    int startSec;
                    int endSec;
                    if (!TryParseTime(tbOneTimeStartTime.Text, out startSec) ||
                        !TryParseTime(tbOneTimeEndTime.Text, out endSec))
                    {
                        valid = false;
                    }
                    else
                    {
                        DateTime startDate = m_oneTimeStartDate + TimeSpan.FromSeconds(startSec);
                        DateTime endDate = m_oneTimeEndDate + TimeSpan.FromSeconds(endSec);
                        if (startDate >= endDate)
                        {
                            valid = false;
                        }
                        else
                        {
                            m_oneTimeStartTime = startSec;
                            m_oneTimeEndTime = endSec;
                        }
                    }
                }
                else if (rbRecurring.Checked)
                {
                    if (m_recurringDateFrom > m_recurringDateTo)
                    {
                        valid = false;
                    }
                    else
                    {
                        int startSec;
                        int endSec;
                        if (!TryParseTime(tbRecurringTimeFrom.Text, out startSec) ||
                            !TryParseTime(tbRecurringTimeTo.Text, out endSec))
                        {
                            valid = false;
                        }
                        else
                        {
                            if (startSec >= endSec)
                                endSec += RecurringScheduleEntry.SECONDS_PER_DAY;
                            m_recurringStartTime = startSec;
                            m_recurringEndTime = endSec;
                        }
                    }
                }
                else
                {
                    valid = false;
                }
            }

            btnOk.Enabled = valid;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            m_scheduleEntry = GenerateScheduleEntry();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private ScheduleEntry GenerateScheduleEntry()
        {
            ScheduleEntry result = null;
            if (rbOneTime.Checked)
            {
                SimpleScheduleEntry sse = new SimpleScheduleEntry();
                sse.StartDateTime = m_oneTimeStartDate +
                    TimeSpan.FromSeconds(m_oneTimeStartTime);
                sse.EndDateTime = m_oneTimeEndDate +
                    TimeSpan.FromSeconds(m_oneTimeEndTime);
                result = sse;
            }
            else if (rbRecurring.Checked)
            {
                RecurringScheduleEntry rse = new RecurringScheduleEntry();
                rse.RecurStart = m_recurringDateFrom;
                rse.RecurEnd = m_recurringDateTo;
                DayOfWeek dow = DayOfWeek.Monday;
                rse.RecurFrequency = GetRecurringFrequencyDropdown(ref dow);
                rse.RecurDayOfWeek = dow;
                rse.RecurDayOfMonth = Convert.ToInt32(nudRecurDayOfMonth.Value);
                rse.OverflowResolution = GetRecurringOverflowDropdown();
                rse.StartSecond = m_recurringStartTime;
                rse.EndSecond = m_recurringEndTime;
                result = rse;
            }

            if (result != null)
            {
                result.Title = tbTitle.Text;
                result.ScheduleEntryOptions = GetTypeFlags();
            }
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!btnOk.Enabled)
                return;
            ScheduleEntry ise = GenerateScheduleEntry();
            this.ScheduleEntry = ise;
        }

        private void tbTitle_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void tbOneTimeStartTime_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void tbOneTimeEndTime_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void tbRecurringTimeFrom_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void tbRecurringTimeTo_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void EditScheduleEntryWindow_Load(object sender, EventArgs e)
        {
            UpdateFromEntry();
        }

        private bool GetDate(ref DateTime res)
        {
            using (DateSelectWindow f = new DateSelectWindow())
            {
                if (res == DateTime.MinValue || res == DateTime.MaxValue)
                    f.SelectedDate = DateTime.Today;
                else
                    f.SelectedDate = res;
                DialogResult dr = f.ShowDialog();
                if (dr == DialogResult.Cancel)
                    return false;
                res = f.SelectedDate;
                return true;
            }
        }

        private void btnOneTimeStartDateChoose_Click(object sender, EventArgs e)
        {
            if (GetDate(ref m_oneTimeStartDate))
                SetOneTimeStartDate(m_oneTimeStartDate);
            ValidateData();
        }

        private void btnOneTimeEndDateChoose_Click(object sender, EventArgs e)
        {
            if (GetDate(ref m_oneTimeEndDate))
                SetOneTimeEndDate(m_oneTimeEndDate);
            ValidateData();
        }

        private void btnRecurringStartDateChoose_Click(object sender, EventArgs e)
        {
            if (GetDate(ref m_recurringDateFrom))
                SetRecurringDateFrom(m_recurringDateFrom);
            ValidateData();
        }

        private void btnRecurringEndDateChoose_Click(object sender, EventArgs e)
        {
            if (GetDate(ref m_recurringDateTo))
                SetRecurringDateTo(m_recurringDateTo);
            ValidateData();
        }
    }
}


using System;
using UnityEngine;

namespace Almond.Attendance.Monthly
{
	public class PlayerPrefsMonthlyRecord : MonthlyRecordBase
	{
		public PlayerPrefsMonthlyRecord(DateTime day) : base(day){}

		public override DateTime RecordMonth => base.RecordMonth;
		public override DateTime StartDay => base.StartDay;

		private string DataKey => $"Monthly_{RecordMonth.Year}_{RecordMonth.Month}";

		private uint mAttendanceData = 0;
		protected override uint AttendanceData
		{
			get
			{ 

			}
			set
			{
				mAttendanceData = value;
				PlayerPrefs.SetString(DataKey, mAttendanceData.ToString());
			}
		}
	}
}
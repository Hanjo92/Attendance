using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace Almond.Attendance.Monthly
{
	public class MonthlyRecordBase
	{
		public virtual DateTime RecordMonth { get; private set; }

		public virtual DateTime StartDay { get; private set; }

		public int DayCount => DateTime.DaysInMonth(RecordMonth.Year, RecordMonth.Month);
		protected virtual uint AttendanceData { get; set; }
		public int TotalAttendanceDays
		{
			get
			{
				var count = 0;
				var index = 1;
				while(index <= DayCount)
				{
					if((AttendanceData & (1 << index)) != 0)
						count++; 
					index++;
				}
				return count;
			}
		}
		
		public MonthlyRecordBase(DateTime day)
		{
			RecordMonth = day.AddDays(1 - day.Day);
			StartDay = day;
		}

		public bool CheckAttendanceDay(DateTime day)
		{
			if(day.Year != RecordMonth.Year || day.Month != RecordMonth.Month) return false;
			return (AttendanceData & (1 << day.Day)) != 0;
		}
		public void SetAttendanceDay(DateTime day, bool chech = true)
		{
			if(day.Year != RecordMonth.Year || day.Month != RecordMonth.Month) return;
			var locateBit = (uint)1 << day.Day;

			if(chech)
			{
				AttendanceData = AttendanceData | locateBit;
			}
			else
			{
				AttendanceData = AttendanceData & (~locateBit);
			}
		}
		public int CurrentContinuousDays()
		{
			var today = DateTime.Today;
			if(today.Year != RecordMonth.Year || today.Month != RecordMonth.Month)
				return 0;
			var checker = (uint)1 << today.Day;
			var continuous = 0;
			while(checker > 0)
			{
				var check = (checker & AttendanceData) > 0;
				if(check == false)
					break;
				
				checker = checker >> 1;
				continuous++;
			}

			return continuous;
		}
	}
}
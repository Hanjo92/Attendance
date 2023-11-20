using System;

namespace Almond.Attendance
{
	public interface IAttendanceRecord
	{
		int DateCount { get; }
		int AttendanceCount { get; }

		void Clear();

		void SetAttendanceToday(bool value = true);
		
	}
}
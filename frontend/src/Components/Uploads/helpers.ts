export const ParseDate = (date: string): string => {
  const now = new Date();
  const providedDate = new Date(date);

  const timeDifference = now.getTime() - providedDate.getTime();
  const minutesDifference = timeDifference / (1000 * 60);

  if (minutesDifference < 60) {
    const flooredMinutesDifference = Math.floor(minutesDifference);

    return flooredMinutesDifference == 1
      ? "1 minute ago"
      : `${flooredMinutesDifference} minutes ago`;
  }

  const hourDifference = timeDifference / (1000 * 60 * 60);

  if (hourDifference < 24) {
    const flooredHourDifference = Math.floor(hourDifference);

    return flooredHourDifference == 1
      ? "1 hour ago"
      : `${flooredHourDifference} hours ago`;
  }

  const dayDifference = hourDifference / 24;

  if (dayDifference < 7) {
    const flooredDayDifference = Math.floor(dayDifference);

    return flooredDayDifference == 1
      ? "1 day ago"
      : `${flooredDayDifference} days ago`;
  }

  const weekDifference = dayDifference / 7;

  if (weekDifference < 4) {
    const flooredWeekDifference = Math.floor(weekDifference);

    return flooredWeekDifference == 1
      ? "1 week ago"
      : `${flooredWeekDifference} weeks ago`;
  }

  const monthDifference = weekDifference / 4;

  if (monthDifference < 12) {
    const flooredMonthDifference = Math.floor(monthDifference);

    return flooredMonthDifference == 1
      ? "1 month ago"
      : `${flooredMonthDifference} months ago`;
  }

  const yearDifference = monthDifference / 12;

  const flooredYearDifference = Math.floor(yearDifference);

  return flooredYearDifference == 1
    ? "1 year ago"
    : `${flooredYearDifference} years ago`;
};

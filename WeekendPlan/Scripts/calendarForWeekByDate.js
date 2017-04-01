function movetodate(dateVal, changeFormat) {
    //  alert(dateVal);
    if (!changeFormat)
        location.href = '/Task/TaskCalendarWeek/?taskDate=' + dateVal;
    else
        location.href = '/Task/TaskCalendarByDateChangeFormat/?taskDate=' + dateVal;
}
﻿function movetoday(dateVal, changeFormat) {
    //  alert(dateVal);
    location.href = '/Task/TaskCalendarByDate/?taskDate=' + dateVal;
    //if (!changeFormat)
    //    location.href = '/Task/TaskCalendarByDate/?taskDate=' + dateVal;
    //else
    //    location.href = '/Task/TaskCalendarByDateChangeFormat/?taskDate=' + dateVal;
}
function getDateTime(d) { // new Date()
    let year = d.getFullYear();
    let month = d.getMonth() + 1; // zero-index (Jan. == 0)
    let date = d.getDate();
    let hours = d.getHours();
    let minutes = d.getMinutes();
    let seconds = d.getSeconds();
    let monthHr = month < 10 ? "0" + month : month;
    let dateHr = date < 10 ? "0" + date : date;
    let hoursHr = hours < 10 ? "0" + hours : hours;
    let minutesHr = minutes < 10 ? "0" + minutes : minutes;
    let secondsHr = seconds < 10 ? "0" + seconds : seconds;

    return year + "-" + monthHr + "-" + dateHr + " " + hoursHr + ":" + minutesHr + ":" + secondsHr;
}
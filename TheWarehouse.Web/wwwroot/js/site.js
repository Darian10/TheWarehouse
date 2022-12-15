//function $$(tt) { return tt < 10 ? "0" + tt : tt; };
//var today = new Date();
//var hour = $$(today.getHours());
//var minu = $$(today.getMinutes());
//var second = $$(today.getSeconds());
//var total = 6000;
//var actime = 'null';
//if ('notFound.jsp' != 'notFound.jsp') {
//    var newWnd = window.open("notFound.jsp", "_blank"); newWnd.opener = null;
//}
//function set() {
//    var loginTime = total - parseInt((new Date().getTime() - today.getTime()) / 1000);
//    if (loginTime++ > 600000) {
//        today = new Date();
//        alert("Ha iniciado sesión en una semana. Reajuste el tiempo.");
//    }
//    if (actime > 0) {
//        setonTime(actime);
//        actime++;
//    }
//    else if (loginTime > 0) {
//        setonTime(loginTime);
//    }
//}
//setInterval(set, 1000);
//function setonTime(loginTime) {
//    var hours = 0;
//    var minutes = 0;
//    var seconds = 0;

//    hours = Math.floor(loginTime / 3600);
//    minutes = Math.floor((loginTime % 3600) / 60);
//    seconds = loginTime % 60;

//    if (hours <= 9) {
//        hours = "0" + hours;
//    }
//    if (minutes <= 9) {
//        minutes = "0" + minutes;
//    }
//    if (seconds <= 9) {
//        seconds = "0" + seconds;
//    }
//    var cdate = hours + ":" + minutes + ":" + seconds;
//    document.getElementById('onlineTime').innerHTML = cdate;
//}
//window.onload = function () {
//    document.getElementById("availableTime").innerHTML = "12:00:00";
//    updateAvailableTime();
//    set();
//}


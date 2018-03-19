function showDialog(diglogContent) {
  $.alert({
    title: '通知',
    content: diglogContent,
    autoClose: '已知|1000',
    buttons: {
      已知: function () {}
    }
  });
}

//状态通知框
function loadingDialog() {
  var d = $.dialog({
    title: '通知',
    content: '正在提交...',
    closeIcon: false
  });
  return d;
}
// $(".jconfirm").remove();
//设置cookie
function setCookie(name, value, time) {
  var strsec = getsec(time);
  var exp = new Date();
  exp.setTime(exp.getTime() + strsec * 1);
  document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString() + ";path=/";
}

function getsec(str) {
  var str1 = str.substring(1, str.length) * 1;
  var str2 = str.substring(0, 1);
  if (str2 == "s") {
    return str1 * 1000;
  } else if (str2 == "h") {
    return str1 * 60 * 60 * 1000;
  } else if (str2 == "d") {
    return str1 * 24 * 60 * 60 * 1000;
  }
}

//读取cookie
function getCookie(name) {
  var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");
  if (arr = document.cookie.match(reg))
    return unescape(arr[2]);
  else
    return null;
}
//删除cookie
function delCookie(name) {
  var exp = new Date();
  exp.setTime(exp.getTime() - 1);
  var cval = getCookie(name);
  if (cval != null)
    document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString() + ";path=/";
}
// var studentMessage = getCookie("student");
// var teacherMessage = getCookie("teacher");
// var adminMessage = getCookie("admin");
// if (studentMessage == null && teacherMessage == null && adminMessage == null) {
//   window.location.href = "/Account/Login";
// }

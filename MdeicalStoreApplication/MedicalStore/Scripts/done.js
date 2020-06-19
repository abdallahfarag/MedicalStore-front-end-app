function go() {
  var myForm = document.forms["mayform"];
  var city = document.forms["mayform"]["city"];
  var phone = document.forms["mayform"]["phone"];
  var dat = document.querySelector(".c-data");
  var pdata = document.querySelector(".p-data");
  var x = true;
  if (city.value == "" || phone.value == "") {
    alert("Please Write Your Data Correctly");
    x = false;
  } else {
    dat.style.display = "none";
    pdata.style.display = "block";
    x = true;
  }
}
function cash() {
  var pdata = document.querySelector(".p-data");
  var finsh = document.querySelector(".finsh");
  // *******
  pdata.style.display = "none";
  finsh.style.display = "block";
}

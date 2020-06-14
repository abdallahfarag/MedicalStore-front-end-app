// function sub() {
//   const myform = document.forms["myForm"];
//   const user = document.forms["myForm"]["user"];
//   const mail = document.forms["myForm"]["mail"];
//   const pass = document.forms["myForm"]["pass"];
//   const conpass = document.forms["myForm"]["con"];
//   const errname = document.querySelector("#errname");
//   const erremail = document.querySelector("#erremail");
//   const errpass = document.querySelector("#errpass");
//   const errpass1 = document.querySelector("#errpass1");
//   const mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
//   const numbers = /^[A-Za-z]\w{7,14}$/;
//   let check = true;
//   //////////////////////////
//   event.preventDefault();
//   if (!isNaN(user.value)) {
//     user.style.borderBottom = "2px solid red";
//     user.style.transition = "all 0.3s ease-in-out";
//     errname.style.display = "block";
//     errname.textContent = "plz Enter Valid Name";
//     check = false;
//   }
//   user.addEventListener("keypress", function () {
//     errname.style.display = "none";
//     user.style.borderBottom = "1px solid #ffffff";
//   });
//   //#region
//   if (mail.value == "" || !mailformat.test(myForm.mail.value)) {
//     mail.style.borderBottom = "2px solid red";
//     mail.style.transition = "all 0.3s ease-in-out";
//     erremail.style.display = "block";
//     erremail.textContent = "Not Valid Email";
//     check = false;
//   }
//   mail.addEventListener("keypress", function () {
//     erremail.style.display = "none";
//     mail.style.borderBottom = "1px solid #ffffff";
//   });
//   //#endregion
//   if (pass.value == "" || !numbers.test(myForm.pass.value)) {
//     pass.style.borderBottom = "2px solid red";
//     pass.style.transition = "all 0.3s ease-in-out";
//     errpass.style.display = "block";
//     errpass.textContent = "plz Enter Valid pass";
//     check = false;
//   }
//   pass.addEventListener("keypress", function () {
//     errpass.style.display = "none";
//     pass.style.borderBottom = "1px solid #ffffff";
//   });
//   if (conpass.value !== pass.value || conpass.value == "") {
//     conpass.style.borderBottom = "2px solid red";
//     conpass.style.transition = "all 0.3s ease-in-out";
//     errpass1.style.display = "block";
//     errpass1.textContent = "plz Enter Valid pass";
//     check = false;
//   }
//   conpass.addEventListener("keypress", function () {
//     errpass1.style.display = "none";
//     conpass.style.borderBottom = "1px solid #ffffff";
//   });
//   if (check == true) {
//     alert("hi ya" + user.value);
//   }
// }
// function log() {
//   const main = document.querySelector(".main");
//   const login = document.querySelector(".login");
//   login.style.display = "none";
//   main.style.display = "block";
// }
// function reg() {
//   const main = document.querySelector(".main");
//   const login = document.querySelector(".login");
//   login.style.display = "block";
//   main.style.display = "none";
// }
// // // // // // // //
// var str = location.href;
// str1 = str.toString();
// str2 = str1[str.length - 1];
// var main = document.querySelector(".main");
// var login = document.querySelector(".login");
// if (str2 == 1) {
//   login.style.display = "none";
//   main.style.display = "block";
// } else if (str2 == 2) {
//   login.style.display = "block";
//   main.style.display = "none";
// }

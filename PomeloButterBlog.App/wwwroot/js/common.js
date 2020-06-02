
function mobileBtn() {
    var toggleMenu = document.getElementsByClassName("menu-toggle")[0];
    var mobileMenu = document.getElementById("mobile-menu");
    if (toggleMenu.classList.contains("active")) {
        toggleMenu.classList.remove("active");
        mobileMenu.classList.remove("active");
    } else {;
        toggleMenu.classList.add("active");
        mobileMenu.classList.add("active");
    }
}
//function RemoveImageBody(flag) {
//
//    if (flag) {
//        document.getElementsByClassName("dark-theme")[0].classList.remove("imageBody");
//    } else {
//        document.getElementsByClassName("dark-theme")[0].classList.add("imageBody");
//    }
//}

const iconOpen = document.getElementById('iconopen')
const iconClose = document.getElementById('iconclose')
const navLeft = document.getElementById('navLeft')
const menu = document.getElementById('menu')

navLeft.addEventListener('click', () => {
    iconOpen.classList.toggle('hide')
    iconClose.classList.toggle('hide')
    menu.classList.toggle('hide')
})

window.addEventListener("scroll", function () {
    const navbar = document.getElementById("myNavbar");
    const scrollPosition = window.scrollY;

    if (scrollPosition > 10) { 
        navbar.classList.add("fixed");
    } else {
        navbar.classList.remove("fixed");
    }
});

ScrollReveal({ delay: 250 }).reveal(".scrollreveal");
ScrollReveal({ delay: 3 }).reveal(".scrollreveal-noreturn");


var toggleButton = document.getElementById('centered-toggle-button');
var modal = document.getElementById('myModal');
var closeModalButton = document.getElementById('closeModal');

toggleButton.onclick = function () {
    modal.style.display = 'block';
};

closeModalButton.onclick = function () {
    modal.style.display = 'none';
};

window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = 'none';
    }
};

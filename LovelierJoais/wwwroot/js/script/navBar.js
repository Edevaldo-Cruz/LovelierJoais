
var nav = document.querySelector('nav');

window.addEventListener('scroll', function () {
    if (window.pageYOffset > 500) {
        nav.classList.add('nav-div', 'shadow');
    } else {
        nav.classList.remove('nav-div', 'shadow');
    }
});
